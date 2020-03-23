using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class AddDepartment
    {
        public static void CollectInput()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Please enter the name of the new department");
                Console.Write("> ");

                string deptName = Console.ReadLine();

                Department newDepartment = new Department
                {
                    DeptName = deptName
                };

                DepartmentRepository departments = new DepartmentRepository();

                departments.AddDepartment(newDepartment);

                Console.WriteLine("Successfully added the new department");
                Console.WriteLine();

                Console.WriteLine($"Press any key to return to the previous menu");
                Console.ReadLine();
                Console.Clear();
                break;

            }
        }
    }
}
