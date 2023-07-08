using System.Net.Mail;
using System.Net;

namespace rental_car_api.Service.Email
{
    public class EmailSender
    {
        public void SendEmail(string destinatario, string subject, bool reserva, EmailContent content)
        {
            var body = reserva ?
                $"Parabéns {content.Locador} pela sua reserva. O seu {content.Marca} {content.Modelo} lhe espera. Sua reserva foi feita por {content.Dias} com o total de {content.TotalReserva}." :
                $"Prezado(a) {content.Locador} sua reserva do {content.Marca} {content.Modelo} foi cancelada. Esperamos ansiosamentes lhe ver novamente numa nova oportunidade.";

            // Configurações do servidor SMTP do Gmail
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("felippem001@gmail.com", "Mf828933");
            smtpClient.EnableSsl = true;

            // Constrói o email
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("felippem001@gmail.com");
            mailMessage.To.Add(destinatario);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            try
            {
                // Envia o email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao enviar o email: " + ex.Message);
            }
        }
    }
}
