using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class DepartmentList
    {
        public static void ListDepartments()
        {
            Console.Clear();
            while (true)
            {
                DepartmentRepository departmentRepo = new DepartmentRepository();

                Console.WriteLine("All Departments:");

                System.Collections.Generic.List<Department> allDepartments = departmentRepo.GetAllDepartments();

                foreach (Department dept in allDepartments)
                {
                    Console.WriteLine($"{dept.Id} {dept.DeptName}");
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
