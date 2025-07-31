namespace Api_Finish_Version.Exceptions.Auth
{
    public class EmailException : UserException
    {
        public EmailException() { }
        public EmailException(string message) : base(message){}    
    }
}