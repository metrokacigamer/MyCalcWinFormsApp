using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;
using ConsoleApp1.Functions_Versions;
using ConsoleApp1.Peripherial_Classes;
using ConsoleApp1.Expression_Formatting;


namespace WinFormsApp1.Tests
{
    public class FunctionsTests
    {
        Functions _funcs;

        [SetUp]
        public void SetUp()
        {
            // common Arrange
            _funcs = new Functions();
        }

        [Version("2.1", IsUpgradedFromPrevVersions = true)]
        [Test]
        public void ComputeOperators_Test_Works()
        {
            //Arrange
            var testString1 = "12/3+98-5*66";
            var testString2 = "78412+87454124/500-21321-54654";
            var testString3 = "-12/-3+-98--5*-66";
            var testString4 = "122/56*98/54--6--6+-7*-5/-23.65";
            
            //Act
            var actual1 = _funcs.ComputeOperators(testString1);
            var actual2 = _funcs.ComputeOperators(testString2);
            var actual3 = _funcs.ComputeOperators(testString3);
            var actual4 = _funcs.ComputeOperators(testString4);


            //Assert
            var expected1 = "-228";
            var expected2 = "177345.248";
            var expected3 = "-424";
            var expected4 = "14.473788270299897";


            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
        }

        [Version("2.1")]
        [Test]
        public void ExpressionOpList_CheckingExpressions_ReturnsExpressionsCorrectly()
        {
            //Arrange
            var testString1 = "78412*(87454124)+445212/(21321)";
            var testString2 = "78412+(87454124)/445212-(21321)-54654";
            var testString3 = "7841244521221321";
            var testString4 = "(234654)/(342)*(112)*(3)-()";
            var testString5 = "((23423)*(((32)/2)-(23)))";
            var testString6 = "(23423)+((32)/2)/(23)";
            var testString7 = "tan(23423)+sin((32)/cos(2))/cot(23)+12/((23)*25/3)";

            //Act
            var actual1 = _funcs.ExpressionOperatorLists(testString1).Item1;
            var actual2 = _funcs.ExpressionOperatorLists(testString2).Item1;
            var actual3 = _funcs.ExpressionOperatorLists(testString3).Item1;
            var actual4 = _funcs.ExpressionOperatorLists(testString4).Item1;
            var actual5 = _funcs.ExpressionOperatorLists(testString5).Item1;
            var actual6 = _funcs.ExpressionOperatorLists(testString6).Item1;
            var actual7 = _funcs.ExpressionOperatorLists(testString7).Item1;

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
            var expected7 = new List<string>()
            {
                "tan(23423)",
                "sin((32)/cos(2))",
                "cot(23)",
                "12",
                "((23)*25/3)"

            };
            
            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
            Assert.That(actual7, Is.EqualTo(expected7));
        }

        [Version("2.1")]
        [Test]
        public void ExpressionOpList_CheckingOperators_ReturnsOperatorsCorrectly()
        {
            //Arrange
            var testString1 = "78412*(87454124)+445212/(21321)";
            var testString2 = "78412+(87454124)/445212-(21321)-54654";
            var testString3 = "7841244521221321";
            var testString4 = "(234654)/(342)*(112)*(3)-()";
            var testString5 = "((23423)*(((32)/2)-(23)))";
            var testString6 = "(23423)+((32)/2)/(23)";
            var testString7 = "tan(23423)+sin((32)/cos(2))/cot(23)+12/((23)*25/3)";

            //Act
            var actual1 = _funcs.ExpressionOperatorLists(testString1).Item2;
            var actual2 = _funcs.ExpressionOperatorLists(testString2).Item2;
            var actual3 = _funcs.ExpressionOperatorLists(testString3).Item2;
            var actual4 = _funcs.ExpressionOperatorLists(testString4).Item2;
            var actual5 = _funcs.ExpressionOperatorLists(testString5).Item2;
            var actual6 = _funcs.ExpressionOperatorLists(testString6).Item2;
            var actual7 = _funcs.ExpressionOperatorLists(testString7).Item2;

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
            var expected7 = new List<char>()//"(23423)+((32)/2)/(23)";
            {
                '+',
                '/',
                '+',
                '/'
            };

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
            Assert.That(actual7, Is.EqualTo(expected7));
        }

        [Version("2.1")]
        [Test]
        public void ComputeExpressionV2_1_Tests_Work()
        {
            //Arrange
            var testString1 = "78412*(874.3)+445212/(2.5)";
            var testString2 = "78412+(87454124)/500-(21321)-54654";
            var testString3 = "7841244";
            var testString4 = "(234654)/(350)*(112)*(3)-()";
            var testString5 = "(23423)+((32)/2)/(20)";
            var testString6 = "((23423)*(((32)/2)-(23)))";
            var testString7 = "45+-(234)";
            var testString8 = "-(-4334)";

            //Act
            var actual1 = _funcs.ComputeExpressionV2_1(testString1);
            var actual2 = _funcs.ComputeExpressionV2_1(testString2);
            var actual3 = _funcs.ComputeExpressionV2_1(testString3);
            var actual4 = _funcs.ComputeExpressionV2_1(testString4);
            var actual5 = _funcs.ComputeExpressionV2_1(testString5);
            var actual6 = _funcs.ComputeExpressionV2_1(testString6);
            var actual7 = _funcs.ComputeExpressionV2_1(testString7);
			var actual8 = _funcs.ComputeExpressionV2_1(testString8);


			//Assert
			var expected1 = "68733696.39999999";
            var expected2 = "177345.248";
            var expected3 = "7841244"; 
            var expected4 = "225267.84";
            var expected5 = "23423.8";
            var expected6 = "-163961";
            var expected7 = "-189";
            var expected8 = "4334";

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
            Assert.That(actual4, Is.EqualTo(expected4));
            Assert.That(actual5, Is.EqualTo(expected5));
            Assert.That(actual6, Is.EqualTo(expected6));
            Assert.That(actual7, Is.EqualTo(expected7));
			Assert.That(actual8, Is.EqualTo(expected8));
		}

		[Test]
        public void ComputeExpressionV2_1_TestingOnTrigFuncs_ExpectNothing()
        {
            //Arrange
            var testString1 = "tan(12)";
            var testString2 = "12+-tan(456)";
            var testString3 = "tan(23423)+sin((32)/cos(2))/cot(23)+12/((23)*25/3)";
            var exprFormat = new ExpressionFormatting();
            
            //Act
            var actual1 = exprFormat.GetFormattedOperand(_funcs.ComputeExpressionV2_1(testString1));
            var actual2 = exprFormat.GetFormattedOperand(_funcs.ComputeExpressionV2_1(testString2));
            var actual3 = exprFormat.GetFormattedOperand(_funcs.ComputeExpressionV2_1(testString3));

            //Assert
            var expected1 = exprFormat.GetFormattedOperand($"{Math.Tan(12)}");
            var expected2 = exprFormat.GetFormattedOperand($"{12 + (-1) * Math.Tan(456)}");
            var expected3 = exprFormat.GetFormattedOperand("-2.3892583728875938458780835111049");

            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));
            Assert.That(actual3, Is.EqualTo(expected3));
        }
    }
}
