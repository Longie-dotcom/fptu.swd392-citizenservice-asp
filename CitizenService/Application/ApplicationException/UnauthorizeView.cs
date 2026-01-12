namespace Application.ApplicationException
{
    public class UnauthorizeView : Exception
    {
        public UnauthorizeView(string message) : base(message) { }
    }
}
