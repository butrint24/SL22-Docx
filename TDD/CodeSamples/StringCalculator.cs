public class StringCalculator
{
    private const int DEFAULT_NUMBER = 0;
    private const string CUSTOM_SEPERATOR_INDICATOR = "//";
    private List<string> SEPERATORS = new List<string>() { ",", "\n" };
    private const int MAX_NUMBER = 1000;

    public int Add(string numbers)
    {
        if (String.IsNullOrWhiteSpace(numbers))
            return DEFAULT_NUMBER;
        if (numbers.StartsWith(CUSTOM_SEPERATOR_INDICATOR))
        {
            numbers = AddCustomSeperators(numbers);
        }
        var cleanedNumbers = CleanNumbers(numbers);

        return cleanedNumbers.Sum();
    }

    private string AddCustomSeperators(string numbers)
    {
        string[] customSeperators = { CUSTOM_SEPERATOR_INDICATOR, "[", "]" };
        var customSeperator = numbers
            .Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .First();

        numbers = numbers.Substring(
            customSeperator.Length,
            numbers.Length - customSeperator.Length
        );
        var allCustomSeperators = customSeperator.Split(
            customSeperators,
            StringSplitOptions.RemoveEmptyEntries
        );

        foreach (var sep in allCustomSeperators)
        {
            SEPERATORS.Add(sep);
        }
        return numbers;
    }

    private IList<int> CleanNumbers(string numbers)
    {
        var nums = numbers.Split(SEPERATORS.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        var cleaned = new List<int>();
        foreach (var num in nums)
        {
            var cleanedNumber = int.Parse(num);
            if (cleanedNumber < 0)
            {
                throw new ApplicationException("Number cannot be negative");
            }
            if (cleanedNumber <= MAX_NUMBER)
            {
                cleaned.Add(cleanedNumber);
            }
        }
        return cleaned;
    }
}