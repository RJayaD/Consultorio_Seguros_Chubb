using Consultorio_Seguros_Chubb.Models;
using System.Data.SqlClient;
using System.Data;

namespace Consultorio_Seguros_Chubb.Controllers
{
    public class SeguroService : ISeguroService
    {
        private readonly string connectionString; 

        public SeguroService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> ActualizarSeguro(Seguro seguro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spEditarSeguro", connection))
                {
                    command.Parameters.AddWithValue("SeguroId", seguro.SeguroId);
                    command.Parameters.AddWithValue("nombre_seguro", seguro.nombre_seguro);
                    command.Parameters.AddWithValue("codigo", seguro.codigo);
                    command.Parameters.AddWithValue("suma", seguro.suma);
                    command.Parameters.AddWithValue("prima", seguro.prima);
                    command.CommandType = CommandType.StoredProcedure;
                    int filas = await command.ExecuteNonQueryAsync();
                    if (filas > 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
        }

        public async Task<bool> AgregarSeguro(Seguro seguro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spGuardarSeguro", connection))
                {
                    command.Parameters.AddWithValue("nombre_seguro", seguro.nombre_seguro);
                    command.Parameters.AddWithValue("codigo", seguro.codigo);
                    command.Parameters.AddWithValue("fecha_creacion", seguro.fecha_creacion);
                    command.Parameters.AddWithValue("suma", seguro.suma);
                    command.Parameters.AddWithValue("prima", seguro.prima);
                    command.CommandType = CommandType.StoredProcedure;
                    int filas = await command.ExecuteNonQueryAsync();
                    if (filas > 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
        }

        public Seguro ObtenerSeguroPorId(int seguroId)
        {
            Seguro seguro = new Seguro();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spVerSeguroId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SeguroId", seguroId));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        seguro.SeguroId = (int)reader["SeguroId"];
                        seguro.nombre_seguro = (string)reader["nombre_seguro"];
                        seguro.codigo = (string)reader["codigo"];
                        seguro.fecha_creacion = (DateTime)reader["fecha_creacion"];
                        seguro.suma = (decimal)reader["suma"];
                        seguro.prima = (decimal)reader["prima"];
                        seguro.estado = (bool)reader["estado"];
                            };
                        
                    }
                }

            return seguro;
        }

        public void EliminarSeguro(int seguroId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spEliminarSeguro", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SeguroId", seguroId));
                    command.ExecuteNonQuery();
                }
            }
        }

            public IEnumerable<Seguro> CargaSeguroId(int id)
            {
                List<Seguro> seguros = new List<Seguro>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT SeguroId, nombre_seguro, codigo, fecha_creacion, suma, prima, estado FROM Seguro";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Seguro seguro = new Seguro
                            {
                                SeguroId = (int)reader["SeguroId"],
                                nombre_seguro = (string)reader["nombre_seguro"],
                                codigo = (string)reader["codigo"],
                                fecha_creacion = (DateTime)reader["fecha_creacion"],
                                suma = (decimal)reader["suma"],
                                prima = (decimal)reader["prima"],
                                estado = (bool)reader["estado"]
                            };

                            seguros.Add(seguro);
                        }
                    }
                }

                return seguros;
            }

        public async Task<List<Seguro>> CargarSeguros()
        {
            List<Seguro> seguros = new List<Seguro>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new ("spVerSeguro", connection))
                    {
                    command.CommandType=CommandType.StoredProcedure;   
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            Seguro seguro = new Seguro
                            {
                                SeguroId = (int)dr["SeguroId"],
                                nombre_seguro = (string)dr["nombre_seguro"],
                                codigo = (string)dr["codigo"],
                                fecha_creacion = (DateTime)dr["fecha_creacion"],
                                suma = (decimal)dr["suma"],
                                prima = (decimal)dr["prima"],
                                estado = (bool)dr["estado"]
                            };

                            seguros.Add(seguro);
                        }
                    }
                }
               
            }

            return seguros ;
        }

        public async Task<List<Seguro>> ObtenerSegurosxCondicion(string condicion)
        {
            List<Seguro> seguros = new List<Seguro>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spVerSeguroxCondicion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@condicion", condicion));
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            Seguro seguro = new Seguro
                            {

                                SeguroId = (int)dr["SeguroId"],
                                nombre_seguro = (string)dr["nombre_seguro"],
                                codigo = (string)dr["codigo"],
                                fecha_creacion = (DateTime)dr["fecha_creacion"],
                                suma = (decimal)dr["suma"],
                                prima = (decimal)dr["prima"],
                                estado = (bool)dr["estado"]

                            };
                            seguros.Add(seguro);

                        }
                    }
                }

            }

            return seguros;
        }

        public IEnumerable<SeguroCombo> CargarComboSeguro()
        {
           
                List<SeguroCombo> segurosCombo = new List<SeguroCombo>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spCargaComboSeguro", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SeguroCombo seguroCombo = new SeguroCombo
                                {
                                    SeguroId = (int)reader["SeguroId"],
                                    nombre_seguro = (string)reader["nombre_seguro"]
                                };

                                segurosCombo.Add(seguroCombo);
                            }
                        }
                    }
                }

                return segurosCombo;

        }
    }

}
