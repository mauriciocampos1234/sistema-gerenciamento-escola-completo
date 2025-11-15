# 💼 Projeto – Visão Geral

## 📸 Prévia do Sistema

<p align="center">
  <img src="./image/f1.png" alt="Prévia 1" width="600px"/>
  <br><br>
  <img src="./image/f2.png" alt="Prévia 2" width="600px"/>
</p>

---

## 🧩 Sobre o Projeto
Este repositório apresenta uma solução desenvolvida com foco em organização, performance e boas práticas.  
As imagens acima demonstram partes do fluxo e da interface do sistema.

---

# 📘 Detalhamento de Requisitos

## 🎯 Objetivo Geral
Construir um sistema de gestão para a escola **EnglishSchool**, com acesso restrito a administradores, professores e alunos, utilizando **login e senha**.  
Cada perfil possui funcionalidades específicas, garantindo segurança e organização no processo de gestão acadêmica.

---

## 📝 Objetivos Específicos

- Garantir acesso seguro por meio de credenciais (login e senha).
- Permitir que **administradores** cadastrem:
  - Turmas  
  - Professores  
  - Alunos  
- Permitir que **professores** registrem notas e faltas de seus alunos.
- Permitir que **alunos** consultem suas notas e faltas de maneira rápida e objetiva.

### 🧑‍🏫 Cadastro de Usuários

- **Aluno**  
  - Nome  
  - Email  

- **Professor**  
  - Nome  
  - Email  

---

## 📏 Regras de Negócio

- As turmas são **semestrais**.  
  - Exemplo: *2ºsem/2024*

- As turmas possuem **níveis**:  
  - Básico  
  - Intermediário  
  - Avançado  

- As turmas possuem **períodos**.  
  - Exemplo: *terça e quinta 19h*, *sábado 9h*, *segunda e quarta 15h*

- As provas são **bimestrais**, com notas de **0 a 10**.

- Cada aluno faz **3 provas**:
  - Leitura  
  - Escrita  
  - Conversação  

- **Nota Final do Bimestre** = média das 3 provas  
- **Nota Final do Semestre** = média dos dois bimestres

- Critérios:
  - Nota final < 6 → **Reprovado**  
  - Nota final ≥ 6 → **Aprovado**

---

## 📚 Exemplo Ilustrativo

**Turma:** Nível Básico – 2ºsem/2024, Segunda e Quinta, 15:45  
**Professor:** Maria Antônia  
**Alunos:**

---

### 👤 José Almeida da Silva

**1º Bimestre**  
- Leitura: 6  
- Escrita: 8  
- Conversação: 10  
- **Nota Final:** 8  

**2º Bimestre**  
- Leitura: 2  
- Escrita: 4  
- Conversação: 6  
- **Nota Final:** 4  

**Nota Final do Semestre:** **6 — Aprovado**

---

### 👤 Ana Luiza

**1º Bimestre**  
- Leitura: 5  
- Escrita: 4  
- Conversação: 3  
- **Nota Final:** 4  

**2º Bimestre**  
- Leitura: 2  
- Escrita: 4  
- Conversação: 6  
- **Nota Final:** 4  

**Nota Final do Semestre:** **4 — Reprovado**

---

# 🧩 Funcionalidades e Perfis de Acesso

## 👨‍💼 Administrador

- Acesso restrito via login e senha  
- Gerenciar professores:
  - Inserir  
  - Visualizar  
  - Editar  
  - Excluir  
  - Dados: **nome completo, email, login, senha**
- Gerenciar alunos:
  - Inserir  
  - Visualizar  
  - Editar  
  - Excluir  
  - Dados: **nome completo, email, login, senha**
- Gerenciar turmas:
  - Inserir  
  - Visualizar  
  - Editar  
  - Excluir  
  - Dados: professor, semestre, ano, nível, período
- Associar e desassociar alunos às turmas
- Visualizar **notas e faltas** de todos os alunos em todas as turmas

---

## 👨‍🏫 Professor

- Acesso restrito via login e senha  
- Visualizar suas turmas e respectivos alunos  
- Registrar **notas e faltas** dos seus alunos  

---

## 👨‍🎓 Aluno

- Acesso restrito via login e senha  
- Visualizar:
  - Turmas atuais  
  - Histórico de turmas  
  - Suas notas e faltas  

---

# 🌐 Conecte-se Comigo

<table>
  <tr>
    <td><strong>LinkedIn</strong></td>
    <td><a href="www.linkedin.com/in/mauricio-campos-dev-full-stack" target="_blank">Acessar Perfil</a></td>
  </tr>
  <tr>
    <td><strong>WhatsApp</strong></td>
    <td><a href="https://wa.me/5512991020922" target="_blank">Enviar Mensagem</a></td>
  </tr>
  <tr>
    <td><strong>App Notion</strong></td>
    <td><a href="https://www.notion.so/276051526fff80e1aa1fd222916c13fd?v=276051526fff81f0972f000c21386f34&source=copy_link" target="_blank">Ver Arquitetura do Projeto</a></td>
  </tr>
  <tr>
    <td><strong>Meu Site Pessoal</strong></td>
    <td><a href="https://site-mauricio-campos.vercel.app/" target="_blank">Ver Arquitetura do Projeto</a></td>
  </tr>
</table>

---
