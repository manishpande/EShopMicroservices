using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace BuildingBlocks.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>  //this where condition added to add validation pipeline for only ICommand not IQuery .i.e. only for crude operations.
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failure = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failure.Any())
            {
                throw new ValidationException(failure);
            }

            return await next();  //for pipeline to continue to next
        }

    }
}