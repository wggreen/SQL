using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class FindDepartment
    {
        public static void CollectId()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Please enter the id of the department you wish to view");
                Console.Write("> ");

                string response = Console.ReadLine();
                int id = Int32.Parse(response);

                DepartmentRepository departments = new DepartmentRepository();

                var foundDepartment = departments.GetDepartmentById(id);

                Console.WriteLine();

                Console.WriteLine($"{foundDepartment.Id}: {foundDepartment.DeptName}");

                Console.WriteLine();

                Console.WriteLine($"Press any key to return to the previous menu");
                Console.ReadLine();
                Console.Clear();
                break;

            }


        }
    }
}
