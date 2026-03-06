# 🏋️ Gym Management System API

A **RESTful Web API** built with **ASP.NET Core** for managing gym members, authentication, and check-ins.

The system implements **JWT Authentication** and **Role-Based Authorization** to ensure secure access to resources.

---

# 🚀 Features

* 🔐 JWT Authentication
* 👥 Role-Based Authorization (Admin, Member, Coach)
* 📋 CRUD Operations for Members
* 🙍 Member Self-Management
* ✅ Gym Check-in System
* 📑 Swagger API Documentation
* 🧩 Clean Architecture (Controllers + Services)

---

# 🛠 Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger / OpenAPI
* C#

---

# 📦 Installation

### 1. Clone the repository

```bash
git clone https://github.com/your-username/gym-management-system.git
```

### 2. Navigate to the project folder

```bash
cd GymManagementSystem
```

### 3. Run the application

```bash
dotnet run
```

The API will start and Swagger will be available.

---

# 🔐 Authentication

This API uses **JWT Authentication**.

### Steps to access protected endpoints

1. Call the login endpoint

```
POST /api/auth/login
```

2. Copy the returned **JWT Token**

3. Open Swagger UI

4. Click **Authorize**

5. Enter the token

```
Bearer YOUR_TOKEN
```

---

# 📚 API Endpoints

## Members

| Method | Endpoint                        | Description                | Access                |
| ------ | ------------------------------- | -------------------------- | --------------------- |
| GET    | /api/Members                    | Get all members            | Admin                 |
| GET    | /api/Members/{memberId}         | Get member by ID           | Admin / Coach / Owner |
| GET    | /api/Members/me                 | Get current member profile | Member                |
| POST   | /api/Members                    | Create new member          | Admin                 |
| PATCH  | /api/Members/{memberId}         | Update member              | Admin / Owner         |
| DELETE | /api/Members/{memberId}         | Delete member              | Admin / Owner         |
| GET    | /api/Members/checkIn/{memberId} | Member check-in            | Admin                 |

---

# 📂 Project Structure

```
GymManagementSystem
│
├── Controllers
├── Services
├── Requests
├── Responses
├── Data
├── Program.cs
└── README.md
```

---

# 👨‍💻 Author

Magdy Mahmoud

Frontend Developer transitioning to Backend (.NET)

* Passionate about Backend Development
* Interested in System Design
* Building RESTful APIs

---

# 📄 License

This project is licensed under the **MIT License**.
