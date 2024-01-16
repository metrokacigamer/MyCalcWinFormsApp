using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Tests
{
    public class FuncsTests
    {
        [Test]
        public void PosOfMinPlus_Test_Works()
        {
            //Arrange
            var test1 = "-     + 457415fdsds/ sadf...*-----";
            var expected = new List<int>()
            {
                0,
                6,
                29,
                30,
                31,
                32,
                33
            };
            //Act
            var actual = Funcs.PosOfMinPlus(new StringBuilder(test1));
            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PosOfMultDiv_Test_Works()
        {
            //Arrange
            var test1 = "-     + 457415fdsds/ sadf...*-----";
            var test2 = "-     + 457415fdsds/ sadf...*---//--";

            //Act
            var actual1 = Funcs.PosOfMultDiv(new StringBuilder(test1));
            var actual2 = Funcs.PosOfMultDiv(new StringBuilder(test2));

            //Assert
            var expected1 = new List<int>()
            {
                19,
                28
            };
            var expected2 = new List<int>()
            {
                19,
                28,
                32,
                33
            };

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void NumberAfterOperator_ContainsBlanks_ReturnsWithoutBlanks()
        {
            //Arrange
            var testString1 = new StringBuilder("+1115487        ");
            var testString2 = new StringBuilder("+     12423546");
            var testString3 = new StringBuilder("+      3454353      +");
            var testString4 = new StringBuilder("+      2 215 76 5 3     ");



            //Act
            var actual1 = Funcs.NumberAfterOperator(testString1, 0);
            var actual2 = Funcs.NumberAfterOperator(testString2, 0);
            var actual3 = Funcs.NumberAfterOperator(testString3, 0);

            //var actual4 = Funcs.NumberAfterOperator(testString1, 0);

            //Assert
            var expected1 = (double)1115487;
            var expected2 = (double)12423546;
            var expected3 = (double)3454353;
            //Assert.AreEqual(actual1, expected1);
            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.Throws<FormatException>(() => Funcs.NumberAfterOperator(testString4, 0));
        }

        [Test]
        public void NumberAfterOperator_OperatorInTheMiddle_ReturnsValue()
        {
            //Arrange
            var testString1 = new StringBuilder("34343-43/431   +1115487     *///**   ");
            var testString2 = new StringBuilder("+     1+24-23546");


            //Act
            var actual1 = Funcs.NumberAfterOperator(testString1, 15);
            var actual2 = Funcs.NumberAfterOperator(testString2, 7);

            //var actual4 = Funcs.NumberAfterOperator(testString1, 0);

            //Assert
            var expected1 = (double)1115487;
            var expected2 = (double)24;
            //Assert.AreEqual(actual1, expected1);
            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void NumberBeforeOperator_ContainsBlanks_ReturnsWithoutBlanks()
        {
            //Arrange
            var testString1 = new StringBuilder("1115487        +");
            var testString2 = new StringBuilder("     12423546+");
            var testString3 = new StringBuilder("+      3454353      +");
            var testString4 = new StringBuilder("      2 215 76 5 3     +");



            //Act
            var actual1 = Funcs.NumberBeforeOperator(testString1, testString1.Length - 1);
            var actual2 = Funcs.NumberBeforeOperator(testString2, testString2.Length - 1);
            var actual3 = Funcs.NumberBeforeOperator(testString3, testString3.Length - 1);

            //var actual4 = Funcs.NumberAfterOperator(testString1, 0);

            //Assert
            var expected1 = (double)1115487;
            var expected2 = (double)12423546;
            var expected3 = (double)3454353;
            //Assert.AreEqual(actual1, expected1);
            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.Throws<FormatException>(() => Funcs.NumberBeforeOperator(testString4, 0));
        }

        [Test]
        public void NumberBeforeOperator_OperatorInTheMiddle_ReturnsValue()
        {
            //Arrange
            var testString1 = new StringBuilder("34343-43/431   +1115487     *///**   ");
            var testString2 = new StringBuilder("+  1241 +24-23546");


            //Act
            var actual1 = Funcs.NumberBeforeOperator(testString1, 15);
            var actual2 = Funcs.NumberBeforeOperator(testString2, 7);

            //var actual4 = Funcs.NumberAfterOperator(testString1, 0);

            //Assert
            var expected1 = (double)431;
            var expected2 = (double)1241;
            //Assert.AreEqual(actual1, expected1);
            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void ChangeOpWithResult_Test_Works()
        {
            //Arrange
            var testString1 = "fysdygs -842  +  8888/";

            //Act
            var actual1 = new StringBuilder(testString1);//Funcs.ChangeOpWithResult(testString1, 9730, 14);
            Funcs.ChangeOpWithResult(ref actual1, 9730, 14);

            //Assert
            var expected1 = new StringBuilder("fysdygs -9730        /");
            Assert.That(actual1.ToString(), Is.EqualTo(expected1.ToString()));
        }

        [Test]
        public void ComputeExpression_Test_Works()
        {
            //Arrange
            var testString1 = "12/3+98-5*66";

            //Act
            var actual1 = Funcs.ComputeExpression(testString1).ToString();

            //Assert
            var expected1 = "-228";
            Assert.That(actual1, Is.EqualTo(expected1));
        }

        [Test]
        public void ComputeExpression2_Test_Works()
        {
            //Arrange
            var testString1 = "12/3+98-5*66";

            //Act
            var actual1 = Funcs.ComputeExpression2(testString1);

            //Assert
            var expected1 = "-228";
            Assert.That(actual1, Is.EqualTo(expected1));
        }

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
            var actual1 = Funcs.LastOperandContainsDecimal(testString1);
            var actual2 = Funcs.LastOperandContainsDecimal(testString2);
            var actual3 = Funcs.LastOperandContainsDecimal(testString3);
            var actual4 = Funcs.LastOperandContainsDecimal(testString4);
            var actual5 = Funcs.LastOperandContainsDecimal(testString5);
            var actual6 = Funcs.LastOperandContainsDecimal(testString6);

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

        [Test]
        public void WhatToAdd_Test_Works()
        {
            //Arrange
            var testString1 = "8745+88/";
            var testString2 = "8745+88/2";
            var testString3 = string.Empty;
            var testString4 = "8745+88.";
            var testString5 = "874.5+88";



            //Act
            var actual1 = Funcs.WhatToAdd(testString1);
            var actual2 = Funcs.WhatToAdd(testString2);
            var actual3 = Funcs.WhatToAdd(testString3);
            var actual4 = Funcs.WhatToAdd(testString4);
            var actual5 = Funcs.WhatToAdd(testString5);

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
