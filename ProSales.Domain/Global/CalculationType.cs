using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProSales.Domain.Identity;

namespace ProSales.Domain.Global
{
    public class CalculationType
    {
        public long Id { get; set; }
        public Guid ExternalId { get; set; } = Guid.NewGuid();

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        public bool InternalProperty { get; set; } = false;
    }
}