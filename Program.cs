var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapGet("/v1/usuarios", (AppDbContext context) =>
{
    var usuarios = context.Usuario;
    return usuarios is not null ? Results.Ok(usuarios) : Results.NotFound();
});

app.MapPost("/v1/usuarios", (AppDbContext context, DtoUsuarioCreate model) =>
{
    var usuario = model.MapTo();
    if (!model.IsValid)
    {
        return Results.BadRequest(model.Notifications);
    }

    context.Usuario.Add(usuario);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{usuario.Id}", usuario);
});

app.MapPut("/v1/usuarios/{id}", (AppDbContext context, DtoUsuarioUpdate model) =>
{
    var usuario = model.MapTo();
    if (!model.IsValid)
    {
        return Results.BadRequest(model.Notifications);
    }

    var usuarioUpdated = context.Usuario.Find(usuario.Id);
    //usuarioUpdated.Nome = usuario.Nome;
    //usuarioUpdated.Email = usuario.Email;    
    context.SaveChanges();

    return Results.Created($"/v1/todos/{usuario.Id}", usuario);
});

app.Run();

