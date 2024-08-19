using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserInterface _userInterface;

        public EmailService(UserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public void SendEmail()
        {
            if (_userInterface.IsEmailSendNeeded())
            {
                Console.WriteLine("Email was sent.");
            }
        }
    }
}
