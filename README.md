A documentação da API está disponível em: https://people-registration-challenge-backend.onrender.com/swagger/index.html

## 🧱 Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- H2 Database (EF Core in Memory)
- Swagger (Swashbuckle)
- Autenticação JWT
- XUnit (para testes automatizados)

## 📦 Funcionalidades

- Cadastro de nova pessoa
- Edição de dados existentes
- Exclusão de pessoa
- Consulta de pessoas
- Versão 2 com suporte a endereço obrigatório
- Autenticação via JWT

## 📋 Campos da Pessoa

| Campo               | Obrigatório | Observações                                      |
|--------------------|-------------|--------------------------------------------------|
| Nome               | ✅          |                                                  |
| Sexo               | ❌          |                                                  |
| E-mail             | ❌          | Validado se fornecido                            |
| Data de Nascimento | ✅          | Validação de data                                |
| Naturalidade       | ❌          |                                                  |
| Nacionalidade      | ❌          |                                                  |
| CPF                | ✅          | Formato e unicidade validados                    |
| Data de Cadastro   | Automático  | Gerado automaticamente no momento do cadastro    |
| Data de Atualização| Automático  | Atualizado automaticamente em cada modificação   |                                          

> Na versão 2 da API, o campo **Endereço** é obrigatório.

## 🛡️ Autenticação

A API utiliza autenticação JWT. Apenas usuários previamente cadastrados podem acessar os endpoints protegidos.

## 🧪 Testes

Testes automatizados foram implementados utilizando XUnit, garantindo mais de 80% de cobertura de código.
