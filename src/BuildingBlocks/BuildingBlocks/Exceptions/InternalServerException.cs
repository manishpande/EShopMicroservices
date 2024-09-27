
namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {

        public InternalServerException(string message) : base(message)
        {
        }

        public InternalServerException(string message, string detail) : base(message)
        {
            Detail = detail;
        }

        public string? Detail { get; }

    }
}
