namespace Shoppers.Utility
{
    public interface IEmailSender
    {
        void Send(string subject, string body, string receiverEmail, string receiverName);
    }
}