using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Server_Class.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Premium { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DoctorsSpecializations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int SpecializationId { get; set; }
    }
    public class Specialization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //public class OrderWithProducts : Order
    //{
    //    public List<Product> Products { get; set; }
    //    public OrderWithProducts(Order order, List<Product> products)
    //    {
    //        Id = order.Id;
    //        CustomerId = order.CustomerId;
    //        TotalPrice = order.TotalPrice;
    //        Products = products;
    //    }
    //}
}
