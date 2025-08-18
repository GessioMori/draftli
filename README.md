# Draftli

A **real-time collaborative document editor** built with **ASP.NET Core 9**, **React**, and **.NET Aspire**.  
Inspired by Google Docs, Draftli enables multiple users to edit the same document simultaneously with conflict resolution, versioning, and scalable cloud infrastructure.

---

## 🚀 Features

- **Real-Time Collaboration**  
  Live updates with SignalR and WebSockets.  

- **Optimistic Concurrency**  
  Version tracking and conflict resolution.  

- **Distributed Caching**  
  Redis for low-latency document state management.  

- **Background Processing**  
  Worker service to persist snapshots and manage cleanup.  

- **Cloud-Ready**  
  Designed with **.NET Aspire** for local orchestration and easy Azure deployment.  

---

## 🏗️ Architecture

```

React (Client)
│  WebSocket (SignalR)
▼
ASP.NET Core 9 API (CQRS + MediatR + SignalR Hub)
│
├── Redis (distributed cache, pub/sub)
└── SQL / CosmosDB (persistence, snapshots)

Worker Service (background tasks)
└── Periodic flush from Redis → DB

```

- **AppHost (Aspire)** orchestrates all services (API, Worker, Redis, SQL, Frontend).  

---

## 📂 Project Structure

```

/src
├── Draftli.Api        # ASP.NET Core API (SignalR, CQRS, MediatR)
├── Draftli.Worker     # Background worker (snapshots, cleanup)
├── Draftli.Shared     # Shared DTOs and domain models
├── Draftli-web        # React frontend (SignalR client)
└── Draftli.AppHost    # Aspire AppHost orchestrator

````

---

## ⚡ Getting Started

### 1. Clone Repository
```bash
git clone https://github.com/gessiomori/draftli.git
cd draftli
````

### 2. Run Aspire

```bash
dotnet run --project src/Draftli.AppHost
```

### 3. Open Services

* **Frontend** → [http://localhost:5173](http://localhost:5173)
* **API** → [http://localhost:5000](http://localhost:5000)
* **Aspire Dashboard** → [http://localhost:8000](http://localhost:8000)

---

## ✅ To-Do List

### Core Functionality

* [ ] Setup **SignalR Hub** for real-time document editing
* [ ] Implement **basic React editor** connected to Hub
* [ ] Add **Redis cache** for in-memory document states
* [ ] Introduce **document versioning** (optimistic concurrency)
* [ ] Broadcast and reconcile edits across clients

### Persistence

* [ ] Create **SQL schema** for documents & versions
* [ ] Implement **periodic snapshots** in Worker service
* [ ] Add **document recovery** from DB snapshots

### Scaling

* [ ] Integrate **Azure SignalR Service** for production
* [ ] Configure **Azure Redis Cache**
* [ ] Deploy with **Azure App Service / Container Apps**

### Enhancements

* [ ] Add **user authentication & presence indicators**
* [ ] Show **cursor positions** of other users
* [ ] Implement **document permissions & sharing**
* [ ] Export documents (Markdown / PDF)

---

## 🌐 Deployment

Planned deployment on **Azure** using:

* **Azure App Service / AKS** → API + Worker
* **Azure Cache for Redis** → state synchronization
* **Azure SQL / Cosmos DB** → persistent storage
* **Azure SignalR Service** → WebSocket scaling

---