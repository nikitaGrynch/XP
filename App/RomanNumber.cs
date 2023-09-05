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
        for (int i = input.Length - 1; i >= 0; i--)
        {
            int current = input[i] switch
            {
                'I' => 1,
                'V' => 5,
                'X' => 10
            };
            result += (current < prev) ? -current : current;
            prev = current;
        }

        return new()
        {
            Value = result
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