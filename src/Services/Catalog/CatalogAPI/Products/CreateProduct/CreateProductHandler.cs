
using Catalog.API.Products.DeleteProduct;

namespace Catalog.API.Products.CreateProduct
{
    //create a command query/Method using IRequest/ICommand Interdace
    public record CreateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    //create a result/response Method. 
    public record CreateProductResult(Guid Id);

    //Validation Using Fluent Validation Lib
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> 
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required"); 
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Name is Required"); 
        }
    }


    //Command Handler
    public class CreateProductHandler (IDocumentSession session)  //this param added for Marten packag to write to Database
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        { 
            //create product entity from incomming command object.
            Product product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //save to database (code below is Marten package specific 
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return result as guid 
            return new CreateProductResult(product.Id); 
        }
    }
}
