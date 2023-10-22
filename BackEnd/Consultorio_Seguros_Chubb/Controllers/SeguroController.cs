using Consultorio_Seguros_Chubb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consultorio_Seguros_Chubb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguroController : ControllerBase
    {


        private readonly ISeguroService seguroService;

        public SeguroController(ISeguroService seguroService)
        {
            this.seguroService = seguroService;
        }
        // GET: api/<ValuesController>
        [HttpGet("verseguros")]
        public async Task<IActionResult> CargarSeguros()
        {
            try
            {
                List<Seguro> seguros = await seguroService.CargarSeguros();
                return Ok(seguros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar seguros: {ex.Message}");
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("CargaCombo")]
        public JsonResult ComboSeguro()
        {
            var segurosCombo = seguroService.CargarComboSeguro();
            return new JsonResult(segurosCombo); // Otra opción es devolverlos como JSON
        }

        [HttpGet("buscarSeguroId")]
        public Seguro? SeguroId(int id)
        {
            var seguro =  seguroService.ObtenerSeguroPorId(id);

            if (seguro != null)
            {
                return seguro;
            }
            return null;
        }
        // POST api/<ValuesController>
        [HttpPost("guardarSeguro")]
        public async Task<IActionResult> AgregarSeguro([FromBody] Seguro seguro)
        {
            
            bool resultado= await seguroService.AgregarSeguro(seguro);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { valor = resultado, msg = "Error" });
            }
        }

        [HttpPost("actualizarSeguro")]
        public async Task<IActionResult> ActualizarSeguro([FromBody] Seguro seguro)
        {
            
                bool resultado = await seguroService.ActualizarSeguro(seguro);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { valor = resultado, msg = "Error" });
            }
        }

        [HttpGet("verseguroxcondicion")]
        public async Task<IActionResult> SeguroxCondicional(string condicion)
        {
            try
            {
                List <Seguro> seguro = await seguroService.ObtenerSegurosxCondicion(condicion);
                return Ok(seguro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar seguros: {ex.Message}");
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpPost("eliminarSeguro")]
        public void Delete(int id)
        {
            try
            { 
            seguroService.EliminarSeguro(id);
            Ok();
            }
            catch (Exception ex) {
                StatusCode(500, $"Error al eliminar seguro: {ex.Message}");
            }
           
        }
    }
}
