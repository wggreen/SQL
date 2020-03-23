using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class FindEmployee
    {
        public static void CollectId()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Please enter the id of the employee you wish to view");
                Console.Write("> ");

                string response = Console.ReadLine();
                int id = Int32.Parse(response);

                EmployeeRepository employees = new EmployeeRepository();

                var foundEmployee = employees.GetEmployeeById(id);

                Console.WriteLine();

                Console.WriteLine($"{foundEmployee.FirstName} {foundEmployee.LastName}, Id: {foundEmployee.Id}, Department Id: {foundEmployee.DepartmentId}");

                Console.WriteLine();

                Console.WriteLine($"Press any key to return to the previous menu");
                Console.ReadLine();
                Console.Clear();
                break;

            }


        }
    }
}
