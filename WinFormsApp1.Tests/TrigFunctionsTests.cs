using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;
using ConsoleApp1.Peripherial_Classes;


namespace WinFormsApp1.Tests
{
    public class TrigFunctionsTests
    {
        TrigFunctions _trigFunctions;
        
        [SetUp]
        public void SetUp()
        {
            _trigFunctions = new TrigFunctions();
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
            var actual1 = _trigFunctions.GetFormattedTrigFunc(testString1, sin, out var i1);
            var actual2 = _trigFunctions.GetFormattedTrigFunc(testString2, cos, out var i2);
            var actual3 = _trigFunctions.GetFormattedTrigFunc(testString3, tan, out var i3);
            var actual4 = _trigFunctions.GetFormattedTrigFunc(testString4, cot, out var i4);
            var actual5 = _trigFunctions.GetFormattedTrigFunc(testString5, sin, out var i5);
            var actual6 = _trigFunctions.GetFormattedTrigFunc(testString6, cos, out var i6);
            var actual7 = _trigFunctions.GetFormattedTrigFunc(testString7, tan, out var i7);
            var actual8 = _trigFunctions.GetFormattedTrigFunc(testString8, sin, out var i8);
            var actual9 = _trigFunctions.GetFormattedTrigFunc(testString9, cot, out var i9);
            var actual10 = _trigFunctions.GetFormattedTrigFunc(testString10, sin, out var i10);
            var actual11 = _trigFunctions.GetFormattedTrigFunc(testString11, sin, out var i11);
            var actual12 = _trigFunctions.GetFormattedTrigFunc(testString12, cos, out var i12);

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
