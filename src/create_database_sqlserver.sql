-- Criação do banco de dados e schema para SQL Server
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'sistema_escolar')
BEGIN
    CREATE DATABASE sistema_escolar;
END
GO

USE sistema_escolar;
GO

-- Criação da tabela funcao
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'funcao' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[funcao] (
        [funcao_id] INT NOT NULL IDENTITY(1,1),
        [nome] VARCHAR(45) NULL,
        PRIMARY KEY ([funcao_id])
    );
END
GO

-- Criação da tabela usuario
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'usuario' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[usuario] (
        [usuario_id] INT NOT NULL IDENTITY(1,1),
        [login] VARCHAR(45) NOT NULL,
        [senha] VARCHAR(45) NOT NULL,
        [funcao_id] INT NOT NULL,
        PRIMARY KEY ([usuario_id]),
        CONSTRAINT [usuario_funcao]
            FOREIGN KEY ([funcao_id])
            REFERENCES [dbo].[funcao] ([funcao_id])
    );
END
GO

-- Criação da tabela aluno
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'aluno' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[aluno] (
        [aluno_id] INT NOT NULL IDENTITY(1,1),
        [nome] VARCHAR(200) NOT NULL,
        [email] VARCHAR(45) NOT NULL,
        [usuario_id] INT NOT NULL,
        PRIMARY KEY ([aluno_id]),
        CONSTRAINT [aluno_usuario]
            FOREIGN KEY ([usuario_id])
            REFERENCES [dbo].[usuario] ([usuario_id])
    );
END
GO

-- Criação da tabela professor
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'professor' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[professor] (
        [professor_id] INT NOT NULL IDENTITY(1,1),
        [nome] VARCHAR(200) NOT NULL,
        [email] VARCHAR(45) NOT NULL,
        [usuario_id] INT NOT NULL,
        PRIMARY KEY ([professor_id]),
        CONSTRAINT [professor_usuario]
            FOREIGN KEY ([usuario_id])
            REFERENCES [dbo].[usuario] ([usuario_id])
    );
END
GO

-- Criação da tabela turma
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'turma' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[turma] (
        [turma_id] INT NOT NULL IDENTITY(1,1),
        [semestre] INT NOT NULL,
        [ano] INT NOT NULL,
        [periodo] VARCHAR(150) NOT NULL,
        [nivel] VARCHAR(45) NOT NULL,
        [professor_id] INT NOT NULL,
        PRIMARY KEY ([turma_id]),
        CONSTRAINT [turma_professor]
            FOREIGN KEY ([professor_id])
            REFERENCES [dbo].[professor] ([professor_id])
    );
END
GO

-- Criação da tabela aluno_turma_boletim
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'aluno_turma_boletim' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[aluno_turma_boletim] (
        [aluno_turma_boletim_id] INT NOT NULL IDENTITY(1,1),
        [nota_bim1_escrita] DECIMAL(4,2) NULL,
        [nota_bim1_leitura] DECIMAL(4,2) NULL,
        [nota_bim1_conversacao] DECIMAL(4,2) NULL,
        [nota_bim1_final] DECIMAL(4,2) NULL,
        [nota_bim2_leitura] DECIMAL(4,2) NULL,
        [nota_bim2_escrita] DECIMAL(4,2) NULL,
        [nota_bim2_conversacao] DECIMAL(4,2) NULL,
        [nota_bim2_final] DECIMAL(4,2) NULL,
        [nota_final_semestre] DECIMAL(4,2) NULL,
        [faltas_semestre] INT NULL,
        [aluno_id] INT NOT NULL,
        [turma_id] INT NOT NULL,
        PRIMARY KEY ([aluno_turma_boletim_id]),
        CONSTRAINT [aluno_boletim]
            FOREIGN KEY ([aluno_id])
            REFERENCES [dbo].[aluno] ([aluno_id])
            ON DELETE NO ACTION
            ON UPDATE NO ACTION,
        CONSTRAINT [turma_boletim]
            FOREIGN KEY ([turma_id])
            REFERENCES [dbo].[turma] ([turma_id])
    );
END
GO

-- Inserção dos dados iniciais na tabela funcao
IF NOT EXISTS (SELECT * FROM [dbo].[funcao] WHERE [funcao_id] = 1)
BEGIN
    SET IDENTITY_INSERT [dbo].[funcao] ON;
    INSERT INTO [dbo].[funcao]
    ([funcao_id],
    [nome])
    VALUES
    (1,
    'Administrador');
    SET IDENTITY_INSERT [dbo].[funcao] OFF;
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[funcao] WHERE [funcao_id] = 2)
BEGIN
    SET IDENTITY_INSERT [dbo].[funcao] ON;
    INSERT INTO [dbo].[funcao]
    ([funcao_id],
    [nome])
    VALUES
    (2,
    'Professor');
    SET IDENTITY_INSERT [dbo].[funcao] OFF;
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[funcao] WHERE [funcao_id] = 3)
BEGIN
    SET IDENTITY_INSERT [dbo].[funcao] ON;
    INSERT INTO [dbo].[funcao]
    ([funcao_id],
    [nome])
    VALUES
    (3,
    'Aluno');
    SET IDENTITY_INSERT [dbo].[funcao] OFF;
END
GO

-- Inserção do usuário inicial
IF NOT EXISTS (SELECT * FROM [dbo].[usuario] WHERE [usuario_id] = 1)
BEGIN
    SET IDENTITY_INSERT [dbo].[usuario] ON;
    INSERT INTO [dbo].[usuario]
    ([usuario_id],
    [login],
    [senha],
    [funcao_id])
    VALUES
    (1,
    'admin',
    '123',
    1);
    SET IDENTITY_INSERT [dbo].[usuario] OFF;
END
GO

