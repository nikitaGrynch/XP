using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace App;

public class RomanNumber
{
    private const char ZERO_DIGIT = 'N';
    private const String MINUS_SIGN = "-";
    private const String PLUS_SIGN = "+";
    private const String INVALID_DIGIT_MESSAGE = "Invalid digit";
    private const String EMPTY_INPUT_MESSAGE = "Empty or NULL input";
    private const String INVALID_STRUCTURE_MESSAGE = "Invalid roman number structure";
    private const String INVALID_DIGIT_FORMAT = "{0} '{1}'";
    private const String INVALID_DIGITS_FORMAT = "Parse error '{0}'. Invalid digit(s): '{1}''";
    private Func<char, String> DIGIT_DECORATOR = c => $"'{c}'";
    private const String PLUS_NULL_ARGUMENT_MESSAGE = "Illegal Plus() invocation with wull argument";
    private const String MINUS_NULL_ARGUMENT_MESSAGE = "Illegal Minus() invocation with wull argument";
    private const String EVAL_NULL_ARGUMENT_MESSAGE = "Illegal Eval() invocation with wull argument";

    private static String DigitDecorator(char c) => $"'{c}'";
    
    // HW 13_09_23
    private static String InvalidDigitsFormatMessage(List<char> invalidChars) => String.Join(",", invalidChars.Select(DigitDecorator));

    public int Value { get; set; }

    public RomanNumber(int value = 0)
    {
        Value = value;
    }

    public override string ToString()
    {
        if (Value == 0)
        {
            return ZERO_DIGIT.ToString();
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

        return $"{(Value < 0 ? MINUS_SIGN : String.Empty)}{result}";
    }

    private static int DigitValue(char digit)
    {
        return digit switch
        {
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            'M' => 1000,
            ZERO_DIGIT => 0,
            _ => throw new ArgumentException(
                String.Format(INVALID_DIGIT_FORMAT, INVALID_DIGIT_MESSAGE, digit))
        };
    }
    

    private static void CheckValidityOrThrow(String input)
    {
        #region Checking number for forbidden digits

        if (String.IsNullOrEmpty(input))
        {
            throw new ArgumentException(EMPTY_INPUT_MESSAGE);
        }

        int firstDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
        List<char> invalidChars = new();
        for (int i = input.Length - 1; i >= firstDigitIndex; i--)
        {
            try
            {
                DigitValue(input[i]);
            }
            catch
            {
                invalidChars.Add(input[i]);
            }
        }

        if (invalidChars.Count > 0)
        {
            throw new ArgumentException(

                String.Format(INVALID_DIGITS_FORMAT,
                    input,
                    //String.Join(", ",invalidChars.Select(DigitDecorator))
                    InvalidDigitsFormatMessage(invalidChars)
                ));
        }

        #endregion
    }

    private static void CheckCompositionOrThrow(String input)
    {
        #region Checking correct number composition
        
        int firstDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
        int maxDigit = 0;
        bool flag = false;
        for (int i = input.Length - 1; i >= firstDigitIndex; i--)
        {
            int current = DigitValue(input[i]);
            if (current > maxDigit)
            {
                maxDigit = current;
            }
            if (current < maxDigit)
            {
                if (flag)
                {
                    throw new ArgumentException(INVALID_STRUCTURE_MESSAGE);
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
        }
        #endregion
    }

    public static RomanNumber Parse(string input)
    {
        input = input?.Trim()!;
        
        CheckValidityOrThrow(input);
        CheckCompositionOrThrow(input);
        
        int firstDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
        int result = 0;
        int prev = 0;
        for (int i = input.Length - 1; i >= firstDigitIndex; i--)
        {
            int current = DigitValue(input[i]);
            result += (current < prev) ? -current : current;
            prev = current;
        }

        return new()
        {
            Value = firstDigitIndex == 0 ? result : -result
        };
    }

    public RomanNumber Plus (RomanNumber other)
    {
        if (other is null)
        {
            throw new ArgumentNullException(PLUS_NULL_ARGUMENT_MESSAGE);
        }
        return new(this.Value + other.Value);
    }

    public RomanNumber Minus(RomanNumber other)
    {
        if (other is null)
        {
            throw new ArgumentNullException(MINUS_NULL_ARGUMENT_MESSAGE);
        }
        return new(this.Value - other.Value);
    }

    public static RomanNumber Sum(params RomanNumber[] numbers)
    {
        if (numbers == null!)
        {
            return null!;
        }

        // HW 14_09_23
        if (numbers.Length > 0)
        {
            int nullableNumbersCount = 0;
            foreach (var number in numbers)
            {
                if (number == null!)
                {
                    nullableNumbersCount++;
                }
            }

            if (nullableNumbersCount == numbers.Length)
            {
                return null!;
            }
        }

        return new(numbers.Sum(number => number?.Value ?? 0)); 
        //return numbers.Aggregate((r1, r2) => r1.Plus(r2));
    }

    public static RomanNumber Eval(String input)
    {
        if (input is null)
        {
            throw new ArgumentException(EVAL_NULL_ARGUMENT_MESSAGE);
        }

        var operationSymbol = "";
        var r1Str = "";
        var r2Str = "";
        if (input.Contains(Char.Parse(PLUS_SIGN)))
        {
            operationSymbol = PLUS_SIGN;
            r1Str = input.Split(operationSymbol)[0].Trim();
            r2Str = input.Split(operationSymbol)[1].Trim();
        }
        else if (input.Contains(Char.Parse(MINUS_SIGN)))
        {
            operationSymbol = MINUS_SIGN;
            r1Str = input.Substring(0, input.IndexOf(operationSymbol, 1)).Trim();
            r2Str = input.Substring(input.IndexOf(MINUS_SIGN, 1) + 1).Trim();
        }
        else
        {
            return RomanNumber.Parse(input);
        }

        try
        {
            var r1 = RomanNumber.Parse(r1Str);
            var r2 = RomanNumber.Parse(r2Str);
            if (operationSymbol == PLUS_SIGN)
            {
                return r1.Plus(r2);
            }
            else 
            {
                return r1.Minus(r2);
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }

    }

    
}