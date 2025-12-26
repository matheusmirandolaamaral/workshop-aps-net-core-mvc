namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public DateTime BithDate { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string email, string name, double baseSalary, DateTime bithDate, Department department)
        {
            Id = id;
            Email = email;
            Name = name;
            BaseSalary = baseSalary;
            BithDate = bithDate;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
