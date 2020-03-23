using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class UpdateEmployee
    {
        public static void CollectInput()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Please enter the id of the employee you wish to update");
                Console.Write("> ");

                string response = Console.ReadLine();
                int id = Int32.Parse(response);

                EmployeeRepository employees = new EmployeeRepository();

                var foundEmployee = employees.GetEmployeeById(id);

                Console.Clear();

                Console.WriteLine($"{foundEmployee.FirstName} {foundEmployee.LastName}, Id: {foundEmployee.Id},  Department Id: {foundEmployee.DepartmentId}");

                Console.WriteLine();
                Console.WriteLine("Please enter a new first name for the employee");
                Console.Write(" >");

                string firstName = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Please enter a new last name for the employee");
                Console.Write(" >");

                string lastName = Console.ReadLine();

                Console.WriteLine("Please enter a new department Id for the employee");

                int deptId = Int32.Parse(Console.ReadLine());

                Employee newEmployee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DepartmentId = deptId
                };

                employees.UpdateEmployee(id, newEmployee);

                var updatedEmployee = employees.GetEmployeeById(id);

                Console.Clear();

                Console.WriteLine("Updated employee:");
                Console.WriteLine($"{updatedEmployee.Id}, {updatedEmployee.FirstName} {updatedEmployee.LastName}, Department Id: {updatedEmployee.DepartmentId}");

                Console.WriteLine($"Press any key to return to the previous menu");
                Console.ReadLine();
                Console.Clear();
                break;
            }
        }
    }
}
