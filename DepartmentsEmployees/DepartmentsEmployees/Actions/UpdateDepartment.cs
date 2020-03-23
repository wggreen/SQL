using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class UpdateDepartment
    {
        public static void CollectInput()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Please enter the id of the department you wish to update");
                Console.Write("> ");

                string response = Console.ReadLine();
                int id = Int32.Parse(response);

                DepartmentRepository departments = new DepartmentRepository();

                var foundDepartment = departments.GetDepartmentById(id);

                Console.Clear();

                Console.WriteLine($"{foundDepartment.Id}: {foundDepartment.DeptName}");

                Console.WriteLine();

                Console.WriteLine("Please enter a new name for the department");
                Console.Write(" >");

                string deptName = Console.ReadLine();

                Department newDepartment = new Department
                {
                    DeptName = deptName
                };

                departments.UpdateDepartment(id, newDepartment);

                var updatedDepartment = departments.GetDepartmentById(id);

                Console.Clear();

                Console.WriteLine("Updated department:");
                Console.WriteLine($"{updatedDepartment.Id}: {updatedDepartment.DeptName}");

                Console.WriteLine($"Press any key to return to the previous menu");
                Console.ReadLine();
                Console.Clear();
                break;
            }
        }
    }
}
