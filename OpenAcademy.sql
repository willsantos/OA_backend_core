CREATE TABLE `Usuario` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `email` VARCHAR(50) UNIQUE NOT NULL,
  `senha` VARCHAR(20) NOT NULL,
  `data_nascimento` DATE NOT NULL,
  `telefone` VARCHAR(15),
  `endereco` VARCHAR(100)
);

CREATE TABLE `Curso` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `categoria` VARCHAR(20) NOT NULL,
  `pre_requisito` VARCHAR(100),
  `preco` DECIMAL(10,2) NOT NULL,
  `professor_id` INT
);

CREATE TABLE `Aluno` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `usuario_id` UNIQUEIDENTIFIER
);

CREATE TABLE `Professor` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `usuario_id` UNIQUEIDENTIFIER,
  `formacao` VARCHAR(50) NOT NULL,
  `experiencia` VARCHAR(100) NOT NULL,
  `foto` VARCHAR(MAX),
  `biografia` VARCHAR(200) NOT NULL
);

CREATE TABLE `Aula` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `caminho` VARCHAR(MAX) NOT NULL,
  `tipo` ENUM ('video', 'texto', 'audio', 'outro') NOT NULL,
  `duracao` INT NOT NULL,
  `ordem` INT NOT NULL,
  `curso_id` UNIQUEIDENTIFIER
);

CREATE TABLE `Avaliacao` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `tipo` ENUM ('prova', 'questionario') NOT NULL,
  `nota_maxima` DECIMAL(5,2) NOT NULL,
  `nota_minima` DECIMAL(5,2) NOT NULL,
  `data_entrega` DATE NOT NULL,
  `aula_id` UNIQUEIDENTIFIER
);

CREATE TABLE `Questao` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `enunciado` VARCHAR(200) NOT NULL,
  `alternativa_a` VARCHAR(50) NOT NULL,
  `alternativa_b` VARCHAR(50) NOT NULL,
  `alternativa_c` VARCHAR(50) NOT NULL,
  `alternativa_d` VARCHAR(50) NOT NULL,
  `alternativa_e` VARCHAR(50) NOT NULL,
  `resposta_correta` ENUM ('a', 'b', 'c', 'd', 'e') NOT NULL,
  `dificuldade` INT NOT NULL,
  `avaliacao_id` UNIQUEIDENTIFIER
);

CREATE TABLE `Resultado` (
  `avaliacao_id` UNIQUEIDENTIFIER,
  `aluno_id` UNIQUEIDENTIFIER,
  `nota_obtida` DECIMAL(5,2) NOT NULL,
  PRIMARY KEY (`avaliacao_id`, `aluno_id`)
);

CREATE TABLE `Certificado` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `carga_horaria` INT NOT NULL,
  `assinatura` VARBINARY(MAX) NOT NULL,
  `selo_caminho` VARCHAR(MAX) NOT NULL
);

CREATE TABLE `Certificado_Aluno` (
  `id` UNIQUEIDENTIFIER PRIMARY KEY,
  `data_emissao` DATE NOT NULL,
  `curso_id` UNIQUEIDENTIFIER,
  `aluno_id` UNIQUEIDENTIFIER,
  `certificado_id` UNIQUEIDENTIFIER
);

ALTER TABLE `Curso` ADD FOREIGN KEY (`professor_id`) REFERENCES `Usuario` (`id`);

ALTER TABLE `Aluno` ADD FOREIGN KEY (`usuario_id`) REFERENCES `Usuario` (`id`);

ALTER TABLE `Professor` ADD FOREIGN KEY (`usuario_id`) REFERENCES `Usuario` (`id`);

ALTER TABLE `Aula` ADD FOREIGN KEY (`curso_id`) REFERENCES `Curso` (`id`);

ALTER TABLE `Avaliacao` ADD FOREIGN KEY (`aula_id`) REFERENCES `Aula` (`id`);

ALTER TABLE `Questao` ADD FOREIGN KEY (`avaliacao_id`) REFERENCES `Avaliacao` (`id`);

ALTER TABLE `Resultado` ADD FOREIGN KEY (`avaliacao_id`) REFERENCES `Avaliacao` (`id`);

ALTER TABLE `Resultado` ADD FOREIGN KEY (`aluno_id`) REFERENCES `Aluno` (`id`);

ALTER TABLE `Certificado_Aluno` ADD FOREIGN KEY (`curso_id`) REFERENCES `Curso` (`id`);

ALTER TABLE `Certificado_Aluno` ADD FOREIGN KEY (`aluno_id`) REFERENCES `Aluno` (`id`);

ALTER TABLE `Certificado_Aluno` ADD FOREIGN KEY (`certificado_id`) REFERENCES `Certificado` (`id`);
