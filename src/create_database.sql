CREATE SCHEMA IF NOT EXISTS `sistema_escolar` DEFAULT CHARACTER SET utf8;

CREATE TABLE IF NOT EXISTS `sistema_escolar`.`funcao` (
  `funcao_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NULL,
  PRIMARY KEY (`funcao_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_escolar`.`usuario` (
  `usuario_id` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(45) NOT NULL,
  `senha` VARCHAR(45) NOT NULL,
  `funcao_id` INT NOT NULL,
  PRIMARY KEY (`usuario_id`),
  CONSTRAINT `usuario_funcao`
    FOREIGN KEY (`funcao_id`)
    REFERENCES `sistema_escolar`.`funcao` (`funcao_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_escolar`.`aluno` (
  `aluno_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`aluno_id`),
  CONSTRAINT `aluno_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `sistema_escolar`.`usuario` (`usuario_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_escolar`.`professor` (
  `professor_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`professor_id`),
  CONSTRAINT `professor_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `sistema_escolar`.`usuario` (`usuario_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_escolar`.`turma` (
  `turma_id` INT NOT NULL AUTO_INCREMENT,
  `semestre` INT NOT NULL,
  `ano` INT NOT NULL,
  `periodo` VARCHAR(150) NOT NULL,
  `nivel` VARCHAR(45) NOT NULL,
  `professor_id` INT NOT NULL,
  PRIMARY KEY (`turma_id`),
  CONSTRAINT `turma_professor`
    FOREIGN KEY (`professor_id`)
    REFERENCES `sistema_escolar`.`professor` (`professor_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_escolar`.`aluno_turma_boletim` (
  `aluno_turma_boletim_id` INT NOT NULL AUTO_INCREMENT,
  `nota_bim1_escrita` DECIMAL(4,2) NULL,
  `nota_bim1_leitura` DECIMAL(4,2) NULL,
  `nota_bim1_conversacao` DECIMAL(4,2) NULL,
  `nota_bim1_final` DECIMAL(4,2) NULL,
  `nota_bim2_leitura` DECIMAL(4,2) NULL,
  `nota_bim2_escrita` DECIMAL(4,2) NULL,
  `nota_bim2_conversacao` DECIMAL(4,2) NULL,
  `nota_bim2_final` DECIMAL(4,2) NULL,
  `nota_final_semestre` DECIMAL(4,2) NULL,
  `faltas_semestre` INT NULL,
  `aluno_id` INT NOT NULL,
  `turma_id` INT NOT NULL,
  PRIMARY KEY (`aluno_turma_boletim_id`),
  CONSTRAINT `aluno_boletim`
    FOREIGN KEY (`aluno_id`)
    REFERENCES `sistema_escolar`.`aluno` (`aluno_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `turma_boletim`
    FOREIGN KEY (`turma_id`)
    REFERENCES `sistema_escolar`.`turma` (`turma_id`))
ENGINE = InnoDB;

INSERT INTO `sistema_escolar`.`funcao`
(`funcao_id`,
`nome`)
VALUES
(1,
'Administrador');

INSERT INTO `sistema_escolar`.`funcao`
(`funcao_id`,
`nome`)
VALUES
(2,
'Professor');

INSERT INTO `sistema_escolar`.`funcao`
(`funcao_id`,
`nome`)
VALUES
(3,
'Aluno');

INSERT INTO `sistema_escolar`.`usuario`
(`usuario_id`,
`login`,
`senha`,
`funcao_id`)
VALUES
(1,
'admin',
'123',
1);

