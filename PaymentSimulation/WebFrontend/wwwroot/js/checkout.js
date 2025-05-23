const formPayment = document.getElementById('sendPayment');

let socketId = null;

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5290/notifications")
    .build();

connection.start().then(() => {
    socketId = connection.connectionId;
    console.log("Conectado ao SignalR com socketId:", socketId);
}).catch(err => console.error("Erro ao conectar SignalR:", err));

formPayment.addEventListener("submit", async function (e) {
    e.preventDefault();

    const payload = {
        productId: document.getElementById("ProductId").value,
        socketId: socketId,
        card: {
            cardNumber: document.getElementById("CardNumber").value,
            cardName: document.getElementById("CardName").value,
            cardExpiry: document.getElementById("CardExpiry").value,
            cardCvv: document.getElementById("CardCvv").value
        }
    };

    try {
        const response = await fetch("/Home/Payment", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(payload)
        });
        const data = await response.json();
        console.log(data);
        
    } catch (error) {
        console.error("Erro de comunicação:", error);
    }
});
