using Fawry_Task.Interfaces;
using Fawry_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Services
{
    internal class ShippingService : IShippingService
    {
        public void ShipItems(List<IShippable> items)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("** Shipment notice **");
            Console.ResetColor();

            Console.WriteLine($"Shipping {items.Count} items:");
            
            Dictionary<string, int> itemCounts = new Dictionary<string, int>();
            Dictionary<string, double> itemWeights = new Dictionary<string, double>();
            foreach (var item in items)
            {
                if (itemCounts.ContainsKey(item.GetName()))
                {
                    itemCounts[item.GetName()] += 1;
                }
                else
                {
                    itemCounts[item.GetName()] = 1;
                }
                itemWeights[item.GetName()] = item.GetWeight();
            }

            double totalWeight = 0;
            foreach (var item in itemCounts)
            {
                Console.WriteLine($"{item.Value}x {item.Key} {itemWeights[item.Key] * item.Value} kg");
                totalWeight += itemWeights[item.Key] * item.Value;
            }

            Console.WriteLine($"Total package weight {totalWeight} kg");
            Console.WriteLine();
        }
    }
}
