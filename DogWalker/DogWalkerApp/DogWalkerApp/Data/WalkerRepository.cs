using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace DogWalkerApp
{
    /// <summary>
    ///  An object to contain all database interactions.
    /// </summary>
    public class WalkerRepository
    {
        /// <summary>
        ///  Represents a connection to the database.
        ///   This is a "tunnel" to connect the application to the database.
        ///   All communication between the application and database passes through this connection.
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Dog Walker;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        /// <summary>
        ///  Returns a list of all departments in the database
        /// </summary>
        public List<Walker> GetAllWalkers()
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
                    cmd.CommandText = "SELECT Id, WalkerName, NeighborhoodId FROM Walker";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Walker> walkers = new List<Walker>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int walkerNameColumnPosition = reader.GetOrdinal("WalkerName");
                        string walkerNameColumnValue = reader.GetString(walkerNameColumnPosition);

                        int neighborhoodIdColumnPosition = reader.GetOrdinal("NeighborhoodId");
                        int neighborhoodIdColumnValue = reader.GetInt32(neighborhoodIdColumnPosition);

                        // Now let's create a new department object using the data from the database.
                        Walker walker = new Walker
                        {
                            Id = idValue,
                            WalkerName = walkerNameColumnValue,
                            NeighborhoodId = neighborhoodIdColumnValue
                        };

                        // ...and add that department object to our list.
                        walkers.Add(walker);
                    }

                    // We should Close() the reader. Unfortunately, a "using" block won't work here.
                    reader.Close();

                    // Return the list of departments who whomever called this method.
                    return walkers;
                }
            }
        }

        /// <summary>
        ///  Returns a single department with the given id.
        /// </summary>
        public Walker GetWalkerByNeighborhoodId(int neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, WalkerName, NeighborhoodId FROM Walker WHERE NeighborhoodId = @neighborhoodId";
                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", neighborhoodId));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Walker walker = null;

                    // If we only expect a single row back from the database, we don't need a while loop.
                    if (reader.Read())
                    {
                        walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            WalkerName = reader.GetString(reader.GetOrdinal("WalkerName")),
                            NeighborhoodId = neighborhoodId
                        };
                    }   

                    reader.Close();

                    return walker;
                }
            }
        }

        /// <summary>
        ///  Add a new department to the database
        ///   NOTE: This method sends data to the database,
        ///   it does not get anything from the database, so there is nothing to return.
        /// </summary>
        public void AddWalker(Walker walker)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    cmd.CommandText = "INSERT INTO Walker (WalkerName, NeighborhoodId) OUTPUT INSERTED.Id Values (@walkerName, @neighborhoodId)";
                    cmd.Parameters.Add(new SqlParameter("@walkerName", walker.WalkerName));
                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", walker.NeighborhoodId));
                    int id = (int)cmd.ExecuteScalar();

                    walker.Id = id;
                }
            }

            // when this method is finished we can look in the database and see the new department.
        }

        /// <summary>
        ///  Updates the department with the given id
        /// </summary>
        public void UpdateWalker(int id, Walker walker)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Walker
                                     SET WalkerName = @walkerName, NeighborhoodId = @neighborhoodId
                                     WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@walkerName", walker.WalkerName));
                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", walker.NeighborhoodId));
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        ///  Delete the department with the given id
        /// </summary>
        public void DeleteWalker(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Walker WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}