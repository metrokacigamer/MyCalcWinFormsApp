using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;
using ConsoleApp1.Expression_Formatting;
namespace WinFormsApp1.Tests
{
    public class ExpressionFormattingTests
    {
        ExpressionFormatting _expFormat;
        
        [SetUp]
        public void SetUp()
        {
            _expFormat = new ExpressionFormatting();
        }
        [Version("2.0")]
        [Test]
        public void GetFormattedDecimalString_Test_Works()
        {
            //Arrange
            var testString1 = "8745+88/";
            var testString2 = "8745+88/2";
            var testString3 = string.Empty;
            var testString4 = "8745+88.";
            var testString5 = "874.5+88";

            //Act
            var actual1 = _expFormat.GetFormattedDecimalString(testString1);
            var actual2 = _expFormat.GetFormattedDecimalString(testString2);
            var actual3 = _expFormat.GetFormattedDecimalString(testString3);
            var actual4 = _expFormat.GetFormattedDecimalString(testString4);
            var actual5 = _expFormat.GetFormattedDecimalString(testString5);

            //Assert
            var expected1 = "0.";
            var expected2 = ".";
            var expected3 = "0.";
            var expected4 = string.Empty;
            var expected5 = ".";

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
        }

    }
}
