using System.Net;
using System.Net.Mail;
using Contracts.Events;
using EmailPayment.Consumer;
using Microsoft.Extensions.Logging;

namespace EmailPayment.Service;

public static class EmailService
{
    public static async Task SendEmail(IPaymentProcessed paymentProcessed, ILogger<PaymentProcessedConsumer> logger)
    {
        logger.LogInformation($"STEP [MICROSERVICE] -> Sending payment processed email [{paymentProcessed.Id}]");
        try
        {
            var emailUser = Environment.GetEnvironmentVariable("EMAIL_USER") ?? "";
            var passwordUser = Environment.GetEnvironmentVariable("EMAIL_PASSWORD") ?? "";
            
            var smtpClient = new SmtpClient(Environment.GetEnvironmentVariable("SMTP_SERVER"))
            {
                Port = 587,
                Credentials = new NetworkCredential(emailUser, passwordUser),
                EnableSsl = true,
            };
            
            var isApproved = paymentProcessed.Status?.ToUpper() == "APPROVED";
            
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailUser, "Isaías Shopping"),
                Subject = isApproved ? "COMPRA APROVADA" : "COMPRA RECUSADA",
                IsBodyHtml = true,
                Body = GenerateHtmlBody(paymentProcessed)
            };

            mailMessage.To.Add(paymentProcessed.Email);

            await smtpClient.SendMailAsync(mailMessage);

            logger.LogInformation($"STEP [MCROSERVICE] -> Sent email [{paymentProcessed.Id}]");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"ERROR [MICROSERVICE] -> Error senting email [{paymentProcessed.Id}] ({ex.Message})");
        }
    }

    public static string GenerateHtmlBody(IPaymentProcessed payment)
    {
        var isApproved = payment.Status?.ToUpper() == "APPROVED";

        var headerColor = isApproved ? "#4CAF50" : "#E53935";
        var headerText = isApproved ? "Pagamento aprovado!" : "Pagamento recusado!";
        var statusText = isApproved ? "Aprovado" : "Recusado";

        return $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    color: #333;
                    padding: 20px;
                }}
                .container {{
                    background-color: #ffffff;
                    border-radius: 8px;
                    padding: 20px;
                    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    font-size: 24px;
                    color: {headerColor};
                }}
                .details {{
                    margin-top: 15px;
                }}
                .footer {{
                    margin-top: 30px;
                    font-size: 12px;
                    color: #888;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>{headerText}</div>
                <div class='details'>
                    <p><strong>Valor:</strong> R$ {payment.Amount:F2}</p>
                    <p><strong>Data:</strong> {payment.ProcessedDate:dd/MM/yyyy HH:mm}</p>
                    <p><strong>Status:</strong> {statusText}</p>
                </div>
                <div class='footer'>
                    Esta é uma mensagem automática. Não responda este e-mail.
                </div>
            </div>
        </body>
        </html>";
    }
}