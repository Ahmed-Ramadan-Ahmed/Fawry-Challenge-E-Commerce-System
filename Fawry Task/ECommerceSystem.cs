using Fawry_Task.Interfaces;
using Fawry_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task
{
    internal class ECommerceSystem
    {
        private readonly IShippingService _shippingService;
        private readonly ICalculateShippingFees _calculateShippingFeesService;
        private readonly decimal _shippingRatePerKg;
        public ECommerceSystem(
            IShippingService shippingService,
            ICalculateShippingFees calculateShippingFees,
            decimal shippingRatePerKg = 5.0m)
        {
            _shippingService = shippingService;
            _shippingRatePerKg = shippingRatePerKg;
            _calculateShippingFeesService = calculateShippingFees;
        }

        public void Checkout(Customer customer)
        {
            Console.ForegroundColor = (ConsoleColor.Cyan);
            Console.WriteLine($"=== CHECKOUT FOR {customer.Name.ToUpper()} ===");
            Console.ResetColor();

            if (customer.Cart.Count == 0)
            {
                Console.WriteLine("ERROR: Cart is empty!");
                Console.WriteLine();
                return;
            }

            // Validate stock availability and expiration
            foreach (var cartItem in customer.Cart)
            {
                if (!cartItem.Product.IsAvailable(cartItem.Quantity))
                {
                    if (cartItem.Product.IsExpired)
                    {
                        Console.WriteLine($"ERROR: Product '{cartItem.Product.Name}' has expired!");
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Product '{cartItem.Product.Name}' is out of stock! Available: {cartItem.Product.Quantity}, Requested: {cartItem.Quantity}");
                    }
                    Console.WriteLine();
                    return;
                }
            }

            // Calculate totals
            decimal subtotal = customer.Cart.Sum(item => item.TotalPrice);
            decimal shippingFees = _calculateShippingFeesService.CalculateShippingFees(customer.Cart, _shippingRatePerKg);
            decimal totalAmount = subtotal + shippingFees;

            // Validate customer balance
            if (customer.Balance < totalAmount)
            {
                Console.WriteLine($"ERROR: Insufficient balance! Required: ${totalAmount:F2}, Available: ${customer.Balance:F2}");
                Console.WriteLine();
                return;
            }

            customer.Balance -= totalAmount;
            
            foreach (var cartItem in customer.Cart)
            {
                cartItem.Product.Quantity -= cartItem.Quantity;
            }

            // Print checkout details
            Console.WriteLine("ORDER SUMMARY:");
            foreach (var cartItem in customer.Cart)
            {
                Console.WriteLine($"{cartItem.Quantity}x {cartItem.Product.Name} = ${cartItem.TotalPrice:F2}");
            }
            Console.WriteLine($"Subtotal: ${subtotal:F2}");
            Console.WriteLine($"Shipping: ${shippingFees:F2}");
            Console.WriteLine($"Amount: ${totalAmount:F2}");
            Console.WriteLine($"Customer Balance After Payment: ${customer.Balance:F2}");
            Console.WriteLine();

            var shippableItems = customer.Cart
                .Where(item => item.Product.RequiresShipping)
                .SelectMany(item => Enumerable.Repeat((IShippable)item.Product, item.Quantity))
                .ToList();

            if (shippableItems.Count > 0)
            {
                _shippingService.ShipItems(shippableItems);
            }

            customer.ClearCart();
        }
    }
}
