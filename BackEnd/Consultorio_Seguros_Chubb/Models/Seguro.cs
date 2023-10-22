namespace Consultorio_Seguros_Chubb.Models
{
    public class Seguro
    {
            public int SeguroId { get; set; }
            public string nombre_seguro { get; set; }
            public string codigo { get; set; }
            public DateTime fecha_creacion { get; set; }
            public decimal suma { get; set; }
            public decimal prima { get; set; }
            public bool estado { get; set; }
       
    }
}
