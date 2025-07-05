using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Models
{
    internal abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public abstract bool RequiresShipping { get; }
        public abstract bool IsExpired { get; }
        public abstract double GetWeight();

        protected Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public virtual bool IsAvailable(int requestedQuantity)
        {
            return !IsExpired && Quantity >= requestedQuantity;
        }

    }
}
