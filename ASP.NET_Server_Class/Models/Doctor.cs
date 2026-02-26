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

    public class DoctorWithSpecializations : Doctor
    {
        public List<Specialization> Specializations { get; set; }
        public Department Department { get; set; }
        public DoctorWithSpecializations(Doctor doc, Department department, List<Specialization> specializations)
        {
            this.Department = department;
            this.Specializations = specializations;
            this.Salary = doc.Salary;
            this.Surname = doc.Surname;
            this.Premium = doc.Premium;
            this.Name = doc.Name;
            this.Id = doc.Id;
            this.DepartmentId = doc.DepartmentId;

        }
    }

    public class SpecializationWithDoctorCount : Specialization
    {
        public int doctorsCount { get; set; }
        public SpecializationWithDoctorCount(int count, Specialization specialization)
        {
            this.doctorsCount = count;
            this.Name = specialization.Name;
            this.Id = specialization.Id;
        }
    }

    public class DepartmentStatistic : Department
    {
        public double totalExpenses { get; set; }
        public double averageSalary { get; set; }
        public int doctorCount { get; set; }
        public DepartmentStatistic(Department dep, double totalExpenses, double averageSalary, int doctorCount)
        {
            this.averageSalary = averageSalary;
            this.doctorCount = doctorCount;
            this.totalExpenses = totalExpenses;
            this.Id= dep.Id;
            this.Name= dep.Name;
        }
    }
}
