using Consultorio_Seguros_Chubb.Models;
using System.Data.SqlClient;
using System.Data;

namespace Consultorio_Seguros_Chubb.Controllers
{
    public class SeguroClienteService : ISeguroClienteService
    {
        private readonly string connectionString;

        public SeguroClienteService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> AgregarSeguroCliente(SeguroCliente segurocliente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spGuardarSeguroCliente", connection))
                {
                    command.Parameters.AddWithValue("SeguroId", segurocliente.SeguroId);
                    command.Parameters.AddWithValue("AseguradoId", segurocliente.AseguradoId);
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

        public void EliminarSeguroCliente(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spEliminarSeguroCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task<List<SeguroClienteAll>> VerSeguroClientes()
        {
            List<SeguroClienteAll> seguroclientes = new List<SeguroClienteAll>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spVerSegurosClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            SeguroClienteAll segurocliente = new SeguroClienteAll
                            {
                                Id = (int)dr["id"],
                                AseguradoId = (int)dr["AseguradoId"],
                                NombreCliente = (string)dr["nombre_cliente"],
                                Cedula = (string)dr["cedula"],
                                SeguroId = (int)dr["SeguroId"],
                                NombreSeguro = (string)dr["nombre_seguro"],
                                Codigo = (string)dr["codigo"]
                            };

                            seguroclientes.Add(segurocliente);
                        }
                    }
                }
            }
            return seguroclientes;
        }

        public async Task<List<SeguroClienteCedulaandCodigo>> VerSeguroClientesCondicion(string condicion)
        {
            List<SeguroClienteCedulaandCodigo> seguroclientes = new List<SeguroClienteCedulaandCodigo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spVerSegurosClientesCondicional", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@condicion", condicion));
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            SeguroClienteCedulaandCodigo segurocliente = new SeguroClienteCedulaandCodigo
                            {
                                NombreCliente = (string)dr["nombre_cliente"],
                                NombreSeguro = (string)dr["nombre_seguro"],
                                Codigo = (string)dr["codigo"],
                                Telefono = (string)dr["telefono"],
                                Edad = (int)dr["edad"],
                            };

                            seguroclientes.Add(segurocliente);
                        }
                    }
                }
            }
            return seguroclientes;
        }

       /* public async Task<List<SeguroClienteCedulaandCodigo>> VerSeguroClientesxCodigo(string codigo)
        {
            List<SeguroClienteCedulaandCodigo> seguroclientes = new List<SeguroClienteCedulaandCodigo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("spVerAseguradosxCodigo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@codigo", codigo));
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            SeguroClienteCedulaandCodigo segurocliente = new SeguroClienteCedulaandCodigo
                            {
                                NombreCliente = (string)dr["nombre_cliente"],
                                NombreSeguro = (string)dr["nombre_seguro"],
                                Codigo = (string)dr["codigo"],
                                Telefono = (string)dr["telefono"],
                                Edad = (int)dr["edad"],
                            };

                            seguroclientes.Add(segurocliente);
                        }
                    }
                }
            }
            return seguroclientes;
        }*/
    }
}
