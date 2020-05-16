using AutoMapper;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApiTestTask.Controllers;
using WebApiTestTask.Models;
using WebApiTestTask.Profiles;
using Xunit;

namespace web_api_tests
{
    public class PhoneControllerTest
    {
        FakePhoneService _service;
        PhoneController _controller;
        IMapper _mapper;
        public PhoneControllerTest()
        {
            _service = new FakePhoneService();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PhoneProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _controller = new PhoneController(_mapper, _service);

        }

        [Fact]
        public async void TestGetAll()
        {
            var okResult = await _controller.GetAllPhones();
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async void TestGetCount()
        {
            var phones = await _service.GetAll();
            Assert.Equal(5, phones.Count);
        }

        [Fact]
        public async void GetByIdResult()
        {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a521");
            var byId = await _service.GetPhoneById(id);
            Assert.NotNull(byId);
        }

        [Fact]
        public async void GetById()
        {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a521");
            var byId = await _service.GetPhoneById(id);
            Assert.Equal(id, byId.Id);
        }

        [Fact]
        public async void AddPhone()
        {
            Phone phone = new Phone
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Blackberry",
                Model = "Priv",
                Price = 175
            };
            var oldCount = _service.GetAll().GetAwaiter().GetResult().Count;
            await _service.AddPhone(phone);
            var newCount = _service.GetAll().GetAwaiter().GetResult().Count;
            Assert.Equal(++oldCount, newCount);
        }

        [Fact]
        public async void AddPhoneResult()
        {
            Phone phone = new Phone
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Blackberry",
                Model = "Priv",
                Price = 175
            };
            var addResult = await _controller.AddPhone(_mapper.Map<PhoneModel>(phone));
            Assert.IsType<OkResult>(addResult);
        }

       [Fact]
       public async void UpdatePhone()
       {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a521");
            var byId = await _service.GetPhoneById(id);
            byId.Price = 165;
            await _service.UpdatePhoneInfo(byId);
            var updated = await _service.GetPhoneById(id);
            Assert.Equal(165, updated.Price);
        }

        [Fact]
        public async void UpdatePhoneResult()
        {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a521");
            var byId = await _service.GetPhoneById(id);
            byId.Price = 165;
            await _service.UpdatePhoneInfo(byId);
            var updated = await _controller.GetPhoneById(id);
            Assert.IsType<OkObjectResult>(updated);
        }

        [Fact]
        public async void TestSeed()
        {
            bool seedResult = await _service.SeedData(true);
            Assert.True(seedResult);
        }

        [Fact]
        public async void DeleteById()
        {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a524");
            var oldCount = _service.GetAll().GetAwaiter().GetResult().Count;
            await _service.DeletePhone(id);
            var newCount = _service.GetAll().GetAwaiter().GetResult().Count;
            Assert.Equal(--oldCount, newCount);
        }

        [Fact]
        public async void Delete()
        {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a524");
            var phone = await _service.GetPhoneById(id);
            var oldCount = _service.GetAll().GetAwaiter().GetResult().Count;
            await _service.DeletePhone(phone);
            var newCount = _service.GetAll().GetAwaiter().GetResult().Count;
            Assert.Equal(--oldCount, newCount);
        }

        [Fact]
        public async void DeleteByIdResult()
        {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a524");
            var deleted = await _controller.DeleteById(id);
            Assert.IsType<OkResult>(deleted);
        }

        [Fact]
        public async void DeleteResult()
        {
            Guid id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a524");
            var phone = await _service.GetPhoneById(id);
            var deleted = await _controller.Delete(_mapper.Map<PhoneModel>(phone));
            Assert.IsType<OkResult>(deleted);
        }
    }
}
