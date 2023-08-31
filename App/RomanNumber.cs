namespace App;

public class RomanNumber
{
    public int Value { get; set; }
    public static RomanNumber Parse(string input)
    {
        int value = 0;
        switch (input)
        {
            case "I": value = 1;
                break;
            case "II": value = 2;
                break;
        }
        return new()
        {
            Value = value
        };
    }
}