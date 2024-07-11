namespace EgwarancjeAPI.Services.Messages
{
    public interface IMessagesService
    {
        bool Email(string subject, string body, string to);
        bool SendAccountConfirmationEmail(string to, string userName, string confirmationLink);
        bool SendNewPassword(string to, string userName, string newPassword);
    }
}
