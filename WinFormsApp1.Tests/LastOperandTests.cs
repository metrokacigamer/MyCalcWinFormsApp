using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;
using ConsoleApp1.Peripherial_Classes;


namespace WinFormsApp1.Tests
{
    public class LastOperandTests
    {
        LastOperand _last;
        
        [SetUp]
        public void SetUp()
        {
            _last = new LastOperand();
        }

        [Version("2.0")]
        [Test]
        public void LastOperandContainsDecimal_Test_Works()
        {
            //Arrange
            var testString1 = "12/3+98-5*66";
            var testString2 = "12/3+98-5*66.";
            var testString3 = "12/3+9.8-5*66";
            var testString4 = "3242365.0";
            var testString5 = "";
            var testString6 = "543534534+";

            //Act
            var actual1 = _last.LastOperandContainsDecimal(testString1);
            var actual2 = _last.LastOperandContainsDecimal(testString2);
            var actual3 = _last.LastOperandContainsDecimal(testString3);
            var actual4 = _last.LastOperandContainsDecimal(testString4);
            var actual5 = _last.LastOperandContainsDecimal(testString5);
            var actual6 = _last.LastOperandContainsDecimal(testString6);

            //Assert
            var expected1 = false;
            var expected2 = true;
            var expected3 = false;
            var expected4 = true;
            var expected5 = false;
            var expected6 = false;

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
        }
    }
}
