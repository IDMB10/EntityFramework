using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace projectef.Models {
    public class Categoria {
        //[Key] //DataAnotacions. Key para que se use como id de la tabla
        public Guid CategoriaId { get; set; } //Recomendable como nombre el nombre de clase mas Id

        //[Required]
        //[MaxLength(150)]
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int Peso { get; set; } //Se agrega propiedad para simular la migración

        //[JsonIgnore]  //Para evitar que hayan redundancias ciclicas.
        public virtual ICollection<Tarea>? Tareas { get; set; }
    }
}