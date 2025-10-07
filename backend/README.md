# OrdersPoc - Sistema de Modernização

Prova de conceito para modernização de sistema legado WinForms para arquitetura moderna com .NET 8, PostgreSQL e React.

## Tecnologias

- **.NET 8** - API REST e Worker Service
- **PostgreSQL** - Banco de dados relacional
- **RabbitMQ** - Sistema de mensageria
- **React + TypeScript** - Frontend
- **xUnit** - Testes automatizados

## Como Executar

### 1. Iniciar Banco de Dados

```shell
cd scripts/docker 
docker-compose up -d
```

### 2. Executar API
```shell
cd src/OrdersPoc.API
dotnet run
```

### 3. Acessar Swagger
http://localhost:5000/swagger

## Migrations

## Criar Migrations
```shell
dotnet ef migrations add NomeDaMigration \
--project src/OrdersPoc.Infrastructure/OrdersPoc.Infrastructure.csproj \
--startup-project src/OrdersPoc.API/OrdersPoc.API.csproj
```

## Aplicar Migrations
```shell
dotnet ef database update \
--project src/OrdersPoc.Infrastructure/OrdersPoc.Infrastructure.csproj \
--startup-project src/OrdersPoc.API/OrdersPoc.API.csproj
```
## Dados de Teste

O banco é automaticamente populado com:
- 5 clientes (3 PF, 2 PJ)
- 6 pedidos em diferentes status
- Múltiplos itens por pedido


## Estrutura do Projeto
OrdersPoc/

├── src/ # Código-fonte

├── tests/ # Testes automatizados

├── scripts/ # Scripts de banco e utilitários

└── docs/ # Documentação

## Arquitetura

O projeto segue princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**.
