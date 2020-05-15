using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTestTask.Services;

namespace WebApiTestTask.Controllers
{
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
    }
}
