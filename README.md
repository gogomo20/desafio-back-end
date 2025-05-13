# Desafio Back-End - API .NET

Este projeto é uma API desenvolvida em .NET como parte de um desafio técnico de back-end. O objetivo é demonstrar habilidades com a construção de APIs RESTful, boas práticas de desenvolvimento e organização de código.

O projeto consiste em uma API para gerenciamento de carteiras digitais permitindo que o usuário:
  - Se autentique
  - adicione saldo a carteira
  - remova saldo da carteira
  - transfira saldo para outro usuário
  - consulte o historico de tranferencia podendo filtrar um range de datas

## Tecnologias Utilizadas

Bibliotecas e ambientes:
  - .NET8 ou superior
  - Entity Framework Core 
  - PostgresSQL
  - Swagger (para documentação da API)
  - MediatR
  - Auto Mapper
    
Padrões de projetos:
  - CQRS
  - Mediator  

## Como Executar o Projeto

1. **Clone este repositório:**

```bash
git clone https://github.com/gogomo20/desafio-back-end.git
cd desafio-back-end
```
2. **Autualize a conection string ** 
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=<dominio_do_banco>;Port=<porta_do_servidor>;Database=<nome_do_banco>;Username=<usuario>;Password=<senha>"
},
```
3. **Atualize a base de dados**
```bash
dotnet ef database update --project DesafioBackEnd.Persistense/ --startup-project DesafioBackEnd.API
```
4. **Execute o projeto**
```
dotnet run --project DesafioBackEnd.API
```
## Endpoints do projeto
 - Endpoints do Usuário
   
  | Método | Rota                 | Descrição                                 | Corpo (Body) obrigatório?| Permission |
  | ------ | -------------------- | ----------------------------------------- | ------------------------ |------------|
  | POST   | `/api/v1/auth/login` | Login do usuário                          | ✅                      | N/A         |
  | POST   | `/api/v1/user`       | Cria um novo usuário                      | ✅                      | CREATE_USER |
  | PUT    | `/api/v1/user/{id}`  | Atualizar cadastro do usuário             | ✅                      | UPDATE_USER |
  | GET    | `/api/v1/user/{id}`  | Consulta usuário cadastrado               | ❌                      | GET_USER    |
  | DELETE | `/api/v1/user/{id}`  | Deleta um usuário cadastrado              | ❌                      | DELETE_USER |
  | POST   | `/api/v1/user/list?page=0&size=10`  | Lista os usuários cadastrados| ✅                    | LIST_USER   |

 - Endpoints da carteira
   
  | Método | Rota                 | Descrição                                 | Corpo (Body) obrigatório?| Permission |
  | ------ | -------------------- | ----------------------------------------- | ------------------------ |------------|
  | GET    | `/api/v1/wallet/balance` | Consultar saldo na carteira           | ❌                      | GET_BALANCE |
  | PUT    | `/api/v1/wallet/deposit` | Depositar/Adicionar saldo na carteira | ✅                      | ADD_BALANCE |
  | PUT    | `/api/v1/wallet/withdraw`| Sacar/Remover saldo na carteira       | ✅                      | REMOVE_BALANCE |
  | POST   | `/api/v1/wallet/transfer`| Transferir saldo a um outro usuário   | ✅                      | CREATE_TRANSFERENCE |
  | POST   | `/api/v1/wallet/history-transference?page=0&size=10`| Transferir saldo a um outro usuário   | ✅          | CREATE_TRANSFERENCE |
  
  - consulta do body de cada endpoint pode ser encontrado nas rotas:
       - `/swagger/index.html`
  ## Observações 
   - Ao executar a migration um usuário com todas as autorizações ja será criado automaticamente  
  ```json
     {
       "username": "admin",
       "password": "a123457z",
     }
   ```
         
         






