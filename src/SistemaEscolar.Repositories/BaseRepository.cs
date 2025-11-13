namespace SistemaEscolar.Repositories
{
    public class BaseRepository
    {
        //Atributo protegido para a string de conexão com o banco de dados
        private readonly string _connectionString;

        //Construtor padrão e recebe como parâmetro a string de conexão
        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Propriedade publica que retorna uma nova conexão com o banco de dados da propriedade privada _connectionString
        public string ConnectionString { get { return _connectionString; } }
    }
}