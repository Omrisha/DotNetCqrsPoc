using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }

        public Product Clone()
        {
            return new Product
            {
                Id = Id,
                Name = Name,
                Description = Description,
                UnitPrice = UnitPrice,
                CurrentStock = CurrentStock
            };
        }
    }
}
