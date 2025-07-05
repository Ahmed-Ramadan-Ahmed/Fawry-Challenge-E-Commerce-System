using Fawry_Task.Interfaces;
using Fawry_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Services
{
    internal class CalculateShippingFeesService : ICalculateShippingFees
    {
        public decimal CalculateShippingFees(List<CartItem> cartItems, decimal shippingRatePerKg = 5.0m)
        {

            if (cartItems == null || cartItems.Count == 0)
            {
                return 0.0m; // No items to ship
            }
            if(shippingRatePerKg <= 0)
            {
                throw new ArgumentException("Shipping rate must be positive", nameof(shippingRatePerKg));
            }

            double totalWeight = cartItems
                .Where(item => item.Product.RequiresShipping)
                .Sum(item => item.Product.GetWeight() * item.Quantity);

            return (decimal)totalWeight * shippingRatePerKg;
        }
    }
}
