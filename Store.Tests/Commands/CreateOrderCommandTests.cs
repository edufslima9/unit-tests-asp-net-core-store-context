using Store.Domain.Commands;

namespace Store.Tests.Commands
{
  [TestClass]
  public class CreateOrderCommandTests
  {
    [TestMethod]
    [TestCategory("Commands")]
    public void ShouldNotCreateOrderWhenInsertInvalidCommand()
    {
      var command = new CreateOrderCommand();
      command.Customer = "";
      command.ZipCode = "13411080";
      command.PromoCode = "12345678";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Validate();

      Assert.AreEqual(command.IsValid, false);
    }
  }
}