using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
  [TestClass]
  public class OrderHandlerTests
  {
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandlerTests()
    {
      _customerRepository = new FakeCustomerRepository();
      _deliveryFeeRepository = new FakeDeliveryFeeRepository();
      _discountRepository = new FakeDiscountRepository();
      _productRepository = new FakeProductRepository();
      _orderRepository = new FakeOrderRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldNotGenerateOrderWhenClientNotExists()
    {
      var command = new CreateOrderCommand();
      command.Customer = "11111147";
      command.ZipCode = "13411080";
      command.PromoCode = "12345678";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

      var handler = new OrderHandler(
        _customerRepository,
        _deliveryFeeRepository,
        _discountRepository,
        _productRepository,
        _orderRepository
      );

      handler.Handle(command);
      Assert.AreEqual(handler.IsValid, true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldGenerateOrderWhenInsertInvalidCEP()
    {
      var command = new CreateOrderCommand();
      command.Customer = "12345678";
      command.ZipCode = "13411000";
      command.PromoCode = "12345678";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

      var handler = new OrderHandler(
        _customerRepository,
        _deliveryFeeRepository,
        _discountRepository,
        _productRepository,
        _orderRepository
      );

      handler.Handle(command);
      Assert.AreEqual(handler.IsValid, true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldGenerateOrderWhenPromoCodeNotExists()
    {
      var command = new CreateOrderCommand();
      command.Customer = "12345678";
      command.ZipCode = "13411080";
      command.PromoCode = "";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      

      var handler = new OrderHandler(
        _customerRepository,
        _deliveryFeeRepository,
        _discountRepository,
        _productRepository,
        _orderRepository
      );

      handler.Handle(command);
      Assert.AreEqual(handler.IsValid, true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldNotGenerateOrderWhenOrderItemsCountIsZero()
    {
      var command = new CreateOrderCommand();
      command.Customer = "12345678";
      command.ZipCode = "13411080";
      command.PromoCode = "12345678";

      var handler = new OrderHandler(
        _customerRepository,
        _deliveryFeeRepository,
        _discountRepository,
        _productRepository,
        _orderRepository
      );

      handler.Handle(command);
      Assert.AreEqual(handler.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldNotGenerateOrderWhenInsertAInvalidCommand()
    {
      var command = new CreateOrderCommand();
      command.Customer = "12345678";
      command.ZipCode = "13411080";
      command.PromoCode = "12345678";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Validate();

      Assert.AreEqual(command.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldGenerateWhenInsertAValidCommand()
    {
      var command = new CreateOrderCommand();
      command.Customer = "12345678";
      command.ZipCode = "13411080";
      command.PromoCode = "12345678";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

      var handler = new OrderHandler(
        _customerRepository,
        _deliveryFeeRepository,
        _discountRepository,
        _productRepository,
        _orderRepository
      );

      handler.Handle(command);
      Assert.AreEqual(handler.IsValid, true);
    }
  }
}