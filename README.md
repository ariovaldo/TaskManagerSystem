
# Task Manager System

Task Manager System é uma aplicação full stack construida para demostração de uma arquitetura usando as seguintes tecnologias/padroes:

#### Angular 13, .Net Core 6, SqlServer, Cqrs, RabbitMq, ElasticSearch.

A aplicação consiste em um gerenciador de tarefas simples.

## Pré-requisito
 
Antes de começar, certifique-se de ter instalado as seguintes ferramentas:

. SqlServer
. Node.js (versão 14 ou superior)
. Angular CLI (versão 13 ou superior)
. NET Core SDK (versão 6 ou superior)
. Docker

## Instalação e Configuração

1. Clone o repositório para sua máquina local
2. Navegue até a pasta raíz do projeto onde o arquivo docker-compose.yml está localizado.
3. Execute o seguinte comando para iniciar os serviços:

```bash
  docker-compose up
```

Esse script é responsável por iniciar os servicos Elasticsearch, Kibana e o RabbitMQ.

- RabbitMQ estará disponível em http://localhost:15672 e poderá ser acessado usando as credenciais padrão guest/guest.

- ElasticSearch estará disponível em http://localhost:9200/

- Kibana estará disponível em http://localhost:5601. Configure um novo índice com o nome taskmanagersystem-* para visualizar os logs gerados pela aplicação.

4. Navegue até a pasta `TaskManagerSystem.API` onde o projeto da API está localizado. Abra o terminal e execute os comandos abaixo.
Lembre-se de configurar as credenciais da conexão com o banco de dados, RabbitMQ e outras configurações que achar necessário no arquivo appsettings.json.

```bash
  dotnet restore
  dotnet run
```
A Api estará disponível em https://localhost:7235/swagger

5. Navegue até a pasta `TaskManagerSystem.Worker` onde o projeto worker está localizado. Abra o terminal e execute os comandos abaixo.
Lembre-se de configurar as credenciais da conexão com o banco de dados, RabbitMQ e outras configurações que achar necessário no arquivo appsettings.json.

```bash
  dotnet restore
  dotnet run
```

6. Navegue até a pasta `TaskManagerSystem.UI` onde o projeto UI está localizado. Abra o terminal e execute os comandos.

```bash
  npm install
  ng serve
```

A aplicação angular estará disponível em http://localhost:4200




a