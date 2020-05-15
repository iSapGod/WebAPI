using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiTestTask.Models
{
    public class PhoneModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        public double Price { get; set; }
    }
}
