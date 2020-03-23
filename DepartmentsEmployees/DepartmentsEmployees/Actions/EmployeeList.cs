using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class EmployeeList
    {
        public static void ListEmployees()
        {
            Console.Clear();
            while (true)
            {
                EmployeeRepository employees = new EmployeeRepository();

                Console.WriteLine("All Employees:");

                System.Collections.Generic.List<Employee> allEmployees = employees.GetAllEmployees();

                foreach (Employee emp in allEmployees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName}, Id: {emp.Id}, Department Id: {emp.DepartmentId}");
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
