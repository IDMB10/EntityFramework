using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;
using projectef.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));  //Crear la db en memoria
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareasWA")); //Conectar con Autenticacion de Windows.
//builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareasSA")); //Conectar con autenticación de SQL Server

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => {
    await dbContext.Database.EnsureCreatedAsync();  //Verificar si la db esta creada
    return Results.Ok($"Base de datos en memoria {dbContext.Database.IsInMemory()}");
});


app.MapGet("/api/tareas", ([FromServices] TareasContext dbContext) => {
    //return Results.Ok(dbContext.Tareas);  //Retornar todas las tareas 
    //return Results.Ok(dbContext.Tareas.Where(t => t.PriodidadTarea == projectef.Models.Priodidad.Baja));  //Usar funciones lambda para traer lo que se desee
    return Results.Ok(dbContext.Tareas?.Include(p => p.Categoria).Where(t => t.PriodidadTarea == projectef.Models.Priodidad.Baja));  //Llenar el campo de Categoria con la información de la otra tabla

});

app.MapGet("/api/categorias", ([FromServices] TareasContext dbContext) => {
    return Results.Ok(dbContext.Categorias?.Include(p => p.Tareas));
    //return Results.Ok(dbContext.Categorias);
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) => {
    //Agregando campos por si no vienen en el body
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;

    //Dos formas de agregar. Directamente al contexto o al objeto del contexto
    await dbContext.AddAsync(tarea);
    //await dbContext.Tareas.AddAsync(tarea);

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) => {
    if (dbContext.Tareas == null) {
        return Results.Problem();
    }

    var tareaActual = await dbContext.Tareas.FindAsync(id);

    if (tareaActual != null) {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PriodidadTarea = tarea.PriodidadTarea;
        tareaActual.Descripcion = tarea.Descripcion;
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) => {
    if (dbContext.Tareas == null) {
        return Results.Problem();
    }

    var tareaEliminar = await dbContext.Tareas.FindAsync(id);

    if (tareaEliminar != null) {
        dbContext.Tareas.Remove(tareaEliminar);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();

    /*
    dbContext.Tareas.FromSqlRaw(“SELECT * FROM Tarea”).ToList(); //Si se quiere usar SQL
    */
});

app.Run();
