# Configuração de User Secrets

As connection strings do banco de dados devem ser configuradas no User Secrets por questões de segurança.

## Como configurar os User Secrets

### Via CLI (Recomendado)

1. Navegue até a pasta do projeto `SistemaEscolar.web`
2. Execute os seguintes comandos:

**Para MySQL:**
```bash
dotnet user-secrets set "ConnectionStrings:SistemaEscolarConnectionString" "Server=localhost;Database=sistemaescolar;Uid=seu_usuario;Pwd=sua_senha;"
```

**Para SQL Server:**
```bash
dotnet user-secrets set "ConnectionStrings:SistemaEscolarConnectionStringSqlServer" "Server=localhost;Database=sistemaescolar;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
```

### Via Visual Studio

1. Clique com o botão direito no projeto `SistemaEscolar.web`
2. Selecione "Manage User Secrets"
3. Adicione as connection strings no formato JSON:

```json
{
  "ConnectionStrings": {
    "SistemaEscolarConnectionString": "Server=localhost;Database=sistemaescolar;Uid=seu_usuario;Pwd=sua_senha;",
    "SistemaEscolarConnectionStringSqlServer": "Server=localhost;Database=sistemaescolar;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
  }
}
```

## Configuração do Database Provider

O provider do banco de dados é configurado no `appsettings.json`:

- `"DatabaseProvider": "MySql"` - Para usar MySQL
- `"DatabaseProvider": "SqlServer"` - Para usar SQL Server

## Exemplo de Connection Strings

### MySQL
```
Server=localhost;Database=sistemaescolar;Uid=root;Pwd=senha123;
```

### SQL Server
```
Server=localhost;Database=sistemaescolar;User Id=sa;Password=senha123;TrustServerCertificate=True;
```

## Nota Importante

⚠️ **Nunca commite as connection strings no código fonte!** Elas devem estar apenas nos User Secrets ou variáveis de ambiente.

