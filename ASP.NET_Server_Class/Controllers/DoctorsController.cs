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


        [HttpGet("doctorsWhereSalaryMoreThan/{numb}")]
        public ActionResult<List<Doctor>> GetDoctorsSalary(int numb)
        {
            List<DoctorWithSpecializations> docs = new List<DoctorWithSpecializations>();
            _doctorsService.GetAll().Where(i => i.Salary > numb).ToList().ForEach(e => docs.Add(getDoctor(e.Id)));
            return Ok(docs);
        }

        [HttpGet("doctorsWhereDepartment/{id}")]
        public ActionResult<List<Doctor>> GetDoctorsDep(int id)
        {
            List<DoctorWithSpecializations> docs = new List<DoctorWithSpecializations>();
            _doctorsService.GetAll().Where(i => i.DepartmentId == id).ToList().ForEach(e => docs.Add(getDoctor(e.Id)));
            return Ok(docs);
        }

        [HttpGet("doctorsWhereSpecializations/{id}")]
        public ActionResult<List<Doctor>> GetDoctorsSpec(int id)
        {
            List<DoctorWithSpecializations> docs = new List<DoctorWithSpecializations>();
            _doctorsService.GetAll().ToList().ForEach(e => docs.Add(getDoctor(e.Id)));
            var newdoc = docs.Where(i => i.Specializations.Contains(_specializationsService.GetAll().Where(s => s.Id == id).FirstOrDefault()));
            return Ok(newdoc);
        }


        



        //[HttpGet("orders")]
        //public ActionResult<List<Order>> GetOrders()
        //{
        //    return Ok(_orderService.GetAll());
        //}
        //[HttpGet("productsInOrder/{id}")]
        //public ActionResult<OrderWithProducts> GetproductsInOrder(int id)
        //{
        //    if (_orderService.GetAll().Where(i => i.Id == id) == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(new OrderWithProducts(
        //        _orderService.GetAll().Where(i => i.Id == id).FirstOrDefault(),
        //        _productService.GetByIds(
        //            _productsInOrdersService.GetAll().Where(i => i.OrderId == id).ToArray()
        //            )
        //        )
        //    );
        //}
        //[HttpPost("AddProduct")]
        //public ActionResult AddProduct([FromBody] Product product)
        //{
        //    _productService.Add(product);
        //    return Ok();
        //}
        //[HttpPost("AddOrder")]
        //public ActionResult AddOrder([FromBody] Order order)
        //{
        //    if (_userService.GetAll().Where(i => i.Id == order.CustomerId) == null)
        //    {
        //        return BadRequest();
        //    }

        //    _orderService.Add(order);
        //    return Ok();
        //}
        //[HttpPost("AddProductToOrder")]
        //public ActionResult AddProductToOrder([FromBody] ProductsInOrder productsInOrder)
        //{
        //    if (_productService.GetAll().Where(i => i.Id == productsInOrder.ProductId).FirstOrDefault() != null && _orderService.GetAll().Where(i => i.Id == productsInOrder.OrderId).FirstOrDefault() != null)
        //    {
        //        _productsInOrdersService.Add(productsInOrder);
        //        _orderService.GetAll().Where(i => i.Id == productsInOrder.OrderId).FirstOrDefault().TotalPrice += _productService.GetAll().Where(i => i.Id == productsInOrder.ProductId).FirstOrDefault().Price;
        //        _productService.GetAll().Where(i => i.Id == productsInOrder.ProductId).FirstOrDefault().Stock -= 1;
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
