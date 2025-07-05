using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Models
{
    // Digital products that don't require shipping
    internal class DigitalProduct: Product
    {
        public override bool RequiresShipping => false;
        public override bool IsExpired => false;
        public override double GetWeight() => 0;

        public DigitalProduct(string name, decimal price, int quantity)
            : base(name, price, quantity)
        {
        }
    }
}
