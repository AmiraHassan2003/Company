
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDay2.Models.Context;
using MVCDay2.Models.Entities;
using MVCDay2.ViewModel;

namespace MVCDay2.Repository.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NewCompanyContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeRepository(NewCompanyContext context, IWebHostEnvironment webHostEnvironment) 

        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public List<Models.Entities.Employee> GetAll()
        {
            return context.employee.Include(E => E.Company).ToList();
        }

        public Models.Entities.Employee GetById(int id)
        {
            return context.employee.Include(E => E.Company).FirstOrDefault(E => E.Id == id);
        }

        public void AddEmployee(EmployeeViewModel vm)
        {
            Models.Entities.Employee emp = new Models.Entities.Employee();
            string fileName = UploadFile(vm);
            emp.Name = vm.Name;
            emp.Age = vm.Age;
            emp.Email = vm.Email;
            emp.Salary = vm.Salary;
            emp.Address = vm.Address;
            emp.Phone = vm.Phone;
            emp.CompanyId = vm.CompanyId;
            emp.Image = fileName;
            context.employee.Add(emp);
            context.SaveChanges();
        }

        private string UploadFile(EmployeeViewModel vm)
        {
            string fileName = null;

            try
            {
                if (vm.Image != null)
                {
                    // Ensure directory exists or create it
                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "image");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    // Generate a unique filename using a GUID
                    fileName = Guid.NewGuid().ToString() + "-" + vm.Image.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.Image.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle file upload exceptions if necessary
                throw new ApplicationException("File upload failed", ex);
            }

            return fileName;
        }

        public void DeleteEmployee(int id)
        {
            Models.Entities.Employee emp = context.employee.FirstOrDefault(E => E.Id == id);
            context.employee.Remove(emp);
            context.SaveChanges();
        }

        public void UpdateEmployee(Models.Entities.Employee vm, int id)
        {
            // Retrieve the employee by ID and include related Company information
            Models.Entities.Employee emp = context.employee.Include(e => e.Company).FirstOrDefault(e => e.Id == id);
            // Update employee properties with new values
            emp.Name = vm.Name;
            emp.Age = vm.Age;
            emp.Email = vm.Email;
            emp.Salary = vm.Salary;
            emp.Address = vm.Address;
            emp.Phone = vm.Phone;
            emp.CompanyId = vm.CompanyId;

            // Mark the entity as modified to update it in the database
            context.employee.Update(emp);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
