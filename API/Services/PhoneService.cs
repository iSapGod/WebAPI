using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApiTestTask.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly EFContext _dbContext;

        public PhoneService(EFContext context)
        {
            _dbContext = context;
        }
        public async Task AddPhone(Phone phone)
        {
            _dbContext.Phones.Add(phone);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePhone(Guid guid)
        {
             _dbContext.Phones.Remove(_dbContext.Phones.Single(x=>x.Id == guid));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePhone(Phone phone)
        {
            _dbContext.Remove(phone);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Phone>> GetAll()
        {
            return await _dbContext.Phones.ToListAsync();
        }

        public async Task<Phone> GetPhoneById(Guid id)
        {
            return await _dbContext.Phones.FindAsync(id);
        }

        public async Task<List<Phone>> GetPhonesByManufacturer(string manufacturer)
        {
            return await _dbContext.Phones.Where(x => x.Manufacturer == manufacturer).ToListAsync();
        }

        public async Task SeedData()
        {
            List<Phone> phones = new List<Phone>
            {
                new Phone
                {
                    Manufacturer = "Microsoft",
                    Model = "Lumia 950 XL",
                    Price = 150
                },
                new Phone
                {
                    Manufacturer = "Apple",
                    Model = "iPhone XR",
                    Price = 200
                },
                new Phone
                {
                    Manufacturer = "Meizu",
                    Model = "17th",
                    Price = 100
                },
                new Phone
                {
                    Manufacturer = "Samsung",
                    Model = "Galaxy Note 20",
                    Price = 250
                },
                new Phone
                {
                    Manufacturer = "Oppo",
                    Model = "Ace 3",
                    Price = 250
                }
            };
            if(!_dbContext.Phones.Any())
            {
                _dbContext.Phones.AddRange(phones);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdatePhoneInfo(Phone phone)
        {
            _dbContext.Phones.Update(phone);
            await _dbContext.SaveChangesAsync();
        }
    }
}
