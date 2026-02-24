using System;

namespace Navchpract_2
{
    [Serializable] 
    internal class Product
    {
        public Product(string name, int quantity, float price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public override string ToString()
        {
            return $"{Name} | {Quantity} шт. | {Price:F2} грн.";
        }
    }
}