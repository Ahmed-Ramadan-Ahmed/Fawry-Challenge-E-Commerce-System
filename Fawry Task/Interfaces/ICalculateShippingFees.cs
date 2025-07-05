using Fawry_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Interfaces
{
    internal interface ICalculateShippingFees
    {
        public decimal CalculateShippingFees(List<CartItem> cartItems, decimal shippingRatePerKg = 5.0m);
    }
}
