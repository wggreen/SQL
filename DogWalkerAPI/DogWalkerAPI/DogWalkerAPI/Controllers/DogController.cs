    using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace DogWalkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DogController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int? neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT d.Id, d.DogName, d.DogOwnerId, d.Breed, d.Notes, o.DogOwnerName, o.DogOwnerAddress, o.NeighborhoodId, o.Phone, n.NeighborhoodName FROM Dog d
                        LEFT JOIN DogOwner o ON d.DogOwnerId = o.Id
                        LEFT JOIN Neighborhood n ON o.NeighborhoodId = n.Id
                        WHERE 1=1";

                    if (neighborhoodId != null)
                    {
                        cmd.CommandText += " AND NeighborhoodId LIKE @neighborhoodId";
                        cmd.Parameters.Add(new SqlParameter("@neighborhoodId", neighborhoodId));
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Dog> dogs = new List<Dog>();

                    while (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                            DogOwnerId = reader.GetInt32(reader.GetOrdinal("DogOwnerId")),
                            DogOwner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogOwnerId")),
                                DogOwnerName = reader.GetString(reader.GetOrdinal("DogOwnerName")),
                                DogOwnerAddress = reader.GetString(reader.GetOrdinal("DogOwnerAddress")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    NeighborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                                },
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            },
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes"))
                        };

                        dogs.Add(dog);
                    }
                    reader.Close();

                    return Ok(dogs);
                }
            }
        }

        [HttpGet("{id}", Name = "Getdog")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT d.Id, d.DogName, d.DogOwnerId, d.Breed, d.Notes, o.DogOwnerName, o.DogOwnerAddress, o.NeighborhoodId, o.Phone, n.NeighborhoodName FROM Dog d
                        LEFT JOIN DogOwner o ON d.DogOwnerId = o.Id
                        LEFT JOIN Neighborhood n ON o.NeighborhoodId = n.Id
                        WHERE d.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Dog dog = null;

                    if (reader.Read())
                    {
                        dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                            DogOwnerId = reader.GetInt32(reader.GetOrdinal("DogOwnerId")),
                            DogOwner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogOwnerId")),
                                DogOwnerName = reader.GetString(reader.GetOrdinal("DogOwnerName")),
                                DogOwnerAddress = reader.GetString(reader.GetOrdinal("DogOwnerAddress")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    NeighborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                                },
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            },
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes"))
                        };
                        reader.Close();

                        return Ok(dog);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dog dog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes)
                                        OUTPUT INSERTED.Id
                                        VALUES (@dogName, @dogOwnerId, @breed, @notes)";
                    cmd.Parameters.Add(new SqlParameter("@dogName", dog.DogName));
                    cmd.Parameters.Add(new SqlParameter("@dogOwnerId", dog.DogOwnerId));
                    cmd.Parameters.Add(new SqlParameter("@breed", dog.Breed));
                    cmd.Parameters.Add(new SqlParameter("@notes", dog.Notes));

                    int newId = (int)cmd.ExecuteScalar();
                    dog.Id = newId;
                    return CreatedAtRoute("GetDog", new { id = newId }, dog);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Dog dog)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Dog
                                            SET DogName = @dogName, DogOwnerId = @dogOwnerId, Breed = @breed, Notes = @notes
                                            WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@dogName", dog.DogName));
                        cmd.Parameters.Add(new SqlParameter("@dogOwnerId", dog.DogOwnerId));
                        cmd.Parameters.Add(new SqlParameter("@breed", dog.Breed));
                        cmd.Parameters.Add(new SqlParameter("@notes", dog.Notes));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!DogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM Dog WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!DogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }


        private bool DogExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, DogName, DogOwnerId, Breed, Notes
                        FROM Dog
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
        }
    }
}
