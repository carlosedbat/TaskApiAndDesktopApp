
# Task Management System - API e Desktop App

Projeto desenvolvido como parte de um desafio técnico, com foco em **Clean Architecture**, **boas práticas de desenvolvimento**, **testes automatizados** e separação em camadas.

---

## 🧠 Desafio

> Criar uma API para gerenciamento de tarefas com frontend em WPF, seguindo arquitetura em camadas, aplicando Clean Code, princípios SOLID e boas práticas de validação e testes.

---

## 🛠️ Tecnologias Utilizadas

- **Backend**: ASP.NET Core Web API (.NET 8)
- **Frontend**: WPF (.NET 8) + MVVM + CommunityToolkit.Mvvm + Refit
- **Banco de Dados**: SQL Server (LocalDB) com Entity Framework Core
- **Arquitetura**: DDD + Clean Architecture (Domain, Application, Infrastructure, Presentation, Shared)
- **Testes**: xUnit com integração real (SQLite in-memory)
- **API Client**: Refit para comunicação da aplicação WPF com a API

---

## 📁 Estrutura de Pastas

```
DataSystem
│
├── Application        # Lógica de negócio (serviços e regras)
├── Domain             # Entidades e enums
├── Infrastructure     # Repositórios e DbContext
├── IoC                # Injeção de dependência
├── Presentation       # ASP.NET Core API (controllers)
├── Shared             # DTOs, exceptions, constants, utils
├── Test               # Testes automatizados (xUnit)
└── FrontendWpf        # Aplicação Desktop (WPF)
```

---

## 🧪 Funcionalidades

### API (.NET Core Web API)

- [x] Criar tarefa
- [x] Editar tarefa
- [x] Excluir tarefa
- [x] Listar todas as tarefas com paginação e filtros
- [x] Buscar tarefa por ID
- [x] Validações de negócio:
  - Título obrigatório e com no máximo 100 caracteres
  - Data de conclusão não pode ser anterior à criação
  - Quando status for "Concluído", data de conclusão obrigatória

### Frontend (WPF)

- [x] Lista de tarefas com filtro por status (enum traduzido)
- [x] CRUD de tarefas em janela separada (com validações)
- [x] Comunicação via **Refit**
- [x] MVVM com CommunityToolkit.Mvvm

---

## 🔧 Configuração

### 1. Variáveis de ambiente (`.env`)

Crie um arquivo `.env` na raiz do projeto `Presentation` com:

```
DatabaseLogLevel=WARNING
```

### 2. Banco de Dados

- SQL Server LocalDB (padrão .NET)
- Migrations são aplicadas automaticamente ao subir a API
- Para rodar manualmente:

```bash
dotnet ef database update Root -s DataSystem.Presentation -p DataSystem.Infrastructure
```

---

## ▶️ Executando o Projeto

### API

```bash
cd DataSystem.Presentation
dotnet run
```

### Frontend (WPF)

```bash
cd DataSystem.FrontendWpf
dotnet run
```

### Testes

```bash
cd DataSystem.Test
dotnet test
```

---

## ✅ Regras de Clean Code e Arquitetura

- Seguindo os princípios **SOLID**
- **SRP** aplicado nas classes
- **Tratamento global de erros** com `CustomException`
- Responses padronizadas com `ServiceResponseDTO<>`
- Arquitetura em **camadas desacopladas**
- Testes integrados com banco local
- Uso de `ILogger<T>` + `Serilog` com sinks por nível

---

## 🧪 Testes

Os testes estão em `DataSystem.Test`, cobrindo:

- Regras de validação
- CRUD completo
- Casos de erro e exceção
- Testes de integração com banco

---


## 🔗 Repositório

[GitHub - TaskApiAndDesktopApp](https://github.com/carlosedbat/TaskApiAndDesktopApp)

---

Desenvolvido por **Carlos Eduardo** — Fullstack .NET/WPF
