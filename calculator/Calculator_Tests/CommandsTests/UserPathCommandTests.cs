using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.UserInteraction;
using Calculator.Commands;
using Moq;

namespace Calculator_Tests.CommandsTests
{
    [TestClass]
    public class UserPathCommandTests
    {
        Mock<IUserInteraction> testInteraction;
        UserPathCommand userPathCommand;


        [TestCleanup]
        public void TestCleanUp()
        {
            testInteraction = null;
            userPathCommand = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testInteraction = new Mock<IUserInteraction>();
            userPathCommand = new UserPathCommand(testInteraction.Object);
        }


        [TestMethod]
        public void UserPathCommand_ShouldReturnCorrectPath()
        {
            string testPath = "C:/TestFolder/testFile.txt";
            testInteraction.Setup(x => x.GetUserInput(It.IsAny<string>())).Returns(testPath);

            userPathCommand.Execute();

            Assert.AreEqual(testPath, userPathCommand.UserInput);
        }
    }
}
