# Kanban

<h1 align="center">
    <a href="https://learn.microsoft.com/pt-br/dotnet/fundamentals">🔗 WebApi .net core 7</a>
</h1>
<p align="center">Projeto de Api para login e acesso a board Kanban</p>

## Rodando a API

O Projeto tem dependência do cli do .net core versão 7 ele é instalado junto com o pacote SDK do mesmo. Será necessário instalar para rodar os comandos dotnet para no projeto, antes de prosseguir faça o download do <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/7.0">🔗SDK .net core 7</a> e instale ou se preferir acesse <a href="https://learn.microsoft.com/pt-br/dotnet/core/install/"> 🔗Instalar o .NET </a> para passo a passo completo.  

Será necessário configurar a chave jwt, essa chave tem que ter pelo menos 24 caracteres, pode ser composta por letras, números ou caracteres especiais.
Inclua no arquivo <a href="https://github.com/isaacslima/Kanban/blob/main/BACK/Presentation/KanbanBackend/appsettings.Development.json">🔗 appsettings.Development.json</a>
ou secrets.json caso esteja debugando.

![image](https://github.com/isaacslima/Kanban/assets/11709857/60b92abe-47ab-4625-b687-23011ada0daa)

![image](https://github.com/isaacslima/Kanban/assets/11709857/5d9bfe8e-5514-47b9-bc81-d25ee564b944)

Caso queira rodar o build do projeto é necessário setar a chave no arquivo <a href="https://github.com/isaacslima/Kanban/blob/main/BACK/Presentation/KanbanBackend/appsettings.json">🔗 appsettings.json</a>

Após configurar a chave jwt acesse a pasta do projeto que foi clonado e execute os seguintes comandos em um terminal.

```console
> cd Kanban\BACK
> dotnet run --project Presentation\KanbanBackend\KanbanBackend.csproj
```

Ela responderá na porta 5000.




