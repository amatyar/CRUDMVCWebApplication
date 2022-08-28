using System;

namespace CRUDMVCWebApplication.Models
{
    public class Employee
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public string EmailId;
        public long PhoneNumber;
        public string Address;
        public string Department;

        public Employee GetRandomEmployee()
        {
            Employee emp = new Employee();
            int randnum = new Random().Next(1, 99);
            emp.FirstName = FirstName + randnum.ToString();
            emp.LastName = LastName + randnum.ToString();
            emp.EmailId = EmailId + randnum.ToString() + "@gmail.com";
            emp.PhoneNumber = PhoneNumber;
            emp.Address = Address + randnum.ToString();
            emp.Department = Department + randnum.ToString();

            return emp;
        }
    }
}
