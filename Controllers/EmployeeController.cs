using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleMVCApps.Models;

namespace SampleMVCApps.Controllers{
    public class EmployeeController : Controller{
        public IActionResult Index()
        {
            return View(Repository.AllEmpoyees);
        }

        // HTTP GET VERSION
        public IActionResult Create()
        {
            return View();
        }
  
        // HTTP POST VERSION  
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid){
                Repository.Create(employee);
                return View("Thanks", employee);
            }else
                return View();
        }

        public IActionResult Update(string empname)
        {
            Employee employee = Repository.AllEmpoyees.Where(e => e.EmployeeName == empname).FirstOrDefault();
            return View(employee);
        }
  
        [HttpPost]
        public IActionResult Update(Employee employee, string empname)
        {
            if (ModelState.IsValid)
            {
                Repository.AllEmpoyees.Where(e => e.EmployeeName == empname).FirstOrDefault().Age = employee.Age;
                Repository.AllEmpoyees.Where(e => e.EmployeeName == empname).FirstOrDefault().Salary = employee.Salary;
                Repository.AllEmpoyees.Where(e => e.EmployeeName == empname).FirstOrDefault().Department = employee.Department;
                Repository.AllEmpoyees.Where(e => e.EmployeeName == empname).FirstOrDefault().Sex = employee.Sex;
                Repository.AllEmpoyees.Where(e => e.EmployeeName == empname).FirstOrDefault().EmployeeName = employee.EmployeeName;
  
                return RedirectToAction("Index");
            }
            else 
                return View();
            
        }

        [HttpPost]
        public IActionResult Delete(string empname)
        {
            Employee employee = Repository.AllEmpoyees.Where(e => e.EmployeeName == empname).FirstOrDefault();
            Repository.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}