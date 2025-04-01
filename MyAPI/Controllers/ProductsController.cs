using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> products = new List<string>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Author FROM Textbooks";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(reader["Author"].ToString());
                        }
                    }
                }
            }
            return products;
        }
    }
}