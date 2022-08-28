using CRUDMVCWebApplication.Models;
using CRUDMVCWebApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
//using PagedList;
//using PagedList.Mvc;


namespace CRUDMVCWebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        //paging
                                   
        public ActionResult Index(string SortingOrder,string currentFilter, string searchString, int? page)
        {
            CRUDOperation operation = new CRUDOperation();
            IList<Employee> employeeList = operation.GetEmployees(null);
            ViewBag.CurrentSort = SortingOrder;
            ViewBag.SortingName = String.IsNullOrEmpty(SortingOrder) ? "Name_Description" : "";
            ViewBag.SortingDepartment = String.IsNullOrEmpty(SortingOrder) ? "Department_Description" : "";
            
            if(searchString !=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            // for Sorting
            var Employee = from s in employeeList
                           select s;
            if(!String.IsNullOrEmpty(searchString))
            {
                Employee = Employee.Where(s => s.FirstName.Contains(searchString)
                || s.LastName.Contains(searchString));
            }


            switch(SortingOrder)
            {
                case "Name_Description":
                    Employee = Employee.OrderByDescending(s => s.FirstName);
                    break;

                case "Department_Description":
                    Employee = Employee.OrderByDescending(s => s.Department);
                    break;

                default:
                    Employee = Employee.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);



            return View(Employee.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            CRUDOperation operation = new CRUDOperation();
            try
            {
                // TODO: Add update logic here
                Employee emp = new Employee();
                emp.Id = 0;
                emp.FirstName = collection.GetValues("FirstName").FirstOrDefault();
                emp.LastName = collection.GetValues("LastName").FirstOrDefault();
                emp.EmailId = collection.GetValues("EmailId").FirstOrDefault();
                emp.PhoneNumber = Convert.ToInt64(collection.GetValues("PhoneNumber").FirstOrDefault());
                emp.Address = collection.GetValues("Address").FirstOrDefault();
                emp.Department = collection.GetValues("Department").FirstOrDefault();

                int status = operation.AddEmployee(emp);
                    if (status == 1)
                    return RedirectToAction("Index");
                else
                    return Content("Error occured");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            CRUDOperation operation = new CRUDOperation();
            Employee employee = operation.GetEmployees(id).First();

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            CRUDOperation operation = new CRUDOperation();
            try
            {
                // TODO: Add update logic here
                Employee emp = new Employee();
                emp.Id = id;
                emp.FirstName = collection.GetValues("FirstName").FirstOrDefault();
                emp.LastName = collection.GetValues("LastName").FirstOrDefault();
                emp.EmailId = collection.GetValues("EmailId").FirstOrDefault();
                emp.PhoneNumber = Convert.ToInt64(collection.GetValues("PhoneNumber").FirstOrDefault());
                emp.Address = collection.GetValues("Address").FirstOrDefault();
                emp.Department = collection.GetValues("Department").FirstOrDefault();
                
                int status = operation.UpdateEmployee(emp);
                if (status == 1)
                    return RedirectToAction("Index");
                else
                    return Content("Error occured");
            }
            catch
            {
                //return View();
            }
            return View();
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            CRUDOperation operation = new CRUDOperation();
            Employee employee = operation.GetEmployees(id).First();
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                CRUDOperation operation = new CRUDOperation();
                int status = operation.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
