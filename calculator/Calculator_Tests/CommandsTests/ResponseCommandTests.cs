using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Commands;
using Calculator.UserInteraction;
using Moq;

namespace Calculator_Tests.CommandsTests
{
    [TestClass]
    public class ResponseCommandTests
    {
        ResponseCommand responseCommand;
        Mock<IUserInteraction> testInteraction;

        [TestCleanup]
        public void TestCleanUp()
        {
            responseCommand = null;
            testInteraction = null;
        }
        [TestInitialize]
        public void TestInitialize()
        {
            testInteraction = new Mock<IUserInteraction>();
        }
        [DataTestMethod]
        [DataRow(MessagesTemplates.WelocmeMessage)]
        [DataRow(MessagesTemplates.ByeMessage)]
        public void ResposeCommand_ShouldPassMessageCorrectly(string message)
        {
            responseCommand = new ResponseCommand(testInteraction.Object, message);
            responseCommand.Execute();
            testInteraction.Verify(x => x.ShowResponse(message), Times.Once);
        }
    }
}
