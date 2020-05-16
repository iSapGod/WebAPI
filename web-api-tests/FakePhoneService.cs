using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using WebApiTestTask.Services;

namespace web_api_tests
{
    public class FakePhoneService : IPhoneService
    {
        private readonly List<Phone> _phones;

        public FakePhoneService()
        {
            _phones = new List<Phone>
            {
                new Phone
                {
                    Id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a521"),
                    Manufacturer = "Microsoft",
                    Model = "Lumia 950 XL",
                    Price = 150
                },
                new Phone
                {
                    Id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a522"),
                    Manufacturer = "Apple",
                    Model = "iPhone XR",
                    Price = 200
                },
                new Phone
                {
                    Id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a523"),
                    Manufacturer = "Meizu",
                    Model = "17th",
                    Price = 100
                },
                new Phone
                {
                    Id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a524"),
                    Manufacturer = "Samsung",
                    Model = "Galaxy Note 20",
                    Price = 250
                },
                new Phone
                {
                    Id = Guid.Parse("ca6529ea-7600-4a36-903e-b5b1f0d0a525"),
                    Manufacturer = "Oppo",
                    Model = "Ace 3",
                    Price = 250
                }
            };
        }
        public async Task AddPhone(Phone phone)
        {
            _phones.Add(phone);
        }

        public async Task DeletePhone(Guid guid)
        {
            var phone = await GetPhoneById(guid);
            _phones.Remove(phone);
        }

        public async Task DeletePhone(Phone phone)
        {
            _phones.Remove(phone);
        }

        public async Task<List<Phone>> GetAll()
        {
            return _phones;
        }

        public async Task<Phone> GetPhoneById(Guid id)
        {
            return _phones.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<List<Phone>> GetPhonesByManufacturer(string manufacturer)
        {
            return _phones.Where(x => x.Manufacturer == manufacturer).ToList();
        }

        public async Task SeedData()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SeedData(bool param)
        {
            if(_phones.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdatePhoneInfo(Phone phone)
        {
            var oldPhoneValues = _phones.SingleOrDefault(x => x.Id == phone.Id);
            oldPhoneValues.Manufacturer = phone.Manufacturer;
            oldPhoneValues.Model = phone.Model;
            oldPhoneValues.Price = phone.Price;
        }
    }
}
