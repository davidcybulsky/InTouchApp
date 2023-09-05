using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTouchApi.Domain.Entities
{
    public class Address : BaseAuditableEntity
    {
        public int UserId { get; set; }

        public int LocalNumber { get; set; }

        public int BuildingNumber { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }

        [MaxLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string ZipCode { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Region { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }
    }
}
