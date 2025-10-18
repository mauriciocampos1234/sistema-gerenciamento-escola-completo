using SistemaEscolar.Repositories;
using SistemaEscolar.Services.Mappings;
using SistemaEscolar.Services.Models.Professor;

namespace SistemaEscolar.Services
{
    //Injeção de dependência
    public interface IProfessorService
    {
        CriarProfessorResult Criar(CriarProfessorRequest request); // Método para criar um professor (Assinatura)

        EditarProfessorResult Editar(EditarProfessorRequest request); // Método para editar um professor (Assinatura)

        ExcluirProfessorResult Excluir(int id);

        IList<ProfessorResult> Listar(); // Método para listar todos os professores (Assinatura)

        ProfessorResult? ObterPorId(int id); // Método para obter um professor pelo ID (Assinatura)
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

        public EditarProfessorResult Editar(EditarProfessorRequest request)
        {
            //Fazer verificação na hora de editar se nenhum outro usuario possue o mesmo login quando bate no BD
            var result = new EditarProfessorResult();
            
            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);
            
            if (usuarioExistente != null && usuarioExistente.Id != request.UsuarioId)
            {
                result.MensagemErro = "Já existe outro Usuário com este Login.";

                return result;
            }
            //Mapear o EditarProfessorRequest para a entidade Professor usando o Mapping
            var professor = request.MapToProfessor();

            //Atualizar o usuário (Primeira Lógica para atualizar um usuário deve ser implementada aqui)
            var affectedRows = _professorRepository.Atualizar(professor);
            
            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o professor.";
                return result;
            }

            var usuario = request.MapToUsuario();

            affectedRows = _usuarioRepository.Atualizar(usuario);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o usuário.";
                return result;
            }

            result.Sucesso = true;

            return result;

        }

        public ExcluirProfessorResult Excluir(int id)
        {
            var result = new ExcluirProfessorResult();

            var professor = _professorRepository.ObterPorId(id);

            if (professor == null)
            {
                result.MensagemErro = "Professor não existe.";
                return result;
            }

            var affectedRows = _professorRepository.Apagar(id);
            
            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o professor.";
                return result;
            }

            //Excluir o usuário associado ao professor
            affectedRows = _usuarioRepository.Apagar(professor.UsuarioId);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o usuário.";
                return result;
            }


            result.Sucesso = true;
            
            return result;

        }

        public IList<ProfessorResult> Listar()
        {
            var professores = _professorRepository.Listar();

            // Mapear a lista de entidades Professor para ProfessorResult usando o Mapping
            var result = professores.Select(p => p.MapToProfessorResult()).ToList();

            return result;

        }

        public ProfessorResult? ObterPorId(int id)
        {
            var professor = _professorRepository.ObterPorId(id); // Chama o repositório para obter o professor pelo ID

            if (professor == null) 
                return null; // Retorna null(Vazio) se o professor não for encontrado

                var result = professor.MapToProfessorResult(); // Mapeia a entidade para ProfessorResult usando o Mapping

                return result;
            
        }
    }
}
