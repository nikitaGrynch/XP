using System.Text;

namespace App;

public class RomanNumber
{
    private Dictionary<String, int> romanNumbers = new()
    {
        { "I", 1 },
        { "V", 5 },
        { "X", 10 }
    };
    public int Value { get; set; }

    public RomanNumber(int value = 0)
    {
        Value = value;
    }

    public override string ToString()
    {
        if (Value == 0)
        {
            return "N";
        }
        Dictionary<int, String> ranges = new()
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" },
        };
        StringBuilder result = new();
        int value = Math.Abs(Value);
        foreach (var range in ranges)
        {
            while (value >= range.Key)
            {
                result.Append(range.Value);
                value -= range.Key;
            }
        }

        return $"{(Value < 0 ? "-" : "")}{result}";
    }

    public static RomanNumber Parse(string input)
    {
        input = input?.Trim()!;
        if (String.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Empty or NULL input");
        }
        int result = 0;
        int prev = 0;
        int firstDigitIndex = input.StartsWith("-") ? 1 : 0;
        var invalidDigits = new List<String>();
        for (int i = input.Length - 1; i >= firstDigitIndex; i--)
        {
            int current;
            try
            {
                current = input[i] switch
                {
                    'I' => 1,
                    'V' => 5,
                    'X' => 10,
                    'L' => 50,
                    'C' => 100,
                    'D' => 500,
                    'M' => 1000,
                    'N' => 0,
                    //'-' => 0
                    //_ => throw new ArgumentException($"'{input}' Parse error: Invalid digit '{input[i]}'")
                    _ => throw new ArgumentException(input[i].ToString())
                };
            }
            catch (ArgumentException ex)
            {
                invalidDigits.Add(ex.Message);
                continue;
            }

            //result *= input[i] == '-' ? -1 : 1;
            result += (current < prev) ? -current : current;
            prev = current;
        }

        if (invalidDigits.Count > 0)
        {
            invalidDigits.Reverse();
            throw new ArgumentException($"'{input}' Parse error: Invalid digits '{String.Join("'", invalidDigits)}'");
        }

        // if (isNegative)
        // {
        //     result = -result;
        // }

        return new()
        {
            Value = firstDigitIndex == 0 ? result : -result
            //Value = result * (1 - (firstDigitIndex << 1))
        };
        // int value = 0;
        // switch (input)
        // {
        //     case "I": value = 1;
        //         break;
        //     case "II": value = 2;
        //         break;
        // }
        //     return new()
        //     { 
        //         // Value = input.Length
        //         // Value = input switch
        //         // {
        //         //     "I" => 1,
        //         //     "II" => 2,
        //         //     "III" => 3
        //         // }
        //     };
    }
}