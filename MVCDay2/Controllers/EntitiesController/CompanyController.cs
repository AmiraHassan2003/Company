using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDay2.Models.Context;
using MVCDay2.Models.Entities;

namespace MVCDay2.Controllers.EntitiesController
{
    public class CompanyController : Controller
    {
        NewCompanyContext context;
        public CompanyController(NewCompanyContext context)
        {
            this.context = context;
        }
        public IActionResult GetAll()
        {
            var getAllCompany = context.company.Include(C=>C.employees).ToList(); 
            return View("GetAll", getAllCompany);
        }

        public IActionResult GetById(int id) {
            var getCompany = context.company.Include(C=>C.employees).FirstOrDefault(C=>C.Id == id);
            return View("GetById",getCompany);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AddCompany(string Name, string Type, DateTime DateStart, string Address)
        {
            Company company = new Company();
            company.Name = Name;
            company.Type = Type;
            company.DateStart = DateStart;
            company.Address = Address;
            context.company.Add(company);
            context.SaveChanges();
            return RedirectToAction("GetAll");
        }

        public IActionResult EditCompany(int id)
        {
            Company company = context.company.FirstOrDefault(C=>C.Id == id);
            return View(company);
        }

        public IActionResult SaveCompany(int Id, Company NewCompany)
        {
            Company oldCompany = context.company.FirstOrDefault(C=>C.Id == Id);
            oldCompany.Name = NewCompany.Name;
            oldCompany.Type = NewCompany.Type;
            oldCompany.DateStart = NewCompany.DateStart;
            oldCompany.Address = NewCompany.Address;
            context.company.Update(oldCompany);
            context.SaveChanges();
            return RedirectToAction("GetById", new { id = Id });
        }

        public IActionResult DeleteCompany(int Id) 
        { 
            Company company = context.company.FirstOrDefault(C=>C.Id == Id);
            //if (company.employees != null)
            //{
            //    context.employee.RemoveRange(company.employees);
            //    context.SaveChanges();
            //}
            context.company.Remove(company);
            context.SaveChanges();

            return RedirectToAction("GetAll");
        }
    }
}
