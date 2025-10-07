namespace SistemaEscolar.Services.Models.Usuario
{
    public class ValidarLoginResult
    {
        public bool Sucesso { get; set; }

        public string? MensagemErro { get; set; } // Pode ser nulo se Sucesso for true
    }
}
