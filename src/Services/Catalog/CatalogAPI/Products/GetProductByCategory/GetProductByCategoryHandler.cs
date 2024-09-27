using Catalog.API.Exceptions;
using Catalog.API.Products.GetProductById;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductByCategory
{
    //request 
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    //response
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
             var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync();
            return new GetProductByCategoryResult(products);
        }
    }
}
