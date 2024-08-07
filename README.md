# Aplicação Hangfire em Projeto ASP.NET

Esta aplicação demonstra o uso do Hangfire em um projeto ASP.NET para executar trabalhos assíncronos. Além de fornecer uma implementação funcional do Hangfire, este projeto também exemplifica vários conceitos e práticas modernas de desenvolvimento de software, tais como:

- **Containers de Injeção de Dependências Personalizados**: Implementação de containers DI para gerenciar a injeção de dependências de maneira eficiente e modular.
- **Segregação de Arquitetura por Projetos em Camadas**: Estruturação do projeto em diferentes camadas (Apresentação, Aplicação, Domínio, Infraestrutura) para promover a separação de responsabilidades.
- **Uso de Testes Unitários com xUnit**: Implementação de testes unitários usando a biblioteca xUnit para garantir a qualidade e a funcionalidade do código.
- **Uso de Armazenamento Local com SQLite**: Utilização do SQLite como banco de dados local para armazenar dados de maneira leve e eficiente.
- **Uso de Entity Framework com SQLite**: Integração do Entity Framework com SQLite para facilitar as operações de banco de dados.
- **Conceitos de Engenharia Reversa de Schema do Banco para o Código**: Geração de código a partir do schema do banco de dados para manter a sincronização entre a camada de dados e o modelo de domínio.
- **Clean Code**: Aplicação dos princípios de Clean Code para escrever um código legível, manutenível e de fácil entendimento.
- **E muito mais**: Exploração de diversas práticas e padrões de design modernos.

## Estrutura do Projeto

O projeto está estruturado em várias camadas para promover a separação de responsabilidades e facilitar a manutenção:

- **Apresentação**: Contém a interface do usuário e os controladores.
- **Aplicação**: Contém a lógica de aplicação e os serviços.
- **Domínio**: Contém as entidades de domínio e as regras de negócios.
- **Infraestrutura**: Contém a implementação de repositórios e acesso a dados.

## Configuração do Projeto

### Requisitos

- .NET 6.0 ou superior
- SQLite
- Entity Framework Core
- Hangfire
- xUnit

### Instalação

1. Clone o repositório:

    ```bash
    git clone https://github.com/newhobbye/cronjob-hangfire-app.git
    ```

2. Navegue até o diretório do projeto:

    ```bash
    cd cronjob-hangfire-app
    ```

3. Restaure os pacotes NuGet:

    ```bash
    dotnet restore
    ```

4. Configure o banco de dados SQLite no arquivo `appsettings.json`.

### Execução

1. Inicie a aplicação:

    ```bash
    dotnet run
    ```

2. Acesse o dashboard do Hangfire em `http://localhost:5000/hangfire` para visualizar e gerenciar os jobs.
