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
    public class WalkerController : ControllerBase
    {
        private readonly IConfiguration _config;

        public WalkerController(IConfiguration config)
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
        public async Task<IActionResult> Get([FromQuery] int? neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.WalkerName, w.NeighborhoodId, n.NeighborhoodName FROM Walker w
                        LEFT JOIN Neighborhood N on w.NeighborhoodId = n.Id
                        WHERE 1=1";

                    if (neighborhoodId != null)
                    {
                        cmd.CommandText += " AND NeighborhoodId LIKE @neighborhoodId";
                        cmd.Parameters.Add(new SqlParameter("@neighborhoodId", neighborhoodId));
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Walker> walkers = new List<Walker>();

                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            WalkerName = reader.GetString(reader.GetOrdinal("WalkerName")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = new Neighborhood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                NeighborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                            },
                        };

                        walkers.Add(walker);
                    }
                    reader.Close();

                    return Ok(walkers);
                }
            }
        }

        [HttpGet("{id}", Name = "GetWalker")]
        public async Task<IActionResult> Get(
            [FromRoute] int id,
            [FromQuery] string include)
        {
            if (include != "walks")
            {
                var walker = GetWalker(id);
                return Ok(walker);
            }
            else
            {
                var walker = GetWalkerWithWalks(id);
                return Ok(walker);
            }
        }

        [HttpGet]
        private List<Walker> GetWalkersByNeighborhood(int? neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.WalkerName, w.NeighborhoodId FROM Walker w
                        AND NeighborhoodId LIKE @neighborhoodId";

                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", neighborhoodId));
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Walker> walkers = new List<Walker>();

                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            WalkerName = reader.GetString(reader.GetOrdinal("WalkerName")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                        };

                        walkers.Add(walker);
                    }
                    reader.Close();

                    return walkers;
                }
            }
        }

        private Walker GetWalker(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id AS walkerId, w.WalkerName, w.NeighborhoodId, n.NeighborhoodName FROM Walker w
                        LEFT JOIN Neighborhood N ON w.NeighborhoodId = n.Id
                        WHERE w.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Walker walker = null;

                    if (reader.Read())
                    {
                        walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("walkerId")),
                            WalkerName = reader.GetString(reader.GetOrdinal("WalkerName")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = new Neighborhood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                NeighborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                            },
                        };
                    }
                    reader.Close();
                    return walker;
                }
            }
        }

        private Walker GetWalkerWithWalks(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id AS walkerId, w.WalkerName, w.NeighborhoodId, n.NeighborhoodName, walk.Id AS walkId, walk.WalkDate, walk.Duration, walk.WalkerId, walk.Dogid FROM Walker w
                        LEFT JOIN Neighborhood N ON w.NeighborhoodId = n.Id
                        LEFT JOIN Walk walk ON w.Id = walk.WalkerId
                        WHERE w.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Walker walker = null;

                    while (reader.Read())
                    {
                        if (walker == null)
                        {
                            walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("walkerId")),
                                WalkerName = reader.GetString(reader.GetOrdinal("WalkerName")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    NeighborhoodName = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                                },
                                Walks = new List<Walk>()
                            };
                        }
                        walker.Walks.Add(new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("walkId")),
                            WalkDate = reader.GetDateTime(reader.GetOrdinal("WalkDate")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("walkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        });
                    }
                    reader.Close();

                    return walker;
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Walker walker)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Walker (WalkerName, NeighborhoodId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@walkerName, @neighborhoodId)";
                    cmd.Parameters.Add(new SqlParameter("@walkerName", walker.WalkerName));
                    cmd.Parameters.Add(new SqlParameter("@neighborhoodId", walker.NeighborhoodId));

                    int newId = (int)cmd.ExecuteScalar();
                    walker.Id = newId;
                    return CreatedAtRoute("GetWalker", new { id = newId }, walker);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Walker walker)
        {
            try
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
                if (!WalkerExists(id))
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
                        cmd.CommandText = @"DELETE FROM Walker WHERE Id = @id";
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
                if (!WalkerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }


        private bool WalkerExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, WalkerName, NeighborhoodId
                        FROM Walker
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
        }
    }
}
