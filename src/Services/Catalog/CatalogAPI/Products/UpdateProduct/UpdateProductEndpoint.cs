﻿
namespace Catalog.API.Products.UpdateProduct
{
    //create a command query/Method using IRequest/ICommand Interdace
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResponse>;

    //create a result/response Method. 
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {

                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);  //   Asynchronously send a request to a single handler & The task result contains the handler response
                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            })
             .WithName("UpdateProducts")
             .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Update Product")
             .WithDescription("Update Product");
        }
    }
}