using System.Collections.Generic;

namespace ElasticSearchWebApi.Services
{
    public interface IProductService
    {
        Product Add(Product product);
        List<Product> GetAll();
        Product GetById(int productId);
        void Update(Product product);
        void Delete(int productId);

    }
}
