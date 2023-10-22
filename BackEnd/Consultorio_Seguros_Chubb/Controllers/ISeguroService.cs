
using Consultorio_Seguros_Chubb.Models;

namespace Consultorio_Seguros_Chubb.Controllers
{
    public interface ISeguroService
    {
         Seguro ObtenerSeguroPorId(int seguroId);
         Task<List<Seguro>> CargarSeguros();
        Task<List<Seguro>> ObtenerSegurosxCondicion(string condicion);
        Task<bool> AgregarSeguro(Seguro seguro);
        Task<bool> ActualizarSeguro(Seguro seguro);
        void EliminarSeguro(int seguroId);
        IEnumerable<SeguroCombo> CargarComboSeguro();
    }
}
