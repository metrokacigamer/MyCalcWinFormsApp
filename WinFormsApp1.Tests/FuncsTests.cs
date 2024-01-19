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
        Funcs Funcs = new Funcs();

        [Version("1.0", IsInUse = false)]
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

        [Version("1.0", IsInUse = false)]
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

        [Version("1.0", IsInUse = false)]
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

        [Version("1.0", IsInUse = false)]
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

        [Version("1.0", IsInUse = false)]
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

        [Version("1.0", IsInUse = false)]
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

        [Version("1.0", IsInUse = false)]
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

        [Version("2.1", IsUpgradedFromPrevVersions = true)]
        [Test]
        public void ComputeOperators_Test_Works()
        {
            //Arrange
            var testString1 = "12/3+98-5*66";
            var testString2 = "78412+87454124/500-21321-54654";
            var testString3 = "-12/-3+-98--5*-66";
            
            //Act
            var actual1 = Funcs.ComputeOperators(testString1);
            var actual2 = Funcs.ComputeOperators(testString2);
            var actual3 = Funcs.ComputeOperators(testString3);

            //Assert
            var expected1 = "-228";
            var expected2 = "177345.248";
            var expected3 = "-424";

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
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

        [Version("2.0")]
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
            var actual1 = Funcs.DecideWhatToAdd(testString1);
            var actual2 = Funcs.DecideWhatToAdd(testString2);
            var actual3 = Funcs.DecideWhatToAdd(testString3);
            var actual4 = Funcs.DecideWhatToAdd(testString4);
            var actual5 = Funcs.DecideWhatToAdd(testString5);

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

        [Version("2.1")]
        [Test]
        public void ExpressionList_CheckingExpressions_ReturnsExpressionsCorrectly()
        {
            //Arrange
            var testString1 = "78412*(87454124)+445212/(21321)";
            var testString2 = "78412+(87454124)/445212-(21321)-54654";
            var testString3 = "7841244521221321";
            var testString4 = "(234654)/(342)*(112)*(3)-()";
            var testString5 = "((23423)*(((32)/2)-(23)))";
            var testString6 = "(23423)+((32)/2)/(23)";


            //Act
            var actual1 = Funcs.ExpressionOpList(testString1).Item1;
            var actual2 = Funcs.ExpressionOpList(testString2).Item1;
            var actual3 = Funcs.ExpressionOpList(testString3).Item1;
            var actual4 = Funcs.ExpressionOpList(testString4).Item1;
            var actual5 = Funcs.ExpressionOpList(testString5).Item1;
            var actual6 = Funcs.ExpressionOpList(testString6).Item1;


            //Assert
            var expected1 = new List<string>()
            {
                "78412",
                "(87454124)",
                "445212",
                "(21321)"
            };
            var expected2 = new List<string>()
            {
                "78412",
                "(87454124)",
                "445212",
                "(21321)",
                "54654"
            };
            var expected3 = new List<string>()
            {
                "7841244521221321"
            };
            var expected4 = new List<string>()
            {
                "(234654)",
                "(342)",
                "(112)",
                "(3)",
                "()"
            };
            var expected5 = new List<string>()
            {
                "((23423)*(((32)/2)-(23)))"
            };
            var expected6 = new List<string>()
            {
                "(23423)",
                "((32)/2)",
                "(23)"
            };

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
        }

        [Version("2.1")]
        [Test]
        public void ExpressionList_CheckingOperators_ReturnsOperatorsCorrectly()
        {
            //Arrange
            var testString1 = "78412*(87454124)+445212/(21321)";
            var testString2 = "78412+(87454124)/445212-(21321)-54654";
            var testString3 = "7841244521221321";
            var testString4 = "(234654)/(342)*(112)*(3)-()";
            var testString5 = "((23423)*(((32)/2)-(23)))";
            var testString6 = "(23423)+((32)/2)/(23)";


            //Act
            var actual1 = Funcs.ExpressionOpList(testString1).Item2;
            var actual2 = Funcs.ExpressionOpList(testString2).Item2;
            var actual3 = Funcs.ExpressionOpList(testString3).Item2;
            var actual4 = Funcs.ExpressionOpList(testString4).Item2;
            var actual5 = Funcs.ExpressionOpList(testString5).Item2;
            var actual6 = Funcs.ExpressionOpList(testString6).Item2;


            //Assert
            var expected1 = new List<char>()
            {
                '*',
                '+',
                '/'
            };
            var expected2 = new List<char>()
            {
                '+',
                '/',
                '-',
                '-'
            };
            var expected3 = new List<char>()
            {
            };
            var expected4 = new List<char>()
            {
                '/',
                '*',
                '*',
                '-'
            };
            var expected5 = new List<char>()
            {
            };
            var expected6 = new List<char>()//"(23423)+((32)/2)/(23)";
            {
                '+',
                '/'
            };

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
        }

        [Version("2.1")]
        [Test]
        public void ComputeExpression2_Tests_Work()
        {
            //Arrange
            var testString1 = "78412*(874.3)+445212/(2.5)";
            var testString2 = "78412+(87454124)/500-(21321)-54654";
            var testString3 = "7841244";
            var testString4 = "(234654)/(350)*(112)*(3)-()";
            var testString5 = "(23423)+((32)/2)/(20)";
            var testString6 = "((23423)*(((32)/2)-(23)))";

            //Act
            var actual1 = Funcs.ComputeExpressionV2_1(testString1);
            var actual2 = Funcs.ComputeExpressionV2_1(testString2);
            var actual3 = Funcs.ComputeExpressionV2_1(testString3);
            var actual4 = Funcs.ComputeExpressionV2_1(testString4);
            var actual5 = Funcs.ComputeExpressionV2_1(testString5);
            var actual6 = Funcs.ComputeExpressionV2_1(testString6);

            //Assert
            var expected1 = "68733696.39999999";
            var expected2 = "177345.248";
            var expected3 = "7841244"; 
            var expected4 = "225267.84";
            var expected5 = "23423.8";
            var expected6 = "-163961";

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
        }

        [Version("2.2")]
        [Test]
        public void DecideHowToAddTrigFunc_Tests_Work()
        {
            //Arrange
            var testString1 = "+-cot(0)";
            var testString2 = "-12";
            var testString3 = "12+9";
            var testString4 = "+cot(0)";
            var testString5 = "84542+(-54)";
            var testString6 = "84542+-(-54)";
            var testString7 = "sin(23)+0";
            var testString8 = "";
            var testString9 = "3+4+(";
            var testString10 = "2+(233)";
            var testString11 = "54";
            var testString12 = "sin(23)+";

            var sin = "sin";
            var cos = "cos";
            var tan = "tan";
            var cot = "cot";
            
            //Act
            var actual1 = Funcs.DecideHowToAddTrigFunc(testString1, sin, out var i1);
            var actual2 = Funcs.DecideHowToAddTrigFunc(testString2, cos, out var i2);
            var actual3 = Funcs.DecideHowToAddTrigFunc(testString3, tan, out var i3);
            var actual4 = Funcs.DecideHowToAddTrigFunc(testString4, cot, out var i4);
            var actual5 = Funcs.DecideHowToAddTrigFunc(testString5, sin, out var i5);
            var actual6 = Funcs.DecideHowToAddTrigFunc(testString6, cos, out var i6);
            var actual7 = Funcs.DecideHowToAddTrigFunc(testString7, tan, out var i7);
            var actual8 = Funcs.DecideHowToAddTrigFunc(testString8, sin, out var i8);
            var actual9 = Funcs.DecideHowToAddTrigFunc(testString9, cot, out var i9);
            var actual10 = Funcs.DecideHowToAddTrigFunc(testString10, sin, out var i10);
            var actual11 = Funcs.DecideHowToAddTrigFunc(testString11, sin, out var i11);
            var actual12 = Funcs.DecideHowToAddTrigFunc(testString12, cos, out var i12);


            //Assert
            var expected1 = "sin(-cot(0))";
            var expected2 = "cos(-12)";
            var expected3 = "tan(9)";
            var expected4 = "cot(cot(0))";
            var expected5 = "sin(-54)";
            var expected6 = "cos(-(-54))";
            var expected7 = "tan(0)";
            var expected8 = "sin(0)";
            var expected9 = "cot(0)";
            var expected10 = "sin(233)";
            var expected11 = "sin(54)";
            var expected12 = "cos(0)";



            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
            Assert.That(actual7, Is.EqualTo(expected7));
            Assert.That(actual8, Is.EqualTo(expected8));
            Assert.That(actual9, Is.EqualTo(expected9));
            Assert.That(actual10, Is.EqualTo(expected10));
            Assert.That(actual11, Is.EqualTo(expected11));
            Assert.That(actual12, Is.EqualTo(expected12));

            Assert.That(i1, Is.EqualTo(1));
            Assert.That(i2, Is.EqualTo(0));
            Assert.That(i3, Is.EqualTo(3));
            Assert.That(i4, Is.EqualTo(1));
            Assert.That(i5, Is.EqualTo(6));
            Assert.That(i6, Is.EqualTo(6));
            Assert.That(i7, Is.EqualTo(8));
            Assert.That(i8, Is.EqualTo(0));
            Assert.That(i9, Is.EqualTo(5));
            Assert.That(i10, Is.EqualTo(2));
            Assert.That(i11, Is.EqualTo(0));
            Assert.That(i12, Is.EqualTo(8));
        }
    }
}
