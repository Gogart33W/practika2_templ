using System;

namespace Navchpract_2
{
    [Serializable]
    public class RealEstate
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Address { get; set; }
        public string Type { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }
        public decimal Price { get; set; }
        public string OwnerName { get; set; }

        public string ToTxtString() => $"{Id}|{Address}|{Type}|{Area}|{Rooms}|{Price}|{OwnerName}";

        public static RealEstate FromTxtString(string line)
        {
            try
            {
                var p = line.Split('|');
                if (p.Length < 7) return null;
                return new RealEstate
                {
                    Id = Guid.Parse(p[0]),
                    Address = p[1],
                    Type = p[2],
                    Area = double.Parse(p[3]),
                    Rooms = int.Parse(p[4]),
                    Price = decimal.Parse(p[5]),
                    OwnerName = p[6]
                };
            }
            catch { return null; }
        }
    }
}