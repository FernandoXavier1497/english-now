# EnglishNow

Portal da escola de inglês EnglishNow (ASP.NET Core 8 MVC + MySQL).

## Como rodar

1. Crie o banco executando `src/create_database.sql` no MySQL.
2. Configure a connection string (o `appsettings.json` traz só um modelo). O melhor é usar o user-secrets, para a senha não ir para o Git:

   ```
   cd src/EnglishNow.Web
   dotnet user-secrets set "ConnectionStrings:EnglishNowConnectionString" "Server=localhost;Port=3306;Database=english_now;Uid=root;Pwd=SUA_SENHA;"
   ```
3. `dotnet run --project src/EnglishNow.Web`
4. Login inicial: usuário `admin`, senha `123` (administrador). Troque depois.
   Se o seu banco já existia com o usuário antigo `adimin`, o login continua sendo `adimin`
   (ou rode: `UPDATE usuario SET login = 'admin' WHERE login = 'adimin';`).

## O que cada perfil faz

| Perfil | Acessa |
|---|---|
| Administrador | Cadastro de professores, alunos e turmas; matricula alunos nas turmas; lança notas |
| Professor | Vê só as próprias turmas e lança notas/faltas dos alunos delas |
| Aluno | Vê o próprio boletim (somente leitura) |

Para um professor ou aluno conseguir entrar, o administrador cadastra o professor/aluno (o cadastro já cria o login).

## Regra das notas

Notas de 0 a 10, digitadas com vírgula (ex.: 7,5).
- Média do bimestre = média simples de escrita, leitura e conversação (só calcula com as três lançadas).
- Nota final do semestre = média simples das médias dos dois bimestres.

Se a escola usar outra regra, ajuste `BoletimService.CalcularMedias` (`EnglishNow.Services/BoletimService.cs`).

## Estrutura

- `EnglishNow.Repositories`: acesso ao MySQL (ADO.NET)
- `EnglishNow.Services`: regras de negócio
- `EnglishNow.Web`: controllers, views e autenticação por cookie
