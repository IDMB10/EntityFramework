using Microsoft.EntityFrameworkCore;
using projectef.Models;

namespace projectef {
    public class TareasContext : DbContext {
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Tarea>? Tareas { get; set; }
        public TareasContext(DbContextOptions<TareasContext> options)
        : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //Creando las tablas y las anotaciones de los modelos con Fluent API. OnModelCreating se ejecuta cuando se esta creando el contexto por ello se sobreescribe.
        {
            //Creando lista de categorias para agregar a la tabla cuando se cree
            List<Categoria> categoriasInit = new List<Categoria>();
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("6f49b05b-696b-4c8d-b227-aa679901b932"), Nombre = "Actividades Pendientes", Peso = 20 });
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("e1c3e934-fd3c-4602-bb04-99ab5dabfe53"), Nombre = "Actividades Personales", Peso = 50 });

            modelBuilder.Entity<Categoria>(categoria => {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);
                //categoria.HasMany(p => p.Tareas).WithOne(p => p.Categoria).HasForeignKey(p => p.TareaId).IsRequired(true); //Otra forma de crear la relación con variable de navegación en lugar de usar virtual en el modelo
                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Descripcion).IsRequired(false);
                categoria.Property(p => p.Peso);

                categoria.HasData(categoriasInit); //Agregando datos iniciales cuando se crea la db
            });

            List<Tarea> tareasInit = new List<Tarea>();
            tareasInit.Add(new Tarea() { TareaId = Guid.Parse("a8cd181f-64ef-420a-be02-b8e28a79b2dc"), CategoriaId = Guid.Parse("6f49b05b-696b-4c8d-b227-aa679901b932"), PriodidadTarea = Priodidad.Media, Titulo = "Pago Servicios Publicos", FechaCreacion = DateTime.Now });
            tareasInit.Add(new Tarea() { TareaId = Guid.Parse("9460c722-8fb4-4373-bc81-26baba96a843"), CategoriaId = Guid.Parse("e1c3e934-fd3c-4602-bb04-99ab5dabfe53"), PriodidadTarea = Priodidad.Baja, Titulo = "Terminar de ver peliculas", FechaCreacion = DateTime.Now });

            //modelBuilder.Entity<Tarea>().Property(p => p.FechaCreacion).HasColumnType("datetime2"); //Configurar propiedades o columnas de manera individual
            //HasColumnName() para cambiar el nombre a la columna
            //HasColumnOrder() cambia el lugar de la columna
            //IsOptional() permite campos nulos
            //IsRequired() no permite campos nulos

            //modelBuilder.Entity<Tarea>().HasKey(p => p.TareaId); //Otra forma de agregar la primaryKey

            modelBuilder.Entity<Tarea>(tarea => {
                tarea.ToTable("Tarea");
                tarea.HasKey(p => p.TareaId);
                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Descripcion).IsRequired(false);
                tarea.Property(p => p.PriodidadTarea);
                tarea.Property(p => p.FechaCreacion);
                tarea.Ignore(p => p.Resumen);

                tarea.HasData(tareasInit);
            });
        }        
    }
}