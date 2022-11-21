using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProSales.Domain.Identity;

namespace ProSales.Domain.Global
{
    public class ContactType
    {
        public long Id { get; set; }
        public Guid ExternalId { get; set; } = Guid.NewGuid();

        [Column(TypeName = "varchar(50)")]
        [StringLength(50)]
        public string TypeName { get; set; }
        public bool InternalProperty { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserCreatedId { get; set; }
        public User? UserCreated { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public User? UserUpdated { get; set; }

    }
}