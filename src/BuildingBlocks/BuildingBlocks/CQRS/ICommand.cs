using MediatR; 

namespace BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit>
    {//this is used when no response.
     //// when no response, and inheriting from below
    }

    //this is just a wrapper over IRequest Interface to seperate Cammand & Query
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
