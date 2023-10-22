namespace Consultorio_Seguros_Chubb.Models
{
    public class Asegurado
    {
        public int AseguradoId { get; set; }
        public string cedula { get; set; }
        public string nombre_cliente { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string telefono { get; set; }
        public int edad { get; set; }
        public bool estado { get; set; }
    }
}
