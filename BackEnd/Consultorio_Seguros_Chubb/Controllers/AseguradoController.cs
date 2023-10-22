using Consultorio_Seguros_Chubb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace Consultorio_Seguros_Chubb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoController : ControllerBase
    {
        private readonly IAseguradoService aseguradoService;

        public AseguradoController(IAseguradoService aseguradoService)
        {
            this.aseguradoService = aseguradoService;
        }

        [HttpGet("verasegurados")]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Asegurado> asegurados = await aseguradoService.CargarAseguradosAsync();
                return Ok(asegurados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar seguros: {ex.Message}");
            }
           
        }
        [HttpGet("veraseguradosxcondicion")]
        public async Task<IActionResult> AseguradoxCondicion(string condicion)
        {
            try
            {
                List<Asegurado> asegurados = await aseguradoService.ObtenerAseguradosxCondicion(condicion);
                return Ok(asegurados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar seguros: {ex.Message}");
            }

        }

        [HttpGet("buscarAseguradoId")]
        public Asegurado? AseguradoId(int id)
        {
            var asegurado = aseguradoService.ObtenerAseguradoPorId(id);

            if (asegurado != null)
            {
                return asegurado;
            }
            return null;
        }

        [HttpPost("agregarAsegurado")]
        public async Task<IActionResult> AgregarAsegurado([FromBody] Asegurado asegurado)
        {
            bool resultado = await aseguradoService.AgregarAsegurado(asegurado);
            if (resultado)
                {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
                    }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { valor = resultado, msg = "Error" });
            }
        }

        [HttpPost("agregarAseguradoMasivamente")]
        public async Task<IActionResult> AgregarAseguradoMasivamente([FromBody] List<Asegurado> asegurados)
        {
            bool resultado = await aseguradoService.GuardarAseguradosMasivamente(asegurados);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { valor = resultado, msg = "Error" });
            }
        }


        [HttpPost("actualizarAsegurado")]
        public async Task<IActionResult> ActualizarAsegurado([FromBody] Asegurado asegurado)
        {
            
                bool resultado = await aseguradoService.ActualizarAsegurado(asegurado);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { valor = resultado, msg = "Error" });
            }


        }

        [HttpPost("eliminarAsegurado")]
        public void Delete(int id)
        {
            try
            {
                aseguradoService.EliminarAsegurado(id);
                Ok();
            }
            catch (Exception ex) { }

        }
    }
}
