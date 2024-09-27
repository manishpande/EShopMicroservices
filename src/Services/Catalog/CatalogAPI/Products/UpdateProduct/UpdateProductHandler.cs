
using Catalog.API.Exceptions;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.UpdateProduct
{
    //create a command query/Method using IRequest/ICommand Interdace
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;

    //create a result/response Method. 
    public record UpdateProductResult(bool IsSuccess);

    //Validation Using Fluent Validation Lib
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId is Required");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is Required")
                .Length(2, 150).WithMessage("Name should be between 2 to 150 character.");             
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Name is Required");
        }
    }

    public class UpdateProductHandler(IDocumentSession session) //this param added for Marten packag to write to Database
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        { 
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if(product is null) { throw new ProductNotFoundException(command.Id); }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
             
            session.Update(product);  //for update **see martin command.
            await session.SaveChangesAsync(cancellationToken);

            //return result as guid 
            return new UpdateProductResult(true);
        }
    }
}
