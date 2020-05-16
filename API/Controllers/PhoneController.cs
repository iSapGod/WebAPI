using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApiTestTask.Models;
using WebApiTestTask.Services;

namespace WebApiTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PhoneController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPhoneService _phoneService;

        public PhoneController(IMapper mapper, IPhoneService phoneService)
        {
            _mapper = mapper;
            _phoneService = phoneService;
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            await _phoneService.SeedData();
            return Ok();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPhones()
        {
            var phones = await _phoneService.GetAll();
            if (phones.Any())
            {
                var mapped = _mapper.Map<List<PhoneModel>>(phones);
                return Ok(mapped);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("byid/{id}")]
        public async Task<IActionResult> GetPhoneById(Guid id)
        {
            var phone = await _phoneService.GetPhoneById(id);
            if (phone != null)
            {
                var mapped = _mapper.Map<PhoneModel>(phone);
                return Ok(mapped);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("bymanufacturer/{manufacturer}")]
        public async Task<IActionResult> GetPhoneByManufacturer(string manufacturer)
        {
            var phones = await _phoneService.GetPhonesByManufacturer(manufacturer);
            if (phones.Any())
            {
                var mapped = _mapper.Map<List<PhoneModel>>(phones);
                return Ok(mapped);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("addnew")]
        public async Task<IActionResult> AddPhone([FromBody] PhoneModel model)
        {
            if(model.Id != Guid.Empty)
            {
                return BadRequest();
            }
            else
            {
                var mapped = _mapper.Map<Phone>(model);
                mapped.Id = Guid.NewGuid();
                await _phoneService.AddPhone(mapped);
                return Ok();
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePhoneInfo([FromBody] PhoneModel model)
        {
            if(model.Id == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {
                var phone = await _phoneService.GetPhoneById(model.Id);
                if(phone == null)
                {
                    return NotFound();
                }
                var mapped = _mapper.Map<Phone>(model);
                await _phoneService.UpdatePhoneInfo(mapped);
                return Ok();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var phone = await _phoneService.GetPhoneById(id);
            if (phone != null)
            {
                await _phoneService.DeletePhone(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] PhoneModel model)
        {
            var phone = await _phoneService.GetPhoneById(model.Id);
            if (phone != null)
            {
                var mapped = _mapper.Map<Phone>(model);
                await _phoneService.DeletePhone(mapped);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
