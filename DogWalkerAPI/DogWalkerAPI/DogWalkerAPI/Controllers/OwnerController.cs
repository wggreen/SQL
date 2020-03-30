using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DogWalkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IConfiguration _config;

        public OwnerController(IConfiguration config)
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
        public async Task<IActionResult> Get([FromQuery] string q)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT o.Id, o.DogOwnerName, o.DogOwnerAddress, o.NeighborhoodId, n.NeighborhoodName, o.Phone, d.Id AS 'DogId', d.DogName, d.DogOwnerId, d.Breed, d.Notes from DogOwner o
                        LEFT JOIN Neighborhood N on o.NeighborhoodId = n.Id
                        LEFT JOIN Dog d on d.DogOwnerId = o.Id
                        WHERE 1=1";

                    if (q != null)
                    {
                        cmd.CommandText += " AND DogOwnerName LIKE @dogOwnerName";
                        cmd.Parameters.Add(new SqlParameter("@dogOwnerName", "%" + q + "%"));
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Owner> owners = new List<Owner>();

                    while (reader.Read())
                    {
                        if (!owners.Any(o => o.Id == reader.GetInt32(reader.GetOrdinal("Id"))))
                        {
                            Owner owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                DogOwnerName = reader.GetString(reader.GetOrdinal("DogOwnerName")),
                                DogOwnerAddress = reader.GetString(reader.GetOrdinal("DogOwnerAddress")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    NeighborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                                },
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Dogs = new List<Dog>()
                            };
                            owners.Add(owner);
                        }
                        Owner existingOwner = owners.FirstOrDefault(o => o.Id == reader.GetInt32(reader.GetOrdinal("Id")));
                        existingOwner.Dogs.Add(new Dog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                            DogOwnerId = reader.GetInt32(reader.GetOrdinal("DogOwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes"))
                        });
                    }
                    reader.Close();

                    return Ok(owners);
                }
            }
        }

        [HttpGet("{id}", Name = "GetOwner")]
        public async Task<IActionResult> Get(
            [FromRoute] int id,
            [FromQuery] string include)
        {
            if (include != "neighborhood")
            {
                var owner = GetOwner(id);
                return Ok(owner);
            }
            else
            {
                var owner = GetOwnerWithNeighborhood(id);
                return Ok(owner);
            }
        }

        private Owner GetOwner(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT o.Id, o.DogOwnerName, o.DogOwnerAddress, o.NeighborhoodId, o.Phone, d.Id AS 'DogId', d.DogName, d.DogOwnerId, d.Breed, d.Notes from DogOwner o
                        LEFT JOIN Dog d on d.DogOwnerId = o.Id
                        WHERE o.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Owner owner = null;

                    while (reader.Read())
                    {
                        if (owner == null)
                        {
                            owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                DogOwnerName = reader.GetString(reader.GetOrdinal("DogOwnerName")),
                                DogOwnerAddress = reader.GetString(reader.GetOrdinal("DogOwnerAddress")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Dogs = new List<Dog>()
                            };
                        }
                        owner.Dogs.Add(new Dog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                            DogOwnerId = reader.GetInt32(reader.GetOrdinal("DogOwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes"))
                        });
                    }
                    reader.Close();
                    return owner;
                }
            }
        }

        private Owner GetOwnerWithNeighborhood(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT o.Id, o.DogOwnerName, o.DogOwnerAddress, o.NeighborhoodId, o.Phone, n.NeighborhoodName, d.Id AS 'DogId', d.DogName, d.DogOwnerId, d.Breed, d.Notes from DogOwner o
                        LEFT JOIN Neighborhood N on o.NeighborhoodId = n.Id
                        LEFT JOIN Dog d on d.DogOwnerId = o.Id
                        WHERE o.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Owner owner = null;

                    while (reader.Read())
                    {
                        if (owner == null)
                        {
                            owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                DogOwnerName = reader.GetString(reader.GetOrdinal("DogOwnerName")),
                                DogOwnerAddress = reader.GetString(reader.GetOrdinal("DogOwnerAddress")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    NeighborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                                },
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Dogs = new List<Dog>()
                            };
                        }
                        owner.Dogs.Add(new Dog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                            DogOwnerId = reader.GetInt32(reader.GetOrdinal("DogOwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes"))
                        });
                    }
                    reader.Close();
                    return owner;
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Owner owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone)
                                        OUTPUT INSERTED.Id
                                        VALUES (@dogOwnerName, @dogOwnerAddress, @neighborhoodId, @phone)";
                    cmd.Parameters.Add(new SqlParameter("@dogOwnerName", owner.DogOwnerName));
                    cmd.Parameters.Add(new SqlParameter("@dogOwnerAddress", owner.DogOwnerAddress));
                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", owner.NeighborhoodId));
                    cmd.Parameters.Add(new SqlParameter("@phone", owner.Phone));

                    int newId = (int)cmd.ExecuteScalar();
                    owner.Id = newId;
                    return CreatedAtRoute("GetDog", new { id = newId }, owner);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Owner owner)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE DogOwner
                                            SET DogOwnerName = @dogOwnerName, DogOwnerAddress = @dogOwnerAddress, NeighborhoodId = @neighborhoodId, Phone = @phone
                                            WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@dogOwnerName", owner.DogOwnerName));
                        cmd.Parameters.Add(new SqlParameter("@dogOwnerAddress", owner.DogOwnerAddress));
                        cmd.Parameters.Add(new SqlParameter("@neighborhoodId", owner.NeighborhoodId));
                        cmd.Parameters.Add(new SqlParameter("@phone", owner.Phone));
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
                if (!OwnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool OwnerExists(int id)
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
