﻿using Catalog.API.Exceptions;
using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        { 
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(query.Id);

            return new GetProductByIdResult(product);
        }
    }
}