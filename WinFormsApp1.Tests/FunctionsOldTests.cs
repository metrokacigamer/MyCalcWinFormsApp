using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;
using ConsoleApp1.Functions_Versions;


namespace WinFormsApp1.Tests
{
    public class FunctionsOldTests
    {
        FunctionsOld _funcs;

        [SetUp]
        public void SetUp()
        {
            // common Arrange
            _funcs = new FunctionsOld();
        }

        [Version("1.0")]
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
            var actual = _funcs.PosOfMinPlus(new StringBuilder(test1));

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Version("1.0")]
        [Test]
        public void PosOfMultDiv_Test_Works()
        {
            //Arrange
            var test1 = "-     + 457415fdsds/ sadf...*-----";
            var test2 = "-     + 457415fdsds/ sadf...*---//--";

            //Act
            var actual1 = _funcs.PosOfMultDiv(new StringBuilder(test1));
            var actual2 = _funcs.PosOfMultDiv(new StringBuilder(test2));

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

        [Version("1.0")]
        [Test]
        public void NumberAfterOperator_ContainsBlanks_ReturnsWithoutBlanks()
        {
            //Arrange
            var testString1 = new StringBuilder("+1115487        ");
            var testString2 = new StringBuilder("+     12423546");
            var testString3 = new StringBuilder("+      3454353      +");
            var testString4 = new StringBuilder("+      2 215 76 5 3     ");

            //Act
            var actual1 = _funcs.NumberAfterOperator(testString1, 0);
            var actual2 = _funcs.NumberAfterOperator(testString2, 0);
            var actual3 = _funcs.NumberAfterOperator(testString3, 0);

            //Assert
            var expected1 = (double)1115487;
            var expected2 = (double)12423546;
            var expected3 = (double)3454353;

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.Throws<FormatException>(() => _funcs.NumberAfterOperator(testString4, 0));
        }

        [Version("1.0")]
        [Test]
        public void NumberAfterOperator_OperatorInTheMiddle_ReturnsValue()
        {
            //Arrange
            var testString1 = new StringBuilder("34343-43/431   +1115487     *///**   ");
            var testString2 = new StringBuilder("+     1+24-23546");

            //Act
            var actual1 = _funcs.NumberAfterOperator(testString1, 15);
            var actual2 = _funcs.NumberAfterOperator(testString2, 7);

            //Assert
            var expected1 = (double)1115487;
            var expected2 = (double)24;

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Version("1.0", IsInUse = false)]
        [Test]
        public void NumberBeforeOperator_ContainsBlanks_ReturnsWithoutBlanks()
        {
            //Arrange
            var testString1 = new StringBuilder("1115487        +");
            var testString2 = new StringBuilder("     12423546+");
            var testString3 = new StringBuilder("+      3454353      +");
            var testString4 = new StringBuilder("      2 215 76 5 3     +");

            //Act
            var actual1 = _funcs.NumberBeforeOperator(testString1, testString1.Length - 1);
            var actual2 = _funcs.NumberBeforeOperator(testString2, testString2.Length - 1);
            var actual3 = _funcs.NumberBeforeOperator(testString3, testString3.Length - 1);

            //Assert
            var expected1 = (double)1115487;
            var expected2 = (double)12423546;
            var expected3 = (double)3454353;

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.Throws<FormatException>(() => _funcs.NumberBeforeOperator(testString4, 0));
        }

        [Version("1.0", IsInUse = false)]
        [Test]
        public void NumberBeforeOperator_OperatorInTheMiddle_ReturnsValue()
        {
            //Arrange
            var testString1 = new StringBuilder("34343-43/431   +1115487     *///**   ");
            var testString2 = new StringBuilder("+  1241 +24-23546");

            //Act
            var actual1 = _funcs.NumberBeforeOperator(testString1, 15);
            var actual2 = _funcs.NumberBeforeOperator(testString2, 7);

            //Assert
            var expected1 = (double)431;
            var expected2 = (double)1241;

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Version("1.0", IsInUse = false)]
        [Test]
        public void ChangeOpWithResult_Test_Works()
        {
            //Arrange
            var testString1 = "fysdygs -842  +  8888/";

            //Act
            var actual1 = new StringBuilder(testString1);//Funcs.ChangeOpWithResult(testString1, 9730, 14);
            _funcs.ChangeOpWithResult(ref actual1, 9730, 14);

            //Assert
            var expected1 = new StringBuilder("fysdygs -9730        /");
            Assert.That(actual1.ToString(), Is.EqualTo(expected1.ToString()));
        }

        [Version("1.0", IsInUse = false)]
        [Test]
        public void ComputeExpression_Test_Works()
        {
            //Arrange
            var testString1 = "12/3+98-5*66";

            //Act
            var actual1 = _funcs.ComputeOperators(testString1).ToString();

            //Assert
            var expected1 = "-228";
            Assert.That(actual1, Is.EqualTo(expected1));
        }
    }
}
