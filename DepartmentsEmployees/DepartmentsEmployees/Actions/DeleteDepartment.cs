using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class DeleteDepartment
    {
        public static void CollectInput()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Please enter the id of the department you wish to delete");
                Console.Write("> ");

                string response = Console.ReadLine();
                int id = Int32.Parse(response);

                DepartmentRepository departments = new DepartmentRepository();

                var foundDepartment = departments.GetDepartmentById(id);

                Console.Clear();

                Console.WriteLine($"{foundDepartment.Id}: {foundDepartment.DeptName}");

                Console.WriteLine();

                Console.WriteLine("Are you sure you wish to delete? y/n");
                Console.Write(" >");

                string choice = Console.ReadLine();

                if (choice == "y")
                {
                    departments.DeleteDepartment(id);
                    Console.WriteLine();
                    Console.WriteLine("Department deleted");
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
