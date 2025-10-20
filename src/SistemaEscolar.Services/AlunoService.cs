using SistemaEscolar.Repositories;
using SistemaEscolar.Services.Mappings;
using SistemaEscolar.Services.Models.Aluno;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEscolar.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AlunoService(IAlunoRepository alunoRepository, IUsuarioRepository usuarioRepository)
        {
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public CriarAlunoResult Criar(CriarAlunoRequest request)
        {
            var result = new CriarAlunoResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null)
            {
                result.MensagemErro = "Usuário já existe.";
                return result;
            }

            var usuarioId = _usuarioRepository.Inserir(request.MapToUsuario());

            if (!usuarioId.HasValue)
            {
                result.MensagemErro = "Erro ao inserir o usuário.";
                return result;
            }

            _alunoRepository.Inserir(request.MapToAlunoR(usuarioId.Value));

            result.Sucesso = true;
            return result;
        }

        public EditarAlunoResult Editar(EditarAlunoRequest request)
        {
            var result = new EditarAlunoResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null && usuarioExistente.Id != request.UsuarioId)
            {
                result.MensagemErro = "Já existe outro usuário com este login.";
                return result;
            }

            var aluno = request.MapToAluno();

            var affectedRows = _alunoRepository.Atualizar(aluno);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o aluno.";
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

        public ExcluirAlunoResult Excluir(int id)
        {
            var result = new ExcluirAlunoResult();

            var aluno = _alunoRepository.ObterPorId(id);

            if (aluno == null)
            {
                result.MensagemErro = "Aluno não existe.";
                return result;
            }

            var affectedRows = _alunoRepository.Apagar(id);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o aluno.";
                return result;
            }

            affectedRows = _usuarioRepository.Apagar(aluno.UsuarioId);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o usuário associado.";
                return result;
            }

            result.Sucesso = true;
            return result;
        }

        public IList<AlunoResult> Listar()
        {
            var alunos = _alunoRepository.Listar();
            var result = alunos.Select(a => a.MapToAlunoResult()).ToList();
            return result;
        }

        public AlunoResult? ObterPorId(int id)
        {
            var aluno = _alunoRepository.ObterPorId(id);
            if (aluno == null) return null;
            return aluno.MapToAlunoResult();
        }
    }
}