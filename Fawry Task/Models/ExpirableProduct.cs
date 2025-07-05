using Fawry_Task.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task.Models
{
    // products that may got expired 
    internal class ExpirableProduct : Product, IShippable
    {
        public DateTime ExpirationDate { get; set; }
        public double Weight { get; set; }
        public override bool RequiresShipping => true;
        public override bool IsExpired => DateTime.Now > ExpirationDate;
        public override double GetWeight() => Weight;
        public string GetName() => Name;

        public ExpirableProduct(string name, decimal price, int quantity, DateTime expirationDate, double weight)
            : base(name, price, quantity)
        {
            ExpirationDate = expirationDate;
            Weight = weight;
        }
    }
}
