using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;

namespace ElasticSearchWebApi.Services
{
    public class ProductService : IProductService
    {
        public readonly IElasticClient _elasticClient;

        public ProductService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Product Add(Product product)
        {
            var response = _elasticClient.IndexDocument(product);

            if (response.IsValid)
            {
                return product;
            }
            else
            {
                return new Product();
            }
        }

        public void Delete(int productId)
        {
            _elasticClient.DeleteByQuery<Product>(p => p.Query(q1 => q1
                             .Match(m => m
                                 .Field(f => f.ProductId)
                                 .Query(productId.ToString()
                                 )
                         )));
        }

        public List<Product> GetAll()
        {
            var esResponse = _elasticClient.Search<Product>().Documents;

            return esResponse.ToList();
        }

        public Product GetById(int productId)
        {
            var esResponse = _elasticClient.Search<Product>(x => x.
                             Query(q1 => q1.Bool(b => b.Must(m =>
                             m.Terms(t => t.Field(f => f.ProductId)
                             .Terms<int>(productId))))));

            return esResponse.Documents.FirstOrDefault();
        }

        public void Update(Product product)
        {
            if (product != null)
            {
                var updateResponse = _elasticClient.UpdateByQueryAsync<Product>(q =>
                                     q.Query(q1 => q1.Bool(b => b.Must(m =>
                                     m.Match(x => x.Field(f =>
                                     f.ProductId == product.ProductId)))))
                                     .Script(s => s.Source(
                                    "ctx._source.price = params.price;" +
                                    "ctx._source.productDescription = params.productDescription;" +
                                    "ctx._source.category = params.category;")
                                    .Lang("painless")
                                    .Params(p => p.Add("price", product.Price)
                                    .Add("productDescription", product.ProductDescription)
                                    .Add("category", product.Category)
                                    )).Conflicts(Conflicts.Proceed));
            }
        }
    }
}
