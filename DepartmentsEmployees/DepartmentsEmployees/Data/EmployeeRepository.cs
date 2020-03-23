using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace DepartmentsEmployees
{
    public class EmployeeRepository
    {
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DepartmentsEmployees;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            //  We must "use" the database connection.
            //  Because a database is a shared resource (other applications may be using it too) we must
            //  be careful about how we interact with it. Specifically, we Open() connections when we need to
            //  interact with the database and we Close() them when we're finished.
            //  In C#, a "using" block ensures we correctly disconnect from a resource even if there is an error.
            //  For database connections, this means the connection will be properly closed.
            using (SqlConnection conn = Connection)
            {
                // Note, we must Open() the connection, the "using" block   doesn't do that for us.
                conn.Open();

                // We must "use" commands too.
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = @"SELECT Id, FirstName, LastName, DepartmentId FROM Employee";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Employee> employees = new List<Employee>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int empFirstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string empFirstNameValue = reader.GetString(empFirstNameColumnPosition);

                        int empLastNameColumnPosition = reader.GetOrdinal("LastName");
                        string emptLastNameValue = reader.GetString(empLastNameColumnPosition);

                        int empDeptIdColumnPosition = reader.GetOrdinal("DepartmentId");
                        int empDeptIdValue = reader.GetInt32(empDeptIdColumnPosition);


                        // Now let's create a new department object using the data from the database.
                        Employee employee = new Employee
                        {
                            Id = idValue,
                            FirstName = empFirstNameValue,
                            LastName = emptLastNameValue,
                            DepartmentId = empDeptIdValue
                        };

                        // ...and add that department object to our list.
                        employees.Add(employee);
                    }

                    // We should Close() the reader. Unfortunately, a "using" block won't work here.
                    reader.Close();

                    // Return the list of departments who whomever called this method.
                    return employees;
                }
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, FirstName, LastName, DepartmentId FROM Employee e
                        WHERE Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int empFirstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string empFirstNameValue = reader.GetString(empFirstNameColumnPosition);

                        int empLastNameColumnPosition = reader.GetOrdinal("LastName");
                        string emptLastNameValue = reader.GetString(empLastNameColumnPosition);

                        int empDeptIdColumnPosition = reader.GetOrdinal("DepartmentId");
                        int empDeptIdValue = reader.GetInt32(empDeptIdColumnPosition);

                        // If we only expect a single row back from the database, we don't need a while loop.
                        var employee = new Employee()
                        {
                            Id = idValue,
                            FirstName = empFirstNameValue,
                            LastName = emptLastNameValue,
                            DepartmentId = empDeptIdValue
                        };

                        reader.Close();

                        return employee;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Employee> GetAllEmployeesWithDepartment()
        {
            //  We must "use" the database connection.
            //  Because a database is a shared resource (other applications may be using it too) we must
            //  be careful about how we interact with it. Specifically, we Open() connections when we need to
            //  interact with the database and we Close() them when we're finished.
            //  In C#, a "using" block ensures we correctly disconnect from a resource even if there is an error.
            //  For database connections, this means the connection will be properly closed.
            using (SqlConnection conn = Connection)
            {
                // Note, we must Open() the connection, the "using" block   doesn't do that for us.
                conn.Open();

                // We must "use" commands too.
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = @"
                        SELECT e.Id, e.FirstName, e.LastName, e.DepartmentId, d.Id, d.DeptName FROM Employee e
                        LEFT JOIN Department d ON e.DepartmentId = d.Id";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Employee> employeesWithDepartments = new List<Employee>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int empFirstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string empFirstNameValue = reader.GetString(empFirstNameColumnPosition);

                        int empLastNameColumnPosition = reader.GetOrdinal("LastName");
                        string emptLastNameValue = reader.GetString(empLastNameColumnPosition);

                        int empDeptIdColumnPosition = reader.GetOrdinal("DepartmentId");
                        int empDeptIdValue = reader.GetInt32(empDeptIdColumnPosition);

                        int deptIdColumnPosition = reader.GetOrdinal("DepartmentId");
                        int deptIdColumnPositionValue = reader.GetInt32(deptIdColumnPosition);

                        int deptNameColumnPosition = reader.GetOrdinal("DeptName");
                        string deptNameColumnPositionValue = reader.GetString(deptNameColumnPosition);

                        // Now let's create a new department object using the data from the database.
                        Employee employee = new Employee
                        {
                            Id = idValue,
                            FirstName = empFirstNameValue,
                            LastName = emptLastNameValue,
                            DepartmentId = empDeptIdValue,
                            Department = new Department()
                            {
                                Id = deptIdColumnPositionValue,
                                DeptName = deptNameColumnPositionValue
                            }
                        };

                        // ...and add that department object to our list.
                        employeesWithDepartments.Add(employee);
                    }

                    // We should Close() the reader. Unfortunately, a "using" block won't work here.
                    reader.Close();

                    // Return the list of departments who whomever called this method.
                    return employeesWithDepartments;
                }
            }
        }
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    cmd.CommandText = "INSERT INTO Employee (FirstName, LastName, DepartmentId) OUTPUT INSERTED.Id Values (@firstName, @lastName, @departmentId)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@departmentId", employee.DepartmentId));
                    int id = (int)cmd.ExecuteScalar();

                    employee.Id = id;
                }
            }

            // when this method is finished we can look in the database and see the new department.
        }

        /// <summary>
        ///  Updates the department with the given id
        /// </summary>
        public void UpdateEmployee(int id, Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Employee
                                    SET FirstName = @firstName
                                    SET LastName = @lastName
                                    SET DepartmentId = @departmentId
                                    WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@firstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@departmentId", employee.DepartmentId));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        ///  Delete the department with the given id
        /// </summary>
        public void DeleteEmployee(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Employee WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
