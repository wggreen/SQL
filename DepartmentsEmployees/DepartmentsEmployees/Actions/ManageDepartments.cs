using System;

namespace DepartmentsEmployees
{
    public class ManageDepartments
    {
        public static void ChooseDepartmentAction()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Manage your departments");
                Console.WriteLine("1. View all departments");
                Console.WriteLine("2. Get department by Id");
                Console.WriteLine("3. Add department");
                Console.WriteLine("4. Update department");
                Console.WriteLine("5. Delete department");
                Console.WriteLine("6. Main menu");
                Console.WriteLine();

                Console.WriteLine("Choose a menu option");
                Console.Write("> ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    DepartmentList.ListDepartments();
                }
                if (option == "2")
                {
                    FindDepartment.CollectId();
                }
                if (option == "3")
                {
                    AddDepartment.CollectInput();
                }
                if (option == "4")
                {
                    UpdateDepartment.CollectInput();
                }
                if (option == "5")
                {
                    DeleteDepartment.CollectInput();
                }
                if (option == "6")
                {
                    Console.Clear();
                    break;
                }

            }
        }
    }
}
