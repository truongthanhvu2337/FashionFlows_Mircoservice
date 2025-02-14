# FashionFlows Microservices Architecture

FashionFlows is a microservices-based application designed for scalable e-commerce solutions. It follows **CQRS**, **MediatR**, and **Event-Driven Architecture**, integrating **Stripe** for payment, and **Grafana Loki** for observability.

## 📜 Project Structure

The solution consists of multiple services:

```plaintext
FashionFlows
├── 📂 FashionFlows.ApiGateway # API Gateway (entry point)
├── 📂 FashionFlows.BuildingBlock # Shared components
├── 📂 FashionFlows.Services # Main services
│   ├── 📂 Account
│   ├── 📂 Notification 
│   ├── 📂 Order 
│   ├── 📂 Payment 
│   ├── 📂 Product
├── 📂 FashionFlows.StateMachine # Orchestration and workflow
├── 📄 docker-compose.yml # Docker setup for local dev
```

## 🚀 Features

✅ **CQRS & MediatR** for clean separation of concerns  
✅ **Event-driven architecture** using messaging  
✅ **Stripe integration** for secure payments  
✅ **Grafana Loki** and **Jeager** for distrubuted logging and tracing  
✅ **Redis** for distrubuted caching  
✅ **Docker Compose** for easy local deployment  

## ⚒ Things still be working on

Not implement **Outbox pattern** yet  
**Distributed lock** still in progress  
**Event-driven architecture** not implement properly  

## 🛠 Tech Stack

✅ **Backend**: .NET, MediatR, CQRS, AutoMapper  
✅ **Database**: Redis, SQL Server  
✅ **Message Broker**: RabbitMQ  
✅ **Monitoring**: Grafana, Loki, Jaeger  
