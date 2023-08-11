CREATE TABLE `Usuario` (
  `id` CHAR(38) PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `email` VARCHAR(50) UNIQUE NOT NULL,
  `senha` VARCHAR(20) NOT NULL,
  `data_nascimento` DATE NOT NULL,
  `telefone` VARCHAR(15),
  `endereco` VARCHAR(100)
);

CREATE TABLE `Aluno` (
  `id` CHAR(38) PRIMARY KEY,
  `usuario_id` CHAR(38)
);

CREATE TABLE `Professor` (
  `id` CHAR(38) PRIMARY KEY,
  `usuario_id` CHAR(38),
  `formacao` VARCHAR(50) NOT NULL,
  `experiencia` VARCHAR(100) NOT NULL,
  `foto` VARCHAR(255),
  `biografia` VARCHAR(200) NOT NULL
);

CREATE TABLE `Curso` (
  `id` CHAR(38) PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `categoria` VARCHAR(20) NOT NULL,
  `pre_requisito` VARCHAR(100),
  `preco` DECIMAL(10,2) NOT NULL,
  `professor_id` CHAR(38)
);

CREATE TABLE `Aula` (
  `id` CHAR(38) PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `caminho` VARCHAR(255) NOT NULL,
  `tipo` ENUM ('video', 'texto', 'audio', 'outro') NOT NULL,
  `duracao` INT NOT NULL,
  `ordem` INT NOT NULL,
  `curso_id` CHAR(38)
);

CREATE TABLE `Avaliacao` (
  `id` CHAR(38) PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `tipo` ENUM ('prova', 'questionario') NOT NULL,
  `nota_maxima` DECIMAL(5,2) NOT NULL,
  `nota_minima` DECIMAL(5,2) NOT NULL,
  `data_entrega` DATE NOT NULL,
  `aula_id` CHAR(38)
);

CREATE TABLE `Questao` (
  `id` CHAR(38) PRIMARY KEY,
  `enunciado` VARCHAR(200) NOT NULL,
  `alternativa_a` VARCHAR(50) NOT NULL,
  `alternativa_b` VARCHAR(50) NOT NULL,
  `alternativa_c` VARCHAR(50) NOT NULL,
  `alternativa_d` VARCHAR(50) NOT NULL,
  `alternativa_e` VARCHAR(50) NOT NULL,
  `resposta_correta` ENUM ('a', 'b', 'c', 'd', 'e') NOT NULL,
  `dificuldade` INT NOT NULL,
  `avaliacao_id` CHAR(38)
);

CREATE TABLE `Resultado` (
  `avaliacao_id` CHAR(38),
  `aluno_id` CHAR(38),
  `nota_obtida` DECIMAL(5,2) NOT NULL,
  PRIMARY KEY (`avaliacao_id`, `aluno_id`)
);

CREATE TABLE `Certificado` (
  `id` CHAR(38) PRIMARY KEY,
  `nome` VARCHAR(50) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `carga_horaria` INT NOT NULL,
  `assinatura` VARBINARY(255) NOT NULL,
  `selo_caminho` VARCHAR(255) NOT NULL
);

CREATE TABLE `Certificado_Aluno` (
  `id` CHAR(38) PRIMARY KEY,
  `data_emissao` DATE NOT NULL,
  `curso_id` CHAR(38),
  `aluno_id` CHAR(38),
  `certificado_id` CHAR(38)
);


ALTER TABLE `Aluno` ADD FOREIGN KEY (`usuario_id`) REFERENCES `Usuario` (`id`);

ALTER TABLE `Professor` ADD FOREIGN KEY (`usuario_id`) REFERENCES `Usuario` (`id`);

ALTER TABLE `Curso` ADD FOREIGN KEY (`professor_id`) REFERENCES `Usuario` (`id`);

ALTER TABLE `Aula` ADD FOREIGN KEY (`curso_id`) REFERENCES `Curso` (`id`);

ALTER TABLE `Avaliacao` ADD FOREIGN KEY (`aula_id`) REFERENCES `Aula` (`id`);

ALTER TABLE `Questao` ADD FOREIGN KEY (`avaliacao_id`) REFERENCES `Avaliacao` (`id`);

ALTER TABLE `Resultado` ADD FOREIGN KEY (`avaliacao_id`) REFERENCES `Avaliacao` (`id`);

ALTER TABLE `Resultado` ADD FOREIGN KEY (`aluno_id`) REFERENCES `Aluno` (`id`);

ALTER TABLE `Certificado_Aluno` ADD FOREIGN KEY (`curso_id`) REFERENCES `Curso` (`id`);

ALTER TABLE `Certificado_Aluno` ADD FOREIGN KEY (`aluno_id`) REFERENCES `Aluno` (`id`);

ALTER TABLE `Certificado_Aluno` ADD FOREIGN KEY (`certificado_id`) REFERENCES `Certificado` (`id`);
