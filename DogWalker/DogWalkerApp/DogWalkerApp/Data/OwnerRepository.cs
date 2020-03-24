using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace DogWalkerApp
{
    /// <summary>
    ///  An object to contain all database interactions.
    /// </summary>
    public class OwnerRepository
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
        public List<Owner> GetAllOwnersWithNeighborhood()
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
                        SELECT o.Id, o.DogOwnerName, o.DogOwnerAddress, o.NeighborhoodId, o.Phone, n.NeighborhoodName FROM DogOwner o
                        LEFT JOIN Neighborhood n ON o.NeighborhoodId = n.Id";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Owner> owners = new List<Owner>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int ownerNameColumnPosition = reader.GetOrdinal("DogOwnerName");
                        string ownerNameColumnValue = reader.GetString(ownerNameColumnPosition);

                        int ownerAddressColumnPosition = reader.GetOrdinal("DogOwnerAddress");
                        string ownerAddressColumnValue = reader.GetString(ownerAddressColumnPosition);

                        int neighborhoodIdColumnPosition = reader.GetOrdinal("NeighborhoodId");
                        int neighborhoodIdColumnValue = reader.GetInt32(neighborhoodIdColumnPosition);

                        int ownerPhoneColumnPosition = reader.GetOrdinal("Phone");
                        string ownerPhoneColumnValue = reader.GetString(ownerPhoneColumnPosition);

                        int neighborhoodNameColumnPosition = reader.GetOrdinal("NeighborhoodName");
                        string neighborhoodNameColumnValue = reader.GetString(neighborhoodNameColumnPosition);

                        // Now let's create a new department object using the data from the database.
                        Owner owner = new Owner
                        {
                            Id = idValue,
                            DogOwnerName = ownerNameColumnValue,
                            DogOwnerAddress = ownerAddressColumnValue,
                            NeighborhoodId = neighborhoodIdColumnValue,
                            Phone = ownerPhoneColumnValue,
                            Neighborhood = new Neighborhood()
                            {
                                Id = neighborhoodIdColumnValue,
                                NeighborhoodName = neighborhoodNameColumnValue
                            }
                        };

                        // ...and add that department object to our list.
                        owners.Add(owner);
                    }

                    // We should Close() the reader. Unfortunately, a "using" block won't work here.
                    reader.Close();

                    // Return the list of departments who whomever called this method.
                    return owners;
                }
            }
        }

        /// <summary>
        ///  Add a new department to the database
        ///   NOTE: This method sends data to the database,
        ///   it does not get anything from the database, so there is nothing to return.
        /// </summary>
        public void AddOwner(Owner owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    cmd.CommandText = "INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone) OUTPUT INSERTED.Id Values (@ownerName, @ownerAddress, @neighborhoodId, @phone)";
                    cmd.Parameters.Add(new SqlParameter("@ownerName", owner.DogOwnerName));
                    cmd.Parameters.Add(new SqlParameter("@ownerAddress", owner.DogOwnerAddress));
                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", owner.NeighborhoodId));
                    cmd.Parameters.Add(new SqlParameter("@phone", owner.Phone));
                    int id = (int)cmd.ExecuteScalar();

                    owner.Id = id;
                }
            }

            // when this method is finished we can look in the database and see the new department.
        }

        /// <summary>
        ///  Updates the department with the given id
        /// </summary>
        public void UpdateOwner(int id, Owner owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE DogOwner
                                     SET DogOwnerName = @ownerName, DogOwnerAddress=@ownerAddress, NeighborhoodId = @neighborhoodId, Phone = @phone
                                     WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@ownerName", owner.DogOwnerName));
                    cmd.Parameters.Add(new SqlParameter("@ownerAddress", owner.DogOwnerAddress));
                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", owner.NeighborhoodId));
                    cmd.Parameters.Add(new SqlParameter("@phone", owner.Phone));
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}