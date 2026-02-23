using ASP.NET_Server_Class.Models;

namespace ASP.NET_Server_Class.Services
{
    public class DoctorsService
    {
        public readonly UsersDbContext _context;

        public DoctorsService(UsersDbContext context)
        {
            _context = context;
        }
        public List<Doctor> GetAll() => _context.Doctors.ToList();

        public void Add(Doctor Doctor)
        {
            _context.Doctors.Add(Doctor);
            _context.SaveChanges();
        }
        
        //public List<Product> GetByIds(ProductsInOrder[]? ids)
        //{
        //    List<Product> responce = new List<Product>();
        //    foreach (ProductsInOrder id in ids)
        //    {
        //        responce.Add(_context.Products.Where(i => i.Id == id.ProductId).FirstOrDefault());
        //    }
        //    return responce;
        //}
    }
    public class DepartmentsService
    {
        public readonly UsersDbContext _context;

        public DepartmentsService(UsersDbContext context)
        {
            _context = context;
        }
        public List<Department> GetAll() => _context.Departments.ToList();

        public void Add(Department Department)
        {
            _context.Departments.Add(Department);
            _context.SaveChanges();
        }
        
    }

    public class DoctorsSpecializationsService
    {
        public readonly UsersDbContext _context;

        public DoctorsSpecializationsService(UsersDbContext context)
        {
            _context = context;
        }
        public List<DoctorsSpecializations> GetAll() => _context.DoctorsSpecializations.ToList();

        public void Add(DoctorsSpecializations DoctorsSpecializations)
        {
            _context.DoctorsSpecializations.Add(DoctorsSpecializations);
            _context.SaveChanges();
        }
    }
    public class SpecializationsService
    {
        public readonly UsersDbContext _context;

        public SpecializationsService(UsersDbContext context)
        {
            _context = context;
        }
        public List<Specialization> GetAll() => _context.Specializations.ToList();

        public void Add(Specialization Specialization)
        {
            _context.Specializations.Add(Specialization);
            _context.SaveChanges();
        }
    }

}

