
namespace BuildingBlocks.Exceptions
{
    internal class BadRequestException : Exception
    {

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, string detail) : base(message)
        {
            Detail = detail;
        }

        public string? Detail { get; }
    }
 }
