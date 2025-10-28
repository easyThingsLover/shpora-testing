
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace HomeExercise.Tasks.NumberValidator;

[TestFixture]
public class NumberValidatorTests
{
    [Test]
    public void Test()
    {
        
        /*Assert.Throws<ArgumentException>(() => new NumberValidator(-1, 2, true));
        Assert.DoesNotThrow(() => new NumberValidator(1, 0, true));
        Assert.Throws<ArgumentException>(() => new NumberValidator(-1, 2, false));

        ClassicAssert.IsTrue(new NumberValidator(17, 2, true).IsValidNumber("0.0"));
        ClassicAssert.IsTrue(new NumberValidator(17, 2, true).IsValidNumber("0"));
        ClassicAssert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("00.00"));
        ClassicAssert.IsTrue(new NumberValidator(4, 2, true).IsValidNumber("+1.23"));
        ClassicAssert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("+1.23"));
        ClassicAssert.IsFalse(new NumberValidator(17, 2, true).IsValidNumber("0.000"));
        ClassicAssert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("-1.23"));
        ClassicAssert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("a.sd"));*/
        using (new AssertionScope())
        {
            var precisionBelowZeroValidator = () => new NumberValidator(-1, 2, true);
            var scaleBelowZeroValidator = () => new NumberValidator(1, -1, true);
            var scaleGreaterThanPrecisionValidator = () => new NumberValidator(1, 3, true);
            
            precisionBelowZeroValidator.Should().Throw<ArgumentException>();
            scaleBelowZeroValidator.Should().Throw<ArgumentException>();
            scaleGreaterThanPrecisionValidator.Should().Throw<ArgumentException>();
            
            var limitedScaleValidator = new NumberValidator(17, 2, true);
            var limitedPrecisionValidator = new NumberValidator(3, 2, true);
            var normalizedNumberValidator = new NumberValidator(4, 2, true);
            var notOnlyPositiveValidator = new NumberValidator(4, 2, false);
            
            limitedScaleValidator.IsValidNumber("0.0").Should().BeTrue();
            limitedScaleValidator.IsValidNumber("0.000").Should().BeFalse();
            limitedScaleValidator.IsValidNumber("0").Should().BeTrue();
            limitedPrecisionValidator.IsValidNumber("00.00").Should().BeFalse();
            limitedPrecisionValidator.IsValidNumber("+1.23").Should().BeFalse();
            limitedPrecisionValidator.IsValidNumber("-1.23").Should().BeFalse();
            limitedPrecisionValidator.IsValidNumber("123").Should().BeTrue();
            limitedPrecisionValidator.IsValidNumber("123.").Should().BeFalse();
            normalizedNumberValidator.IsValidNumber("+1.23").Should().BeTrue();
            normalizedNumberValidator.IsValidNumber("a.sd").Should().BeFalse();
            normalizedNumberValidator.IsValidNumber("-1.23").Should().BeFalse();
            normalizedNumberValidator.IsValidNumber("00,00").Should().BeTrue();
            normalizedNumberValidator.IsValidNumber("1.2.3").Should().BeFalse();
            normalizedNumberValidator.IsValidNumber("").Should().BeFalse();
            notOnlyPositiveValidator.IsValidNumber("-1.23").Should().BeTrue();
            
        }
    }
}