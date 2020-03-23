using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentsEmployees
{
    public class ManageEmployees
    {
        public static void ChooseEmployeeAction()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Manage your employees");
                Console.WriteLine("1. View all employees");
                Console.WriteLine("2. View all employees by department");
                Console.WriteLine("3. Get employee by Id");
                Console.WriteLine("4. Add employee");
                Console.WriteLine("5. Update employee");
                Console.WriteLine("6. Delete employee");
                Console.WriteLine("7. Main menu");
                Console.WriteLine();

                Console.WriteLine("Choose a menu option");
                Console.Write("> ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    EmployeeList.ListEmployees();
                }
                if (option == "2")
                {
                    EmployeeListByDepartment.ListEmployeesWithDepartment();
                }
                if (option == "3")
                {
                    FindEmployee.CollectId();
                }
                if (option == "4")
                {
                    AddEmployee.CollectInput();
                }
                if (option == "5")
                {
                    UpdateEmployee.CollectInput();
                }
                if (option == "6")
                {
                    DeleteEmployee.CollectInput();
                }
                if (option == "7")
                {
                    Console.Clear();
                    break;
                }

            }
        }
    }
}
