using Consultorio_Seguros_Chubb.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio_Seguros_Chubb.Controllers
{
    public class AseguradoService : IAseguradoService
    {
        private readonly string connectionString;

        public AseguradoService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> ActualizarAsegurado(Asegurado asegurado)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spEditarAsegurado", connection))
                {
                    command.Parameters.AddWithValue("AseguradoId", asegurado.AseguradoId);
                    command.Parameters.AddWithValue("cedula", asegurado.cedula);
                    command.Parameters.AddWithValue("nombre_cliente", asegurado.nombre_cliente);
                    command.Parameters.AddWithValue("telefono", asegurado.telefono);
                    command.Parameters.AddWithValue("edad", asegurado.edad);
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

        public async Task<bool> AgregarAsegurado(Asegurado asegurado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spGuardarAsegurado", connection))
                {
                    command.Parameters.AddWithValue("cedula", asegurado.cedula);
                    command.Parameters.AddWithValue("nombre_cliente", asegurado.nombre_cliente);
                    command.Parameters.AddWithValue("fecha_creacion", asegurado.fecha_creacion.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("telefono", asegurado.telefono);
                    command.Parameters.AddWithValue("edad", asegurado.edad);
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


        public async Task<List<Asegurado>> CargarAseguradosAsync()
        {
               
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();


                        // Crea un objeto SqlCommand
                        using (var cmd = new SqlCommand("spVerAsegurado", connection))
                        {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                var asegurados = new List<Asegurado>();

                                while (await reader.ReadAsync())
                                {
                                    var asegurado = new Asegurado
                                    {
                                        AseguradoId = reader.GetInt32(reader.GetOrdinal("AseguradoId")),
                                        cedula = reader.GetString(reader.GetOrdinal("cedula")),
                                        nombre_cliente = reader.GetString(reader.GetOrdinal("nombre_cliente")),
                                        fecha_creacion = reader.GetDateTime(reader.GetOrdinal("fecha_creacion")),
                                        telefono = reader.GetString(reader.GetOrdinal("telefono")),
                                        edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                        estado = reader.GetBoolean(reader.GetOrdinal("estado"))
                                    };

                                    asegurados.Add(asegurado);
                                }

                                return asegurados;
                            }
                        }
                    }
               
            }

        public void EliminarAsegurado(int aseguradoId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spEliminarAsegurado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AseguradoId", aseguradoId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task<bool> GuardarAseguradosMasivamente(List<Asegurado> asegurados)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var asegurado in asegurados)
                        {
                            using (SqlCommand command = new("spGuardarAsegurado", connection, transaction))
                            {
                                command.Parameters.AddWithValue("cedula", asegurado.cedula);
                                command.Parameters.AddWithValue("nombre_cliente", asegurado.nombre_cliente);
                                command.Parameters.AddWithValue("fecha_creacion", asegurado.fecha_creacion.ToString("yyyy-MM-dd"));
                                command.Parameters.AddWithValue("telefono", asegurado.telefono);
                                command.Parameters.AddWithValue("edad", asegurado.edad);
                                command.CommandType = CommandType.StoredProcedure;

                                int filas = await command.ExecuteNonQueryAsync();

                                if (filas <= 0)
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                        }

                        transaction.Commit(); 

                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public Asegurado ObtenerAseguradoPorId(int aseguradoId)
        {
            Asegurado asegurado = new Asegurado();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spVerAseguradoId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AseguradoId", aseguradoId));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        asegurado.AseguradoId = (int)reader["AseguradoId"];
                        asegurado.cedula = (string)reader["cedula"];
                        asegurado.nombre_cliente = (string)reader["nombre_cliente"];
                        asegurado.fecha_creacion = (DateTime)reader["fecha_creacion"];
                        asegurado.telefono = (string)reader["telefono"];
                        asegurado.edad = (int)reader["edad"];
                        asegurado.estado = (bool)reader["estado"];
                    };

                }
            }

            return asegurado;
        }

        public async Task<List<Asegurado>> ObtenerAseguradosxCondicion(string condicion)
        {
            List<Asegurado> asegurados = new List<Asegurado>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spVerAseguradoxCondicion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@condicion", condicion));
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            Asegurado asegurado = new Asegurado
                            {

                                AseguradoId = (int)dr["AseguradoId"],
                                cedula = (string)dr["cedula"],
                                nombre_cliente = (string)dr["nombre_cliente"],
                                fecha_creacion = (DateTime)dr["fecha_creacion"],
                                telefono = (string)dr["telefono"],
                                edad = (int)dr["edad"],
                                estado = (bool)dr["estado"]

                            };
                            asegurados.Add(asegurado);
                        }
                    }
                }
            }
                return asegurados;
        }
    }


}