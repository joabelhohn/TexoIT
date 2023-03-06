# TexoIT

Projeto criado com Visual Studio 2022, dotnet core 7. Pode ser aberto com VS Code ou executador via linha de comando.

## Executar API

Via linha de comando:

`
dotnet run --project .\src\TexoItApi\TexoIt.Api.csproj
`

Acessar no navegador:

`
http://localhost:8080/swagger
`

Swagger habilitado para facilitar o teste manual.

Api <b>/Movies</b>:

POST, multipart/formdata - gravar o arquivo movielist.csv

GET, application/json - retorna o relatório com os ganhadores com o menor e maior intervalo entre premios consecutivos

## Executar Projeto teste por linha de comando

`
dotnet test
`