using Fawry_Task.Models;
using Fawry_Task.Services;

namespace Fawry_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create shipping service and e-commerce system
            var shippingService = new ShippingService();
            var calculateShippingFeesService = new CalculateShippingFeesService();
            decimal shippingRatePerKg = 5.0M;

            var ecommerceSystem = new ECommerceSystem(shippingService, calculateShippingFeesService , shippingRatePerKg);

            // Create products
            var cheese = new ExpirableProduct("Cheese", 15.99m, 10, DateTime.Now.AddDays(7), 0.5);
            var biscuits = new ExpirableProduct("Biscuits", 8.50m, 20, DateTime.Now.AddDays(-1), 0.3); // Expired
            var tv = new ShippableProduct("TV", 599.99m, 5, 15.0);
            var mobile = new ShippableProduct("Smartphone", 799.99m, 8, 0.2);
            var scratchCard = new DigitalProduct("Mobile Scratch Card", 10.00m, 100);

            // Create customers
            var customer1 = new Customer("Ahmed", 1000.00m);
            var customer2 = new Customer("Ramadan", 50.00m);
            var customer3 = new Customer("Mostafa", 2000.00m);
            var customer4 = new Customer("Ali", 500.00m);
            var customer5 = new Customer("Muhammed", 100.00m);

            Console.ForegroundColor = (ConsoleColor.Blue);
            Console.WriteLine("=== E-COMMERCE SYSTEM DEMO ===");
            Console.ResetColor();
            Console.WriteLine();

            /*********************************************************
                 Test Case 1: Successful checkout with mixed products
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Green);
            Console.WriteLine("TEST CASE 1: Successful checkout with mixed products");
            Console.ResetColor();

            try
            {
                customer1.AddToCart(cheese, 2);
                customer1.AddToCart(scratchCard, 3);
                customer1.AddToCart(mobile, 1);
                ecommerceSystem.Checkout(customer1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            /*********************************************************
                 Test Case 2: Insufficient balance
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Red);
            Console.WriteLine("TEST CASE 2: Insufficient balance");
            Console.ResetColor();

            try
            {
                customer2.AddToCart(tv, 1);
                ecommerceSystem.Checkout(customer2);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            /*********************************************************
                 Test Case 3: Empty cart
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Red);
            Console.WriteLine("TEST CASE 3: Empty cart checkout");
            Console.ResetColor();

            ecommerceSystem.Checkout(customer3);

            /*********************************************************
                Test Case 4: Expired product
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Red);
            Console.WriteLine("TEST CASE 4: Expired product");
            Console.ResetColor();

            try
            {
                customer3.AddToCart(biscuits, 2);
                ecommerceSystem.Checkout(customer3);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            /*********************************************************
                 Test Case 5: Out of stock
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Red);
            Console.WriteLine("TEST CASE 5: Out of stock product");
            Console.ResetColor();

            try
            {
                customer3.ClearCart();
                customer3.AddToCart(tv, 10); // Only 5 available
                ecommerceSystem.Checkout(customer3);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            /*********************************************************
                 Test Case 6: Adding more quantity than available
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Red);
            Console.WriteLine("TEST CASE 6: Adding more quantity than available to cart");
            Console.ResetColor();

            try
            {
                customer3.ClearCart();
                customer3.AddToCart(tv, 6); // Only 5 available
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            /*********************************************************
                 Test Case 7: Successful large order with shipping
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Green);
            Console.WriteLine("TEST CASE 7: Large order with heavy items");
            Console.ResetColor();

            try
            {
                customer3.ClearCart();
                customer3.AddToCart(tv, 2);
                customer3.AddToCart(cheese, 5);
                customer3.AddToCart(scratchCard, 10);
                ecommerceSystem.Checkout(customer3);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            /*********************************************************
                 Test Case 8: Adding same product multiple times
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Red);
            Console.WriteLine("TEST CASE 8: Adding same product multiple times");
            Console.ResetColor();
            try
            {
                customer4.AddToCart(cheese, 2);
                customer4.AddToCart(cheese, 3); // Should combine to 5
                Console.WriteLine($"Total cheese in cart: {customer4.Cart.First(item => item.Product.Name == "Aged Cheese").Quantity}");
                ecommerceSystem.Checkout(customer4);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            /*********************************************************
                TEST CASE 9: Adding Digital products only
             *********************************************************/

            Console.ForegroundColor = (ConsoleColor.Green);
            Console.WriteLine("TEST CASE 9: Digital-only order");
            Console.ResetColor();

            try
            {
                customer5.AddToCart(scratchCard, 8);
                ecommerceSystem.Checkout(customer5);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine();
            }

            Console.ForegroundColor = (ConsoleColor.Blue);
            Console.WriteLine("=== DEMO COMPLETED ===");
            Console.ResetColor();
        }
    }
}
