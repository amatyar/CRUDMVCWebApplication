using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CRUDMVCWebApplication.Models;

namespace CRUDMVCWebApplication.Data
{
    public class CRUDOperation
    {
        private SqlConnection CreateDBConnection() {
            string connectionString = ConfigurationSettings.AppSettings.Get("MyConnectionString");
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int AddEmployee(Employee employee)
        {
            int status =0;
            using (SqlConnection connection = CreateDBConnection())
            {
                // Create the Command and Parameter objects.
                
                SqlCommand command = new SqlCommand("insert_update_delete", connection);
                command.Parameters.AddWithValue("@EmployeeId", employee.Id);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@StatementType", "Insert");

                command.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    status = command.ExecuteNonQuery();
                    connection.Close();
                }
                
                catch (Exception ex)
                {
                    connection.Close();
                    throw ex;
                }
            }
            return status;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployees(int? id)
        {
            List<Employee> employees = new List<Employee>(); 
            using (SqlConnection connection = CreateDBConnection())
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("insert_update_delete", connection);
                command.Parameters.AddWithValue("@EmployeeId", id == null? 0 : id);
                command.Parameters.AddWithValue("@StatementType","Select");

                command.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                try { 
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                        foreach(DataRow row in dt.Rows)
                        {
                            Employee emp = new Employee();
                            emp.Id = Convert.ToInt32(row[0]);
                            emp.FirstName = row[1].ToString();
                            emp.LastName = row[2].ToString();
                            emp.EmailId = row[3].ToString();
                            emp.PhoneNumber = Convert.ToInt64(row[4]);
                            emp.Address = row[5].ToString();
                            emp.Department = row[6].ToString();
                            employees.Add(emp);
                        }
                    }

                }
                
                catch (Exception ex)
                {
                    connection.Close();
                    throw ex;
                }
                connection.Close();
            }
            return employees;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int UpdateEmployee(Employee employee)
        {
            List<Employee> emplyees = new List<Employee>();
            int status = 0;
            using (SqlConnection connection = CreateDBConnection())
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("insert_update_delete", connection);
                command.Parameters.AddWithValue("@EmployeeId", employee.Id);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@StatementType", "Update");

                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                try
                {
                    status = command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    connection.Close();
                    throw ex;
                }
                connection.Close();
            }
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int DeleteEmployee(int employeeId)
        {
            int status = 0;
            using (SqlConnection connection = CreateDBConnection())
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("insert_update_delete", connection);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                command.Parameters.AddWithValue("@StatementType", "Delete");
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    status = command.ExecuteNonQuery();
                    connection.Close();
                }

                catch (Exception ex)
                {
                    connection.Close();
                    throw ex;
                }
                
            }
            return status;
        }
    }
    
}

    

