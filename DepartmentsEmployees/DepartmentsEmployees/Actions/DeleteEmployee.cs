using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class DeleteEmployee
    {
        public static void CollectInput()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Please enter the id of the employee you wish to delete");
                Console.Write("> ");

                string response = Console.ReadLine();
                int id = Int32.Parse(response);

                EmployeeRepository employees = new EmployeeRepository();

                var foundEmployee = employees.GetEmployeeById(id);

                Console.Clear();

                Console.WriteLine($"{foundEmployee.FirstName} {foundEmployee.LastName}, Id: {foundEmployee.Id}, Department Id: {foundEmployee.Department}");

                Console.WriteLine();

                Console.WriteLine("Are you sure you wish to delete? y/n");
                Console.Write(" >");

                string choice = Console.ReadLine();

                if (choice == "y")
                {
                    employees.DeleteEmployee(id);
                    Console.WriteLine();
                    Console.WriteLine("Employee deleted");
                    Console.WriteLine();

                    Console.WriteLine($"Press any key to return to the previous menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
                else if (choice == "n")
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }
}
