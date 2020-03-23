using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class AddEmployee
    {
        public static void CollectInput()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Please enter the first name of the new employee");
                Console.Write("> ");

                string firstName = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Please enter the last name of the new employee");
                Console.Write("> ");

                string lastName = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Please enter the departmentId of the new employee");
                Console.Write("> ");

                int deptId = Int32.Parse(Console.ReadLine());

                Employee newEmployee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DepartmentId = deptId
                };

                EmployeeRepository employees = new EmployeeRepository();

                employees.AddEmployee(newEmployee);

                Console.WriteLine("Successfully added the new employee");
                Console.WriteLine();

                Console.WriteLine($"Press any key to return to the previous menu");
                Console.ReadLine();
                Console.Clear();
                break;

            }
        }
    }
}
