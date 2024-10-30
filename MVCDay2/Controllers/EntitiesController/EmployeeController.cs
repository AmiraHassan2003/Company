using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDay2.Models.Context;
using MVCDay2.Models.Entities;
using MVCDay2.Repository.Employee;
using MVCDay2.ViewModel;


namespace MVCDay2.Controllers.EntitiesController
{
    public class EmployeeController : Controller
    {
        NewCompanyContext context;
        IEmployeeRepository employeeRepo;
        public EmployeeController(NewCompanyContext context, IEmployeeRepository employeeRepo) { 
            this.context = context;
            this.employeeRepo = employeeRepo;
        }
        public IActionResult GetAll()
        {
            var getAllEmployees = employeeRepo.GetAll();
            return View("GetAll", getAllEmployees);
        }

        public IActionResult GetById(int id)
        {
            var getEmployee = employeeRepo.GetById(id);
            return View("GetById" ,getEmployee);
        }

        public IActionResult Create()
        {
            ViewBag.Companies = context.company.ToList();
            EmployeeViewModel empView = new EmployeeViewModel();

            return View(empView);
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeViewModel vm)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    employeeRepo.AddEmployee(vm);
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("Create");
        }

        public ActionResult DeleteEmployee(int id) 
        {
            employeeRepo.DeleteEmployee(id);
            return RedirectToAction("GetAll");
        }

      
        public IActionResult EditEmployee(int Id)
        {
            Employee emp = employeeRepo.GetById(Id);
            Employee empView = new Employee
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Email = emp.Email,
                Salary = emp.Salary,
                Address = emp.Address,
                Phone = emp.Phone,
                Image = emp.Image,
                CompanyId = emp.CompanyId,
            };
            ViewBag.Companies = context.company.ToList();
            return View("EditEmployee", empView);
        }



        [HttpPost]
        public IActionResult SaveEmployee(Models.Entities.Employee vm, int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    employeeRepo.UpdateEmployee(vm, Id);
                    return RedirectToAction("GetById", new { id = Id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("EditEmployee", new { id = Id });
        }


    }
}
