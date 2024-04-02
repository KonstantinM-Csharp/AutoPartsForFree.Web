using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMaster.Entities
{
    public class PriceItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(64)]
        public string Vendor { get; set; }

        [StringLength(64)]
        public string Number { get; set; }

        [StringLength(64)]
        public string SearchVendor { get; set; }

        [StringLength(64)]
        public string SearchNumber { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [Range(0, 9999999999999999.99)] // Ограничение на диапазон цены
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)] // Ограничение на диапазон количества
        public int Count { get; set; }
    }
}
