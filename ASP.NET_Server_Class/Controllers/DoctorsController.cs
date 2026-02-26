using ASP.NET_Server_Class.Models;
using ASP.NET_Server_Class.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorsService _doctorsService;
        private readonly DepartmentsService _departmentsService;
        private readonly DoctorsSpecializationsService _doctorsSpecializationsService;
        private readonly SpecializationsService _specializationsService;

        public DoctorsController(DoctorsService doctorsService, DepartmentsService departmentsService, DoctorsSpecializationsService doctorsSpecializationsService, SpecializationsService specializationsService)
        {
            _doctorsService = doctorsService;
            _departmentsService = departmentsService;
            _specializationsService = specializationsService;
            _doctorsSpecializationsService = doctorsSpecializationsService;
        }

        DoctorWithSpecializations getDoctor(int id)
        {
            Doctor doctor = _doctorsService.GetAll().Where(i => i.Id == id).FirstOrDefault();
            Department dep = _departmentsService.GetAll().Where(i => i.Id == doctor.DepartmentId).FirstOrDefault();
            List<DoctorsSpecializations> spec = _doctorsSpecializationsService.GetAll().Where(i => i.DoctorId == id).ToList();

            List<Specialization> specializations = new List<Specialization>();
            foreach (var item in spec)
            {
                specializations.Add(_specializationsService.GetAll().Where(i => i.Id == item.SpecializationId).FirstOrDefault());

            }

            DoctorWithSpecializations doc = new DoctorWithSpecializations(doctor, dep, specializations);
            return doc;
        }



        [HttpGet("doctors")]
        public ActionResult<List<Doctor>> GetDoctors()
        {
            return Ok(_doctorsService.GetAll());
        }
        [HttpGet("doctor/{id}")]
        public ActionResult<Doctor> GetDoctor(int id)
        {
            return Ok(getDoctor(id));
        }

        [HttpGet("paged/{page}")]
        public ActionResult<List<Doctor>> GetUsersPages(int page, int size = 2)
        {
            var doctors = _doctorsService.GetAll();

            if (page > doctors.Count / size || page < 1)
                return BadRequest();

            if (size < 1)
                return BadRequest();

            return Ok(new PagedResult<Doctor>()
            {
                Items = doctors.GetRange((page - 1) * size, size),
                TotalCount = doctors.Count,
                Page = page,
                PagesCount = doctors.Count / size,
                PageSize = size

            });
        }

        [HttpGet("departments")]
        public ActionResult<List<Department>> GetDepartments()
        {
            return Ok(_departmentsService.GetAll());
        }
        [HttpGet("doctorsSpecializations")]
        public ActionResult<List<DoctorsSpecializations>> GetDoctorsSpecializations()
        {
            return Ok(_doctorsSpecializationsService.GetAll());
        }
        [HttpGet("specializations")]
        public ActionResult<List<Specialization>> GetSpecializations()
        {
            return Ok(_specializationsService.GetAll());
        }



        [HttpPost("AddDepartments")]
        public ActionResult PostAddDepartments([FromBody] Department department)
        {
            _departmentsService.Add(department);
            return Ok();
        }
        [HttpPost("AddDoctors")]
        public ActionResult PostAddDoctors([FromBody] Doctor doctor)
        {
            _doctorsService.Add(doctor);
            return Ok();
        }
        [HttpPost("AddSpecialization")]
        public ActionResult PostAddSpecializations([FromBody] Specialization specialization)
        {
            _specializationsService.Add(specialization);
            return Ok();
        }
        [HttpPost("AddDoctorsSpecializations")]
        public ActionResult PostAddDoctorsSpecializations([FromBody] DoctorsSpecializations doctorsSpecializations)
        {
            _doctorsSpecializationsService.Add(doctorsSpecializations);
            return Ok();
        }



        [HttpPut("EditDepartments")]
        public ActionResult PutEditDepartments([FromBody] Department department)
        {
            _departmentsService.Update(department);
            return Ok();
        }
        [HttpPut("EditDoctors")]
        public ActionResult PutEditDoctors([FromBody] Doctor doctor)
        {
            _doctorsService.Update(doctor);
            return Ok();
        }
        [HttpPut("EditSpecialization")]
        public ActionResult PutEditSpecializations([FromBody] Specialization specialization)
        {
            _specializationsService.Update(specialization);
            return Ok();
        }
        [HttpPut("EditDoctorsSpecializations")]
        public ActionResult PutEditDoctorsSpecializations([FromBody] DoctorsSpecializations doctorsSpecializations)
        {
            _doctorsSpecializationsService.Update(doctorsSpecializations);
            return Ok();
        }



        [HttpDelete("DeleteDepartments")]
        public ActionResult DeleteDepartments([FromBody] int id)
        {
            _departmentsService.Delete(id);
            return Ok();
        }
        [HttpDelete("DeleteDoctors")]
        public ActionResult DeleteDoctors([FromBody] int id)
        {
            _doctorsService.Delete(id);
            return Ok();
        }
        [HttpDelete("DeleteSpecialization")]
        public ActionResult DeleteSpecializations([FromBody] int id)
        {
            _specializationsService.Delete(id);
            return Ok();
        }
        [HttpDelete("DeleteDoctorsSpecializations")]
        public ActionResult DeleteDoctorsSpecializations([FromBody] int id)
        {
            _doctorsSpecializationsService.Delete(id);
            return Ok();
        }



        [HttpGet("salary-above/{numb}")]
        public ActionResult<List<Doctor>> GetDoctorsSalary(int numb)
        {
            if (numb <= 0)
                return BadRequest();
            List<DoctorWithSpecializations> docs = new List<DoctorWithSpecializations>();
            _doctorsService.GetAll().Where(i => i.Salary > numb).ToList().ForEach(e => docs.Add(getDoctor(e.Id)));
            if (docs == null || docs.Count == 0 )
                return NotFound();
            return Ok(docs);
        }

        [HttpGet("by-department/{id}")]
        public ActionResult<List<Doctor>> GetDoctorsDep(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (_departmentsService.GetAll().Where(i => i.Id == id).FirstOrDefault() == null)
                return NotFound();
            List<DoctorWithSpecializations> docs = new List<DoctorWithSpecializations>();
            _doctorsService.GetAll().Where(i => i.DepartmentId == id).ToList().ForEach(e => docs.Add(getDoctor(e.Id)));
            if (docs == null || docs.Count == 0)
                return NotFound();
            return Ok(docs);
        }

        [HttpGet("by-specialization/{id}")]
        public ActionResult<List<Doctor>> GetDoctorsSpec(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (_specializationsService.GetAll().Where(i => i.Id == id).FirstOrDefault() == null)
                return NotFound();
            List<DoctorWithSpecializations> docs = new List<DoctorWithSpecializations>();
            _doctorsService.GetAll().ToList().ForEach(e => docs.Add(getDoctor(e.Id)));
            docs = docs.Where(i => i.Specializations.Contains(_specializationsService.GetAll().Where(s => s.Id == id).FirstOrDefault())).ToList();

            if (docs == null || docs.Count == 0)
                return NotFound();
            return Ok(docs);
        }

        [HttpGet("with-doctor-count")]
        public ActionResult<List<Specialization>> GetSpecializationsWithDoctorCount()
        {
            List<SpecializationWithDoctorCount> spec = new List<SpecializationWithDoctorCount>();
            _specializationsService.GetAll().ToList().ForEach(e => spec.Add(new SpecializationWithDoctorCount(_doctorsSpecializationsService.GetAll().Where(i => i.SpecializationId == e.Id).ToList().Count, e)));

            if (spec == null || spec.Count == 0)
                return NotFound();
            return Ok(spec);
        }

        double totalExpenses(Department e)
        {
            double total = 0;
            _doctorsService.GetAll().Where(i => i.DepartmentId == e.Id).ToList().ForEach(d => total += (d.Salary + d.Premium));
            return total;
        }
        double averageSalary(Department e)
        {
            double total = 0;
            _doctorsService.GetAll().Where(i => i.DepartmentId == e.Id).ToList().ForEach(d => total += d.Salary);
            return total / _doctorsService.GetAll().Where(i => i.DepartmentId == e.Id).ToList().Count;

        }

        [HttpGet("statistics")]
        public ActionResult<List<Doctor>> GetDepStatistics()
        {
            List<DepartmentStatistic> dep = new List<DepartmentStatistic>();
            _departmentsService.GetAll().ToList().ForEach(e => dep.Add(
                new DepartmentStatistic(
                    e,
                    totalExpenses(e),
                    averageSalary(e),
                    _doctorsService.GetAll().Where(i => i.DepartmentId == e.Id).ToList().Count)));

            if (dep == null || dep.Count == 0)
                return NotFound();
            Console.WriteLine(dep);
            return Ok(dep);
        }






    }
}
