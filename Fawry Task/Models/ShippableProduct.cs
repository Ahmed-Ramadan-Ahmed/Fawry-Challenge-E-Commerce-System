using Fawry_Task.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Models
{
    // Non-expirable products that require shipping
    internal class ShippableProduct : Product, IShippable
    {
        public double Weight { get; set; }
        public override bool RequiresShipping => true;
        public override bool IsExpired => false;
        public override double GetWeight() => Weight;
        public string GetName() => Name;

        public ShippableProduct(string name, decimal price, int quantity, double weight)
            : base(name, price, quantity)
        {
            Weight = weight;
        }
    }
}
