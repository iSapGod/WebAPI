using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("DbPhones")]
    public class Phone : BaseEntity
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
    }
}
