using Flunt.Notifications;
using Flunt.Validations;

public class DtoUsuarioUpdate : Notifiable<Notification>
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public UsuarioModel MapTo()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNull(Nome, "Informe o nome do usuário")
            .IsGreaterThan(Nome, 5, "O nome deve conter mais de 5 caracteres"));

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNull(Email, "Informe o e-mail da tarefa")
            .IsEmail(Email, "Informe um e-mail válido"));

        return new UsuarioModel(Guid.NewGuid().ToString(), Nome, Email, false);
    }
}
