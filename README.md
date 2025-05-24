
# 💳 Sistema de Pagamentos Distribuído com RabbitMQ e ASP.NET Core

Este projeto demonstra uma arquitetura de microserviços em .NET utilizando comunicação assíncrona com **MassTransit + RabbitMQ**, incluindo serviços REST, WebSocket (SignalR) e frontend ASP.NET MVC.

## 📦 Serviços incluídos

| Serviço                | Porta | Descrição                                                 |
|------------------------|-------|-----------------------------------------------            |
| RabbitMQ               | 5672  | Broker de mensagens (AMQP)                                |
| RabbitMQ Management UI | 15672 | Interface web do RabbitMQ                                 |
| WebRestApi             | 5001  | API REST para iniciar pagamentos                          |
| ProcessPayment         | —     | Processa o pagamento                                      |
| EmailPayment           | —     | Envia e-mail de confirmação do pagamento                  |
| StatusPayment          | 5003  | Serviço com SignalR para notificar o frontend             |
| WebFrontend            | 5000  | Aplicação ASP.NET MVC (interface do usuário)              |

## ▶️ Como rodar o projeto

Certifique-se de que você tem o **Docker** e **Docker Compose** instalados.

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. Compile e inicie os containers:

   ```bash
   docker-compose up --build
   ```

3. Acesse os serviços nos navegadores:

   - 🖥️ **Frontend (MVC)**: [http://localhost:5000](http://localhost:5000)
   - 🛠️ **API REST**: [http://localhost:5001](http://localhost:5001)
   - 📢 **Notificações (SignalR)**: [http://localhost:5003](http://localhost:5003)
   - 🐇 **RabbitMQ Management UI**: [http://localhost:15672](http://localhost:15672)
     - Usuário: `guest`
     - Senha: `guest`
