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

    public static RomanNumber Parse(string input)
    {
        if (String.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Empty input");
        }
        int result = 0;
        int prev = 0;
        int firstDigitIndex = input.StartsWith("-") ? 1 : 0;
        // if (isNegative)
        // {
        //     input = input.Substring(1);
        // }
        for (int i = input.Length - 1; i >= firstDigitIndex; i--)
        {
            int current = input[i] switch
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
                _ => throw new ArgumentException($"'{input[i]}'")
            };
            //result *= input[i] == '-' ? -1 : 1;
            result += (current < prev) ? -current : current;
            prev = current;
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