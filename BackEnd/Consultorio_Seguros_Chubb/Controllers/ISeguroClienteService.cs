using Consultorio_Seguros_Chubb.Models;

namespace Consultorio_Seguros_Chubb.Controllers
{
    public interface ISeguroClienteService
    {
        Task<bool> AgregarSeguroCliente(SeguroCliente segurocliente);

        Task<List<SeguroClienteAll>> VerSeguroClientes();

        void EliminarSeguroCliente(int id);
        
        Task<List<SeguroClienteCedulaandCodigo>> VerSeguroClientesCondicion(string cedula);
     //   Task<List<SeguroClienteCedulaandCodigo>> VerSeguroClientesxCodigo(string codigo);
    
        }
}
