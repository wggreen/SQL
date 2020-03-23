using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class EmployeeListByDepartment
    {
        public static void ListEmployeesWithDepartment()
        {
            Console.Clear();
            while (true)
            {
                EmployeeRepository employees = new EmployeeRepository();

                Console.WriteLine("All Employees:");

                System.Collections.Generic.List<Employee> allEmployees = employees.GetAllEmployeesWithDepartment();

                foreach (Employee emp in allEmployees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} is in { emp.Department.DeptName} ");
                }

                Console.WriteLine();
                Console.WriteLine($"Press any key to return to the previous menu");
                Console.ReadLine();
                Console.Clear();
                break;
            }
        }
    }
}
