namespace EgwarancjeAPI.Services.Messages
{
    public interface IMessagesService
    {
        bool Email(string subject, string body, string to);
    }
}
