using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
  public class FakeProductRepository : IProductRepository
  {
    public IEnumerable<Product> Get(IEnumerable<Guid> ids)
    {
      IList<Product> products = new List<Product>();
      for (int i = 1; i <= 5; i++)
      {
        products.Add(new Product($"Product 0{i}", 10, i <= 3 ? true : false));
      }

      return products;
    }
  }
}