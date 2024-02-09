
# Produtos API - AutoGlass

Api construída utilizando .NET 5, Entity Framework Core (SQLite) para gestão de produtos.  
A aplicação oferece recursos básicos de um API (GET, PUT, POST, DELETE)  


## Arquitetura

- Domain Driven Design (DDD)

A aplicação segue os conceitos de DDD

**Domain**  
Na camada de domínio temos as entidades da aplicação. foi aplicado o conceito de domínio rico na criação do produto, onde no construtor da entidade deixamos como padrão **SituacaoProduto** como true.   

Como a aplicação é simples, não foi encontrado outras alternativas de encapsular a regra de negócio no domínio.

**Infrastructure**  
Na camada de infraestrutura temos a definição dos repositórios e o contexto do banco de dados.

**API**  
Já na camada de apresentação temos todos os controllers e DTOs, assim como as configurações de mapping para o automapper.

**Application**  
Nesta camada estão os itens que não estão diretamente relacionados às principais camadas do DDD. Aqui está localizado os Comandos, Handlers e Queries da aplicação, seguindo os conceitos de CQRS

## Instalação e Execução

Para rodar a aplicação utilize o Visual Studio em conjunto do .NET 5.

## Banco de Dados

Foi utilizado de ORM o Entity Framework Core junto do SQLite.
Há um arquivo chamado data.db, caso seja necessário reiniciar o banco de dados apenas apague o arquivo e siga os passos abaixo:  

1- Instale o EF Core global com o dotnet.

```bash
$dotnet tool install --global dotnet-ef 
```

2- Rode o update utilizando o EF core.

```bash
$dotnet ef database update
```

## Diferenciais

- Foi utilizdo o **Automapper** para as entidades, comandos e DTOs
- Foi utilizado o **EF Core** como ORM
- Foi aplicado testes unitários para os Comandos, Handlers e Queries utilizando **xUnit**
- Para os testes unitários foi utilizado libs como **Bogus** e **Moq**
- Aplicação dos conceitos de **CQRS**
## Autor

- [@ricardo_gfontana](https://www.linkedin.com/in/ricardo-fontana-978060208/) - Ricardo Fontana - (27 99764-9613)


## Recursos da API

#### retorna todos os produtos com base nos filtros

```http
  GET /produtos
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `pagina` | `int` | Número da página |
| `tamanhoPagina` | `int` | Quantidade de itens retornados por página |
| `dataValidadeMin` | `int` | Retorna todos os produtos com a data de validade maior ou igual a **dataValidadeMin** |
| `dataValidadeMax` | `int` | Retorna todos os produtos com a data de validade menor ou igual a **dataValidadeMax** |
| `dataFabricacaoMin` | `int` | Retorna todos os produtos com a data de fabricação maior ou igual a **dataFabricacaoMin** |
| `dataFabricacaoMax` | `int` | Retorna todos os produtos com a data de validade menor ou igual a **dataValidadeMax** |
| `codigoFornecedor` | `int` | Filtra os produtos baseados no código do fornecedor |

#### Retorna um produto

```http
  GET /produtos/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do produto que você quer |


#### Cria um produto

```http
  POST /produtos
```

#### Edita um produto

```http
  POST /produtos/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do produto que você quer |


#### Deleta um produto
obs: a exclusão dos produtos é lógica e é realizada através da propriedade SituacaoProduto
```http
  DELETE /produtos/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do produto que você quer |

#### retorna todos os fornecedores com base nos filtros

```http
  GET /fornecedor
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `pagina` | `int` | Número da página |
| `tamanhoPagina` | `int` | Quantidade de itens retornados por página |
| `CNPJ` | `int` | Retorna o fornecedor com o CNPJ informado |
| `descricaoFornecedor` | `int` | Realiza uma pesquisa nos fornecedores baseados na descrição |

#### Retorna um fornecedor

```http
  GET /fornecedor/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do fornecedor que você quer |


#### Cria um fornecedor

```http
  POST /fornecedor
```

#### Edita um fornecedor

```http
  POST /fornecedor/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do produto que você quer |
