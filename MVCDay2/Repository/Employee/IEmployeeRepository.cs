using Microsoft.AspNetCore.Mvc;
using MVCDay2.Models.Entities;
using MVCDay2.ViewModel;

namespace MVCDay2.Repository.Employee
{
    public interface IEmployeeRepository
    {
        public List<MVCDay2.Models.Entities.Employee> GetAll();
        public MVCDay2.Models.Entities.Employee GetById(int id);

        public void AddEmployee(EmployeeViewModel vm);
        public void UpdateEmployee(Models.Entities.Employee vm, int id);

        public void DeleteEmployee(int id);

    }
}
