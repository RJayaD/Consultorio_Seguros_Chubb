namespace Consultorio_Seguros_Chubb.Models
{
    public class SeguroClienteAll
    {
        public int Id { get; set; }
        public int AseguradoId { get; set; }
        public string NombreCliente { get; set; }
        public string Cedula { get; set; }
        public int SeguroId { get; set; }
        public string NombreSeguro { get; set; }
        public string Codigo { get; set; }

    }
}
