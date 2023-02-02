using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace projectef.Models {
    public class Tarea {
        //[Key]
        public Guid TareaId { get; set; }

        //[ForeignKey("CategoriaId")]
        public Guid CategoriaId { get; set; }

        //[Required]
        //[MaxLength(200)]
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public Priodidad PriodidadTarea { get; set; }
        public DateTime FechaCreacion { get; set; }

        [JsonIgnore]  //Para evitar las redundancias ciclicas entre clases. Pero solo uno debe tenerlo
        public virtual Categoria? Categoria { get; set; }
        //[NotMapped] //No mapea el campo hacia la db. Solo para uso del desarrollador
        public string? Resumen { get; set; }
    }

    public enum Priodidad {
        Baja,
        Media,
        Alta
    }
}

