namespace Domain.DomainException
{
    public class CitizenAreaAggregateException : Exception
    {
        public CitizenAreaAggregateException(string message) : base(message) { }
    }
}
