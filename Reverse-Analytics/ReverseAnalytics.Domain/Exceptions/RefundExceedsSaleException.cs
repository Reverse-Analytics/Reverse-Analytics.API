namespace ReverseAnalytics.Domain.Exceptions
{
    public class RefundExceedsSaleException : Exception
    {
        public RefundExceedsSaleException()
            : base()
        {
        }

        public RefundExceedsSaleException(string message)
            : base(message)
        {
        }

        public RefundExceedsSaleException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
