using Consultorio_Seguros_Chubb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio_Seguros_Chubb.Controllers
{
    public interface IAseguradoService
    {
        Asegurado ObtenerAseguradoPorId(int aseguradoId);
        Task<List<Asegurado>> CargarAseguradosAsync();
        Task<List<Asegurado>> ObtenerAseguradosxCondicion(string condicion);
        Task<bool> AgregarAsegurado(Asegurado asegurado);

        Task<bool> GuardarAseguradosMasivamente(List<Asegurado> asegurados);
        Task<bool> ActualizarAsegurado(Asegurado asegurado);
        void EliminarAsegurado(int aseguradoId);
    }
}
