using SistemaEscolar.Repositories;
using SistemaEscolar.Services.Enums;
using SistemaEscolar.Services.Mappings;
using SistemaEscolar.Services.Models.Professor;

namespace SistemaEscolar.Services
{
    //Injeção de dependência
    public interface IProfessorService
    {
        CriarProfessorResult Criar(CriarProfessorRequest request); // Método para criar um professor (Assinatura)
    }
    public class ProfessorService : IProfessorService
    {
        // Repositório de professor (Injeção de dependência)
        private readonly IProfessorRepository _professorRepository; // Declaração da variável do repositório de professor

        // Repositório de usuario (Injeção de dependência)
        private readonly IUsuarioRepository _usuarioRepository; // Declaração da variável do repositório de usuario

        // Construtor que recebe o repositório de professor via injeção de dependência
        public ProfessorService(IProfessorRepository professorRepository, IUsuarioRepository usuarioRepository)
        {
            // Atribuição do repositório de professor à variável local
            _professorRepository = professorRepository;
            _usuarioRepository = usuarioRepository;

        }

        // Método para criar um professor
        public CriarProfessorResult Criar(CriarProfessorRequest request)
        {
            //retornar o objeto de resultado
            var result = new CriarProfessorResult();

            //Validando se o usuario já existe no Banco de Dados
            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null)
            {
                //Retornar erro
                result.MensagemErro = "Usuário já existe.";
                return result;
            }


            //Inserir o usuário (Primeira Lógica para criar um usuário deve ser implementada aqui)
            // Depois, criar o professor associado ao usuário
            //var usuarioId = _usuarioRepository.Inserir(new Repositories.Entities.Usuario
            //{
            //Login = request.Login,
            //Senha = request.Senha,
            //FuncaolId = (int)Funcao.Professor // Definindo a função como Professor
            //});

            //Segunda Lógica Usando o Mapping para mapear o Request para o Usuario
            var usuarioId = _usuarioRepository.Inserir(request.MapToUsuario());

            if (!usuarioId.HasValue)
            {
                //Retornar erro
                result.MensagemErro = "Erro ao inserir o usuário.";
                return result;
            }


            // Primeira Lógica para inserir um professor
            //_professorRepository.Inserir(new Repositories.Entities.Professor
            //{
            //Nome = request.Nome,
            //Email = request.Email,
            //UsuarioId = usuarioId.Value.ToString() // Associando o professor ao usuário criado
            //});

            // Segunda Lógica Usando o Mapping para mapear o Request para o Professor
            _professorRepository.Inserir(request.MapToProfessorR(usuarioId.Value));

            result.Sucesso = true;
            return result;
        }
    }
}
