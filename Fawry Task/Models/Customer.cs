using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Models
{
    internal class Customer
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<CartItem> Cart { get; set; }

        public Customer(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
            Cart = new List<CartItem>();
        }

        public void AddToCart(Product product, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            if (!product.IsAvailable(quantity))
                throw new InvalidOperationException($"Product {product.Name} is not available in requested quantity");

            var existingItem = Cart.FirstOrDefault(item => item.Product == product);
            if (existingItem != null)
            {
                int newQuantity = existingItem.Quantity + quantity;
                if (!product.IsAvailable(newQuantity))
                    throw new InvalidOperationException($"Not enough stock for {product.Name}");
                existingItem.Quantity = newQuantity;
            }
            else
            {
                Cart.Add(new CartItem(product, quantity));
            }
        }

        public void ClearCart()
        {
            Cart.Clear();
        }
    }
}
