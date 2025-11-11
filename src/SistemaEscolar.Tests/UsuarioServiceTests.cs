using Moq;
using SistemaEscolar.Repositories;
using SistemaEscolar.Repositories.Entities;
using SistemaEscolar.Services;
using SistemaEscolar.Services.Enums;

namespace SistemaEscolar.Tests
{
    [TestClass]
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        private readonly IUsuarioService _usuarioService;
        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        //Testanto o método ValidarLogin, se o Login é valido e que retorna o Usuario deste Login
        [TestMethod]
        public void ValidarLogin_LoginValido_RetornaUsuario()
        {
            //Arrange
            var login = "administrador";
            var senha = "senha123";

            //Criação objeto usuario
            var usuario = new Usuario //É uma Entitie
            {
                Id = 1,
                Login = login,
                Senha = senha,
                FuncaoId = (int)Funcao.Administrador
            };

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns(usuario);

            //Act Vai fazer disparar o método que está sendo testado
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once); //Verifica se o método foi chamado e só pode 1 vez(Validação)

            Assert.IsTrue(
                result != null &&
                result.Sucesso == true &&
                result.Usuario != null &&
                result.Usuario.Login == login);
        }

        //Testanto o método ValidarLogin_SenhaInvalida_RetornaErro, se a senha é inválida e que retorna a mensagem de erro
        [TestMethod]
        public void ValidarLogin_SenhaInvalida_RetornaErro()
        {
            //Arrange
            var login = "administrador";
            var senha = "senha123";


            //Criação objeto usuario
            var usuario = new Usuario //É uma Entitie
            {
                Id = 1,
                Login = login,
                Senha = "123",//Senha ficticia diferente da senhaValida
                FuncaoId = (int)Funcao.Administrador
            };

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns(usuario);

            //Act Vai fazer disparar o método que está sendo testado
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once); //Verifica se o método foi chamado e só pode 1 vez(Validação)
            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.MensagemErro == "Senha inválida");
        }

        //Testanto o método ValidarLogin_LoginInvalido_RetornaErro, se o usuário não é encontrado e que retorna a mensagem de erro
        [TestMethod]
        public void ValidarLogin_LoginInvalido_RetornaErro()
        {
            //Arrange
            var login = "administrador";
            var senha = "senha123";

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns((Usuario?)null); //Retorna nulo para simular usuário não encontrado

            //Act Vai fazer disparar o método que está sendo testado
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once); //Verifica se o método foi chamado e só pode 1 vez(Validação)

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.MensagemErro == "Usuário não encontrado");
        }

        //Testanto o método ValidarLogin_LoginVazio_RetornaErro, se o campo usuário(Login) está vazio e que retorna a mensagem de erro
        [TestMethod]
        public void ValidarLogin_LoginVazio_RetornaErro()
        {
            //Arrange
            var login = "";
            var senha = "senha123";

            //Act Vai fazer disparar o método que está sendo testado
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Never); //Se o campo login estiver vazio, o método ObterPorLogin(login) não deve ser chamado

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.Usuario == null &&
                result.MensagemErro == "Usuário é obrigatório");
        }

        //Testanto o método ValidarLogin_SenhaVazia_RetornaErro, se o campo senha está vazio e que retorna a mensagem de erro
        [TestMethod]
        public void ValidarLogin_SenhaVazia_RetornaErro()
        {
            //Arrange
            var login = "administrador";
            var senha = "";

            //Act Vai fazer disparar o método que está sendo testado
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Never); //Se o campo senha estiver vazio, o método ObterPorLogin(login) não deve ser chamado

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.Usuario == null &&
                result.MensagemErro == "Senha é obrigatória");
        }
    }
}