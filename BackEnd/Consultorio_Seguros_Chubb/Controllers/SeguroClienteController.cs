using Consultorio_Seguros_Chubb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

namespace Consultorio_Seguros_Chubb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguroClienteController : Controller
    {
        private readonly ISeguroClienteService seguroclienteService;

        public SeguroClienteController(ISeguroClienteService seguroclienteService)
        {
            this.seguroclienteService = seguroclienteService;
        }


        // GET: SeguroClienteController
        [HttpGet("verseguroclientes")]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<SeguroClienteAll> aseguradosclientes = await seguroclienteService.VerSeguroClientes();
                return Ok(aseguradosclientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar seguros: {ex.Message}");
            }
        }

        [HttpGet("verseguroclientesxcondicional")]
        public async Task<IActionResult> SCxCedula(string condicional)
        {
            try
            {
                List<SeguroClienteCedulaandCodigo> aseguradosclientes = await seguroclienteService.VerSeguroClientesCondicion(condicional);
                return Ok(aseguradosclientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar seguros: {ex.Message}");
            }
        }
        
        /*[HttpGet("verseguroclientesxcodigo")]
        public async Task<IActionResult> SeguroClientexCodigo(string codigo)
        {
            try
            {
                List<SeguroClienteCedulaandCodigo> aseguradosclientes = await seguroclienteService.VerSeguroClientesxCodigo(codigo);
                return Ok(aseguradosclientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar seguros: {ex.Message}");
            }
        }*/

        // GET: SeguroClienteController/Details/5
        // POST: SeguroClienteController/Create
        [HttpPost("AgregarSeguroCliente")]
        public async Task<IActionResult> Create([FromBody] SeguroCliente seguroCliente)
        {
            bool resultado =await  seguroclienteService.AgregarSeguroCliente(seguroCliente);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { valor = resultado, msg = "Error" });
            }

        }


        // GET: SeguroClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
