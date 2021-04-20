using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.UserInteraction;
using Moq;
using System;

namespace Calculator_Tests.UserInteractionTests
{
    [TestClass]
    public class ConsoleInteractionTests
    {
        Mock<IUserInteraction> testInteraction;

        [TestCleanup]
        public void TestCleanUp()
        {
            testInteraction = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testInteraction = new Mock<IUserInteraction>();
        }

        [DataTestMethod]
        [DataRow("2341243124%^$&*&^")]
        [DataRow("String")]
        [DataRow("12312fsdfsd32fdfg;;::::;;;:21312")]
        public void GetUserInputTest_PassedAndReturnedValuesShouldMatch(string value)
        {
            string formalMessage = String.Empty;
            testInteraction.Setup(x => x.GetUserInput(formalMessage)).Returns(value);
            var test = testInteraction.Object.GetUserInput(formalMessage);
            Assert.AreEqual(value, test);
        }


        [DataTestMethod]
        [DataRow(MessagesTemplates.MainMenu)]
        [DataRow(MessagesTemplates.WelocmeMessage)]
        [DataRow(MessagesTemplates.RealizationChoose)]
        [DataRow(MessagesTemplates.GetExpression)]
        [DataRow(MessagesTemplates.GetPath + "asdas")]
        [DataRow(MessagesTemplates.WarnEmptyInput + "123123123")]
        [DataRow(MessagesTemplates.WarnIncorrectInput + " ")]
        [DataRow(MessagesTemplates.ExitOrRestart)]
        [DataRow(MessagesTemplates.ByeMessage)]
        public void ShowResponseTest_PassedAndReturnedValuesShouldMatch(string value)
        {
            testInteraction.Setup(x => x.ShowResponse(It.IsAny<string>()));
            testInteraction.Object.ShowResponse(value);
            testInteraction.Verify(x => x.ShowResponse((value)), Times.Once());
        }

        [DataTestMethod]
        [DataRow(ConsoleKey.A)]
        [DataRow(ConsoleKey.B)]
        [DataRow(ConsoleKey.C)]
        [DataRow(ConsoleKey.F)]
        [DataRow(ConsoleKey.Escape)]
        public void GetUserKeyDialogTest(ConsoleKey key)
        {
            string formalMessage = String.Empty;
            testInteraction.Setup(x => x.GetUserKey(formalMessage)).Returns(key);
            var resKey = testInteraction.Object.GetUserKey(formalMessage);
            Assert.AreEqual(key, resKey);
        }
    }

}
