namespace Domain.DomainException
{
    public class CitizenProfileAggregateException : Exception
    {
        public CitizenProfileAggregateException(string message) : base(message) { }
    }
}
