[TestFixture]
public class StringCalculatorTests
{
    StringCalculator calc;

    [SetUp]
    public void setup()
    {
        calc = new StringCalculator();
    }

    [Test]
    public void add_emptystring_zero()
    {
        var result = calc.Add("");
        Assert.AreEqual(0, result);
    }

    [Test]
    public void add_singlenumber_returnsThatNumber()
    {
        var result = calc.Add("1");
        Assert.AreEqual(1, result);
    }

    [Test]
    public void add_singlenumber_returnsThatNumber_1()
    {
        var result = calc.Add("2");
        Assert.AreEqual(2, result);
    }

    [Test]
    public void add_twonumbers_sumsThem()
    {
        var result = calc.Add("1,2");
        Assert.AreEqual(3, result);
    }

    [Test]
    public void add_twonumbers_sumsThem_1()
    {
        var result = calc.Add("1,3");
        Assert.AreEqual(4, result);
    }

    [Test]
    public void add_twolargernumbers_sumsThem()
    {
        var result = calc.Add("10,30");
        Assert.AreEqual(40, result);
    }

    [Test]
    public void add_newline_zero()
    {
        var result = calc.Add("\n");
        Assert.AreEqual(0, result);
    }

    [Test]
    public void add_newlinebetweennumbers_treatsasseperator()
    {
        var result = calc.Add("1\n2");
        Assert.AreEqual(3, result);
    }

    [Test]
    public void add_differentDelimiter_usedToParseTheSum()
    {
        var result = calc.Add("//A\n1A2");
        Assert.AreEqual(3, result);
    }

    [Test]
    public void add_differentDelimiter_usedToParseTheSum2()
    {
        var result = calc.Add("//B\n1B2");
        Assert.AreEqual(3, result);
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void add_negative_throws()
    {
        var result = calc.Add("-1");
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void add_multiple_negative_throws()
    {
        var result = calc.Add("-1,-2");
    }

    [Test]
    [ExpectedException(typeof(ApplicationException))]
    public void add_negativeandpositive_throwsonlythenegative()
    {
        var result = calc.Add("-1,2");
    }

    [Test]
    public void add_numbersbiggerthan1000_ignored()
    {
        var result = calc.Add("2,1001");
        Assert.AreEqual(2, result);
    }

    [Test]
    public void add_delimiterslongerthanonechar_stillaccepted()
    {
        var result = calc.Add("//AAA\n1AAA2AAA3");
        Assert.AreEqual(6, result);
    }

    [Test]
    public void add_multipledelimiters_allused()
    {
        var result = calc.Add("//[A][b]\n1A2b3");
        Assert.AreEqual(6, result);
    }

    [Test]
    public void add_multipledelimiters_allused2()
    {
        var result = calc.Add("//[AB][bc]\n1AB2bc3");
        Assert.AreEqual(6, result);
    }
}
