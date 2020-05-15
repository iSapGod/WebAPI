using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTestTask.Services
{
    public interface IPhoneService
    {
        Task<List<Phone>> GetAll();
        Task<Phone> GetPhoneById(Guid id);
        Task<List<Phone>> GetPhonesByManufacturer(string manufacturer);
        Task AddPhone(Phone phone);
        Task UpdatePhoneInfo(Phone phone);
        Task DeletePhone(Guid guid);
        Task DeletePhone(Phone phone);
        Task SeedData();
    }
}
