using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Commands;
using Calculator.UserInteraction;
using Moq;

namespace Calculator_Tests.CommandsTests
{
    [TestClass]
    public class UserInputCommandTests
    {
        Mock<IUserInteraction> testInteraction;
        UserInputCommand userInputCommand;


        [TestCleanup]
        public void TestCleanUp()
        {
            testInteraction = null;
            userInputCommand = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testInteraction = new Mock<IUserInteraction>();

        }

        [DataTestMethod]
        [DataRow("2+2")]
        [DataRow("2-2")]
        public void UserInputCommand_ShouldReturnCorrectExpresion(string expression)
        {
            userInputCommand = new UserInputCommand(testInteraction.Object);
            this.testInteraction.Setup(x => x.GetUserInput(It.IsAny<string>())).Returns(expression);
            userInputCommand.Execute();
            Assert.AreEqual(expression, userInputCommand.UserInput);
        }
    }
}