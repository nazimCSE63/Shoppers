namespace Shoppers.Web.Models
{
    public enum ResponseTypes
    {
        Success,
        Danger,
        Warning
    }
    public class ResponseModel
    {
        public string ResponseMessage { get; set; }
        public ResponseTypes ResponseType { get; set; }
    }
}
