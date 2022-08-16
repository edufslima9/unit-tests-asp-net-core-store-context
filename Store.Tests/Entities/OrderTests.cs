using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
  [TestClass]
  public class OrderTests
  {
    private readonly Customer _customer = new Customer("Lorem Ipsum", "lorem@ipsum.com");
    private readonly Product _product = new Product("notebook", 10, true);
    private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldGenerateGuidWhenInsertAValidNewOrder()
    {
      var order = new Order(_customer, 0, null);
      Assert.AreEqual(8, order.Number.Length);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChangeOrderStatusToWaitingPaymentWhenInsertANewOrder()
    {
      var order = new Order(_customer, 0, null);
      Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChangeOrderStatusToWaitingDeliveryWhenPayOrder()
    {
      var order = new Order(_customer, 0, null);

      order.AddItem(_product, 1);
      order.Pay(10);
      Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChandeOrderStatusToCanceledWhenInsertAOrderCanceled()
    {
      var order = new Order(_customer, 0, null);

      order.Cancel();
      Assert.AreEqual(order.Status, EOrderStatus.Canceled);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldNotAddOrderItemWhenInsertANewOrderItemWithoutProduct()
    {
      var order = new Order(_customer, 0, null);

      order.AddItem(null, 1);
      Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldNotAddOrderItemWhenInsertANewOrderItemWithZeroQuantityOrLess()
    {
      var order = new Order(_customer, 0, null);

      order.AddItem(_product, 0);
      Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChangeOrderTotalToFiftyWhenInsertANewValidOrder()
    {
      var order = new Order(_customer, 10, _discount);

      order.AddItem(_product, 5);
      Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChangeOrderTotalToSixtyWhenInsertsAExpiredDiscount()
    {
      var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-10));
      var order = new Order(_customer, 10, expiredDiscount);

      order.AddItem(_product, 5);
      Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChangeOrderTotalToSixtyWhenInsertsAInvalidDiscount()
    {
      var order = new Order(_customer, 10, null);

      order.AddItem(_product, 5);
      Assert.AreEqual(order.Total(), 60);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChangeOrderTotalToFiftyWhenInsertAValidDiscountOfTen()
    {
      var order = new Order(_customer, 10, _discount);

      order.AddItem(_product, 5);
      Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldChangeOrderTotalToSixtyWhenInsertADeliveryFeeOfValueTen()
    {
      var order = new Order(_customer, 10, null);

      order.AddItem(_product, 5);
      Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnAInvalidOrderWhenInsertOrderWithoutClient()
    {
      var order = new Order(null, 10, null);
      Assert.AreEqual(order.IsValid, false);
    }
  }
}