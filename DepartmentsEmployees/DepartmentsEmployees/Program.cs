using System;

namespace DepartmentsEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Manage your business");
                Console.WriteLine("1. Manage Deparments");
                Console.WriteLine("2. Manage Employees");
                Console.WriteLine("3. Exit");

                Console.WriteLine();
                Console.WriteLine("Choose a menu option");
                Console.Write("> ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    ManageDepartments.ChooseDepartmentAction();
                }
                else if (option == "2")
                {
                    ManageEmployees.ChooseEmployeeAction();
                }
                else if (option == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid option: {option}");
                    Console.WriteLine($"");
                    Console.ReadLine();
                }
            }
        }
    }
}
