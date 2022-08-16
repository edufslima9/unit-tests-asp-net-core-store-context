using Flunt.Validations;

namespace Store.Domain.Entities
{
  public class OrderItem : Entity
  {
    public OrderItem(Product product, int quantity)
    {
      AddNotifications(
        new Contract<OrderItem>()
          .Requires()
          .IsNotNull(product, "Product", "Produto inválido")
          .IsGreaterThan(quantity, 0, "Quantity", "A quatidade deve ser maior que zero")
      );

      Product = product;
      Price = Product != null ? Product.Price : 0;
      Quantity = quantity;
    }

    public Product Product { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public decimal Total()
    {
      return Price * Quantity;
    }
  }
}