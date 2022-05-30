public class UsuarioController
{
    public UsuarioController(WebApplication app)
    {
        app.MapGet("/v1/usuarios", (AppDbContext context) =>
        {
            var usuarios = context.Usuario;
            return usuarios is not null ? Results.Ok(usuarios) : Results.NotFound();
        });

        app.MapGet("/v1/usuarios/{id}", (AppDbContext context, String id) =>
        {
            var usuario = context.Usuario.Find(id);

            if (usuario is null)
            {
                return Results.BadRequest("Usuário não encontrado");
            }

            return Results.Ok(usuario);
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

        app.MapPut("/v1/usuarios/{id}", (AppDbContext context, DtoUsuarioUpdate model, String id) =>
        {
            var usuario = model.MapTo();
            if (!model.IsValid)
            {
                return Results.BadRequest(model.Notifications);
            }

            var usuarioUpdated = context.Usuario.Find(id);

            if (usuarioUpdated is null)
            {
                return Results.BadRequest("Usuário não encontrado");
            }

            usuarioUpdated.Nome = usuario.Nome;
            usuarioUpdated.Email = usuario.Email;
            context.SaveChanges();

            return Results.Accepted($"/v1/todos/{usuarioUpdated.Id}", usuarioUpdated);
        });

        app.MapDelete("/v1/usuarios/{id}", (AppDbContext context, String id) =>
        {
            var usuarioUpdated = context.Usuario.Find(id);

            if (usuarioUpdated is null)
            {
                return Results.BadRequest("Usuário não encontrado");
            }
            context.Usuario.Remove(usuarioUpdated);
            context.SaveChanges();

            return Results.Accepted($"/v1/todos/{usuarioUpdated.Id}", "Registro deletado");
        });
    }
}

