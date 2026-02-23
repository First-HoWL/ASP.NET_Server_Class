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

        public void Update(Doctor Doctor)
        {
            Doctor foundDoc = _context.Doctors.Where(d => d.Id == Doctor.Id).FirstOrDefault();
            foundDoc.Name = Doctor.Name;
            foundDoc.Salary = Doctor.Salary;
            foundDoc.Surname = Doctor.Surname;
            foundDoc.Premium = Doctor.Premium;
            foundDoc.DepartmentId = Doctor.DepartmentId;

        }
        public bool Delete(int id)
        {
            Doctor foundDoc = _context.Doctors.Where(d => d.Id == id).FirstOrDefault();
            if (foundDoc != null)
            {
                _context.Doctors.Remove(foundDoc);
                return true;
            }
            else
            {
                return false;
            }

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

        public void Update(Department Department)
        {
            Department foundDep = _context.Departments.Where(d => d.Id == Department.Id).FirstOrDefault();
            foundDep.Name = Department.Name;

        }
        public bool Delete(int id)
        {
            Department foundDep = _context.Departments.Where(d => d.Id == id).FirstOrDefault();
            if (foundDep != null)
            {
                _context.Departments.Remove(foundDep);
                return true;
            }
            else
            {
                return false;
            }

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

        public void Update(DoctorsSpecializations DoctorsSpecializations)
        {
            DoctorsSpecializations foundDoctorsSpecializations = _context.DoctorsSpecializations.Where(d => d.Id == DoctorsSpecializations.Id).FirstOrDefault();
            foundDoctorsSpecializations.DoctorId = DoctorsSpecializations.DoctorId;
            foundDoctorsSpecializations.SpecializationId = DoctorsSpecializations.SpecializationId;

        }
        public bool Delete(int id)
        {
            DoctorsSpecializations foundDoctorsSpecializations = _context.DoctorsSpecializations.Where(d => d.Id == id).FirstOrDefault();

            if (foundDoctorsSpecializations != null)
            {
                _context.DoctorsSpecializations.Remove(foundDoctorsSpecializations);
                return true;
            }
            else
            {
                return false;
            }

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

        public void Update(Specialization Specialization)
        {
            Specialization foundSpecialization = _context.Specializations.Where(d => d.Id == Specialization.Id).FirstOrDefault();
            foundSpecialization.Name = Specialization.Name;

        }
        public bool Delete(int id)
        {
            Specialization foundSpecialization = _context.Specializations.Where(d => d.Id == id).FirstOrDefault();

            if (foundSpecialization != null)
            {
                _context.Specializations.Remove(foundSpecialization);
                return true;
            }
            else
            {
                return false;
            }

        }

    }

}

