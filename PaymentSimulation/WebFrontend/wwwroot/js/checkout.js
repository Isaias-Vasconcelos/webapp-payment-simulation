const formPayment = document.getElementById('sendPayment');

const productIdField = document.getElementById("ProductId");
const cardNumberField = document.getElementById("CardNumber");
const cardNameField = document.getElementById("CardName");
const cardExpiryField = document.getElementById("CardExpiry");
const cardCvvField = document.getElementById("CardCvv");

const modal = document.getElementById('modalLoading');
const spinner = document.getElementById('spinner');
const statusIcon = document.getElementById('statusIcon');
const message = document.getElementById('modalMessage');

let socketId = null;

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5003/notifications")
    .build();

connection.start().then(() => {
    socketId = connection.connectionId;
    console.log("Server ws connected!", socketId);
}).catch(err => console.error("Erro connection ws", err));

connection.on("PaymentProcessed", (msg) => {
    if(msg.status === "APPROVED")
        paymentApproved()
    else 
        paymentError();
})

formPayment.addEventListener("submit", async function (e) {
    e.preventDefault();

    const payload = {
        productId: productIdField.value,
        socketId: socketId,
        card: {
            cardNumber: cardNumberField.value,
            cardName: cardNameField.value,
            cardExpiry: cardExpiryField.value,
            cardCvv: cardCvvField.value
        }
    };

    try {
        resetModal();
        modal.showModal();

        const response = await fetch("/Home/Payment", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(payload)
        });

        const data = await response.json();
        
        if(!data.isSuccess) {
            paymentError(data.message);
        }
        
        setTimeout(() => modal.close(), 3000);

    } catch (error) {
        console.error("Erro de comunicação:", error);
        resetModal();
        modal.showModal();
        paymentError("Erro de comunicação com o servidor.");
        setTimeout(() => modal.close(), 3000);
    }
});

function resetModal() {
    spinner.style.display = 'block';
    statusIcon.style.display = 'none';
    message.textContent = 'Processando pagamento...';
    statusIcon.className = 'status-icon';
}

function paymentApproved() {
    spinner.style.display = 'none';
    statusIcon.style.display = 'block';
    statusIcon.textContent = '✅';
    statusIcon.classList.add('success');
    message.textContent = 'Pagamento aprovado!';
}

function paymentError(errorMessage) {
    spinner.style.display = 'none';
    statusIcon.style.display = 'block';
    statusIcon.textContent = '❌';
    statusIcon.classList.add('error');
    message.textContent = errorMessage || 'Pagamento recusado!';
}

function clearCardFields() {
    cardNumberField.value = "";
    cardNameField.value = "";
    cardCvvField.value = "";
    cardExpiryField.value = "";
}