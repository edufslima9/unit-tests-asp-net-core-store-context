using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries
{
  [TestClass]
  public class ProductQueriesTests
  {
    private IList<Product> _products = new List<Product>();

    public ProductQueriesTests()
    {
      for (int i = 1; i <= 5; i++)
      {
        _products.Add(new Product($"Product 0{i}", 10, i <= 3 ? true : false));
      }
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void ShouldReturnThreeProductsWhenGetActiveProducts()
    {
      var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
      Assert.AreEqual(result.Count(), 3);
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void ShouldReturnTwoProductsWhenGetInactiveProducts()
    {
      var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
      Assert.AreEqual(result.Count(), 2);
    }
  }
}