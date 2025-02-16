using MinimalAPI.Dominio.Enuns;

namespace MinimalAPI.Dominio.ModelViews;

public record AdministradorLogado
{
    public string Email {get;set;} = default!;
    public String? Perfil {get;set;} = default!;

    public string Token {get;set;} = default!;
}