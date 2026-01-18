# Backend Service - PmServiceNetCore

A robust **.NET Core 8 backend service** following clean architecture principles, dependency injection, and modern software engineering practices.

---

## 🧠 Overview

This backend service was carefully designed and implemented to follow **best practices in software architecture**. Key features include:

- Fully **layered architecture** with **DI (Dependency Injection)** and separation of concerns
- Hosted **database on Azure** for scalable and secure data storage
- **Entity Framework Core** for ORM and database management
- Comprehensive **unit and integration testing** using **xUnit** and MVC test patterns
- Fully **automated deployment** to **Azure Web App** using CI/CD pipelines

The service is designed to be **maintainable, testable, and scalable**, ready for enterprise-level applications.

---

## 🛠 Tech Stack

- **Backend:** .NET Core 8, ASP.NET Core MVC, Web API  
- **Database:** Azure SQL / Entity Framework Core  
- **Testing:** xUnit, Integration Tests, MVC Tests  
- **CI/CD:** GitHub Actions / Azure Pipelines  
- **Architecture:** Clean Architecture, SOLID principles, Dependency Injection

---

## ✨ Key Features & Implementation Highlights

- **Database Hosting:** Deployed and managed on **Azure SQL**, ensuring security and scalability  
- **Testing:**  
  - **Unit Tests:** Using **xUnit** for business logic validation  
  - **Integration Tests:** Testing API endpoints and database interactions  
  - **MVC Tests:** Full coverage of controller logic and routing  
- **Automated Deployment:** Configured **CI/CD pipelines** for automated publish to **Azure Web App**  
- **Entity Framework Core:** Code-first migrations and fully managed ORM  
- **Clean Architecture & DI:** Fully decoupled layers for maintainability and scalability  

---

## 📌 Architectural Principles Followed

- **SOLID principles** throughout the service  
- **Separation of concerns**: Controllers, Services, Repositories  
- **Testable design**: Unit and integration testing enabled by design  
- **Dependency injection** for all services and repositories  
- **Scalable and maintainable architecture** for enterprise use

---

## ⚙️ Setup & Run

### Prerequisites
- .NET Core 8 SDK  
- Azure SQL Database (or local SQL for testing)  
- Visual Studio / VS Code  

### Running Locally
```bash
git clone https://github.com/samedfadavi/PmServiceNetCore.git
cd PmServiceNetCore
dotnet restore
dotnet build
dotnet run
