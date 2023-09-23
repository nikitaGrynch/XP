using App;

namespace Tests;

[TestClass]
public class RomanNumberTest
{
    [TestMethod]
    public void TestParse()
    {
        Dictionary<String, int> testCases = new()
        {
            { "II", 2 },
            { "III", 3 }, { "IIII", 4 }, { "IV", 4 }, { "IX", 9 }, { "LXII", 62 }, { "LXIII", 63 },
            { "LXIV", 64 }, { "LXV", 65 }, { "LXVI", 66 }, { "LXVII", 67 }, { "LXXXI", 81 }, { "LXXXII", 82 },
            { "LXXXIII", 83 }, { "LXXXIV", 84 }, { "LXXXV", 85 }, { "LXXXVI", 86 }, { "V", 5 }, { "VI", 6 },
            { "VII", 7 }, { "VIII", 8 }, { "VIIII", 9 }, { "X", 10 }, { "XI", 11 }, { "XII", 12 }, { "XIII", 13 },
            { "XIIII", 14 }, { "XIIIII", 15 }, { "XIV", 14 }, { "XL", 40 }, { "XLI", 41 },
            { "XLII", 42 }, { "XLIII", 43 }, { "XLIV", 44 }, { "XLV", 45 }, { "XV", 15 }, { "XVI", 16 }, { "XVII", 17 },
            { "XVIII", 18 }, { "XX", 20 }, { "XXIIIII", 25 }, { "XXV", 25 }, { "XXX", 30 }, {"C", 100}, {"D", 500}, {"M", 1000},
            {"IM", 999}, {"CM", 900}, {"XM", 990}, {"MCM", 1900}, {"N", 0}, {"-XLI", -41}, {"-CLI", -151}, {"-IL", -49}, {"-XLIX", -49}
        };
        Assert.AreEqual(               // Элемент теста - увтерждение (Assert)
            1,                 // ожидаемое значение 
            RomanNumber          
                .Parse("I")           // фактическое значение 
                .Value,               // 
            "1 == I"
        );
        foreach (var pair in testCases)
        {
            Assert.AreEqual(      
                pair.Value,                
                RomanNumber       
                    .Parse(pair.Key)   
                    .Value,       
                $"{pair.Value} == {pair.Key}"
            );
        }

    }

    [TestMethod]
    public void TestParseException()
    {

        Assert.ThrowsException<ArgumentException>(
            () => RomanNumber.Parse(null!),
            "RomanNumber.Parse(null!) -> Exception"
        );
        var ex = Assert.ThrowsException<ArgumentException>(
            () => RomanNumber.Parse(""),
            "RomanNumber.Parse('') -> Exception"
        );
        Assert.IsFalse(String.IsNullOrEmpty(ex.Message),
                "RomanNumber.Parse('') -- ex.Message not empty");

        Dictionary<String, char> testCases = new ()
        {
            {"XA", 'A'},
            {"LB", 'B'},
            {"vI", 'v'},
            {"1X", '1'},
            {"$M", '$'},
            {"mX", 'm'},
            {"iM", 'i'}
        };
        foreach (var pair in testCases)
        {
            Assert.IsTrue(
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse(pair.Key),
                    $"RomanNumber.Parse({pair.Key}) -> Exception"
                ).Message.Contains($"'{pair.Value}'"),
                $"RomanNumber.Parse({pair.Key}): ex.Message contains '{pair.Value}'");
        }

        String num = "MAM";  // TODO: расширить набор тестов
        ex = Assert.ThrowsException<ArgumentException>(
            () => RomanNumber.Parse(num));
        Assert.IsTrue(
            ex.Message.Contains("Invalid digit", StringComparison.OrdinalIgnoreCase),
            "ex.Message Contains 'Invalid digit'"
            );
        Assert.IsTrue(
            ex.Message.Contains($"'{num}'"),
            $"ex.Message contains '{num}'");
        
    }

    [TestMethod]
    public void TestParseInvalid()
    {
        Dictionary<String, char> testCases = new ()
        {
            {"X C", ' '},
            {"X\tC", '\t'},
            {"X\nC", '\n'},
        };
        foreach (var pair in testCases)
        {
            Assert.IsTrue(
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse(pair.Key),
                    $"RomanNumber.Parse({pair.Key}) -> Exception"
                ).Message.Contains($"'{pair.Value}'"),
                $"RomanNumber.Parse({pair.Key}): ex.Message contains '{pair.Value}'");
        }

        Dictionary<String, char[]> testCases2 = new()
        {
            { "12XC", new[] { '1', '2' } },
            { "XC12", new[] { '1', '2' } },
            { "123XC", new[] { '1', '2', '3' } },
            { "321X", new[] { '3', '2', '1' } },
        };
        foreach (var pair in testCases2)
        {
            var ex =
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse(pair.Key),
                    $"Roman Number parse {pair.Key} --> Exception");
            foreach (char c in pair.Value)
            {
                Assert.IsTrue(
                    ex.Message.Contains($"'{c}'"),
                    $"Roman Number parse ({pair.Key}): ex.Message contains '{c}'"
                    );
            }
        }
    }

    [TestMethod]
    public void TestParseDubious()
    {
        String[] dubious = { "XC", "XC ", "XC\n", "\tXC", "XC  ", " XC  " };
        foreach (var str in dubious)
        {
            Assert.IsNotNull(
                RomanNumber.Parse(str),
                $"Dubious '{str}' cause NULL");
        }

        int value = 90; // RomanNumber.Parse(dubious[0]).Value;
        foreach (var str in dubious)
        {
            Assert.AreEqual(
                value,
                RomanNumber.Parse(str).Value,
                $"Dubious equality '{str}' --> '{value}'"
            );
        }

        // HW 11_09_23
        String[] dubious2 =
            { "IIX", "VVX", "IVX", "VIX", "IIIX", "VVIX", "CCM", "VVC", "XXD", "DDM", "CCMMC", "LLM", "XVC", "IIM" };
        foreach (var str in dubious2)
        {
            /*
            Assert.IsNotNull(
                RomanNumber.Parse(str),
                $"Dubious '{str}' cause NULL"
                );
            */
            Assert.ThrowsException<ArgumentException>(() =>
                    RomanNumber.Parse(str),
                $"Dubious '{str}' cause Exception"
                );

        }


    }

    [TestMethod]
    public void TestToString()
    {
        Dictionary<int, String> testCases = new()
        {
            {0, "N"},
            { 1, "I" },
            { 2, "II" },
            { 4, "IV" },
            { 9, "IX" },
            { 19, "XIX" },
            { 99, "XCIX" },
            { 499, "CDXCIX" },
            { 999, "CMXCIX" },
            {-45, "-XLV"},
            {-95, "-XCV"},
            {-285, "-CCLXXXV"},
            {500, "D"},
            {2000, "MM"},
            {50, "L"},
            {1050, "ML"},
            {1115, "MCXV"},
            {1400, "MCD"},
            {1935, "MCMXXXV"},
            {2023, "MMXXIII"}
        };
        foreach (var testCase in testCases)
        {
            Assert.AreEqual(
                testCase.Value,
                new RomanNumber(testCase.Key).ToString(),
                $"{testCase.Key}.ToString() --> '{testCase.Value}'"
            );
        }
    }

    [TestMethod]
    public void CrossTestParseToString()
    {
        for (int i = 0; i < 697; i++)
        {
            int rnd = new Random().Next(-5000, 5000);
            RomanNumber r = new(rnd);
            Assert.IsNotNull(r);
            Assert.AreEqual(
                rnd,
                RomanNumber.Parse(r.ToString()).Value,
                $"Parse('{r}'.ToString()) --> '{rnd}'");
        }
    }

    [TestMethod]
    public void TypesFeature()
    {
        RomanNumber r = new(10);
        Assert.AreEqual(10L, r.Value); // 10u - uint, r.Value - int -- fail
        
        Assert.AreEqual((short)10, r.Value);
    }

    [TestMethod]
    public void TestPlus()
    {
        RomanNumber r1 = new(10);
        RomanNumber r2 = new(20);
        var r3 = r1.Plus(r2);
        Assert.IsInstanceOfType(r1.Plus(r2), typeof(RomanNumber));
        Assert.AreNotSame(r1, r3);
        Assert.AreNotSame(r2, r3);

        Assert.AreEqual(30, r3.Value);
        Assert.AreEqual("XXX", r3.ToString());

        Assert.AreEqual(15, r1.Plus(new(5)).Value);
        Assert.AreEqual(1, r1.Plus(new(-9)).Value);
        Assert.AreEqual(-1, r1.Plus(new(-11)).Value);
        Assert.AreEqual(0, r1.Plus(new(-10)).Value);
        Assert.AreEqual(10, r1.Plus(new()).Value);
        
        Assert.AreEqual(5, RomanNumber.Parse("IV").Plus(new(1)).Value);
        Assert.AreEqual(-6, RomanNumber.Parse("-V").Plus(new(-1)).Value);
        
        Assert.AreEqual("N", new RomanNumber(20).Plus(new(-20)).ToString());
        Assert.AreEqual("-II", new RomanNumber(-20).Plus(new(18)).ToString());

        var ex = Assert.ThrowsException<ArgumentNullException>(() => r1.Plus(null!),
            "Plus(null!) -> ArgumentNullException"
        );

        String expectedFragment = "Illegal Plus() invocation with wull argument";
        Assert.IsTrue(ex.Message.Contains(expectedFragment,
                StringComparison.InvariantCultureIgnoreCase
            ),
            $"Plus(null!): ex.Message ({ex.Message}) contains '{expectedFragment}'"
        );

    }
    
    [TestMethod]
    public void TestMinus()
    {
        RomanNumber r1 = new(10);
        RomanNumber r2 = new(20);
        var r3 = r1.Minus(r2);
        Assert.IsInstanceOfType(r1.Minus(r2), typeof(RomanNumber));
        Assert.AreNotSame(r1, r3);
        Assert.AreNotSame(r2, r3);
        
        Assert.AreEqual(-10, r3.Value);
        Assert.AreEqual("-X", r3.ToString());

        Assert.AreEqual(5, r1.Minus(new(5)).Value);
        Assert.AreEqual(19, r1.Minus(new(-9)).Value);
        Assert.AreEqual(21, r1.Minus(new(-11)).Value);
        Assert.AreEqual(20, r1.Minus(new(-10)).Value);
        Assert.AreEqual(10, r1.Minus(new()).Value);
        Assert.AreEqual(0, r1.Minus(new(10)).Value);
        
        Assert.AreEqual(3, RomanNumber.Parse("IV").Minus(new(1)).Value);
        Assert.AreEqual(-4, RomanNumber.Parse("-V").Minus(new(-1)).Value);
        
        Assert.AreEqual("XL", new RomanNumber(20).Minus(new(-20)).ToString());
        Assert.AreEqual("-XXXVIII", new RomanNumber(-20).Minus(new(18)).ToString());
        Assert.AreEqual("N", new RomanNumber(20).Minus(new(20)).ToString());

        var ex = Assert.ThrowsException<ArgumentNullException>(() => r1.Minus(null!),
            "Minus(null!) -> ArgumentNullException"
        );

        String expectedFragment = "Illegal Minus() invocation with wull argument";
        Assert.IsTrue(ex.Message.Contains(expectedFragment,
                StringComparison.InvariantCultureIgnoreCase
            ),
            $"Minus(null!): ex.Message ({ex.Message}) contains '{expectedFragment}'"
        );

    }

    [TestMethod]
    public void TestSum()
    {
        RomanNumber r1 = new(10);
        RomanNumber r2 = new(20);

        var r3 = RomanNumber.Sum(r1, r2);
        Assert.IsInstanceOfType(r3, typeof(RomanNumber));
        
        Assert.AreEqual(60 ,RomanNumber.Sum(r1, r2, r3).Value);
        
        Assert.AreEqual(0, RomanNumber.Sum().Value);

        Assert.IsNull(RomanNumber.Sum(null!));
        
        Assert.AreEqual(40, RomanNumber.Sum(r1, null!, r3).Value);

        var arr1 = Array.Empty<RomanNumber>();
        var arr2 = new RomanNumber[] { new(2), new(4), new(5) };
        Assert.AreEqual(0, RomanNumber.Sum(arr1).Value, "Empty arr --> Sum 0");
        Assert.AreEqual(11, RomanNumber.Sum(arr2).Value, "2-4-5 arr --> Sum 11");

        IEnumerable<RomanNumber> arr3 = new List<RomanNumber>() { new(2), new(4), new(5)};
        Assert.AreEqual(11, RomanNumber.Sum(arr3.ToArray()).Value, "2-4-5 list --> Sum 11");

        // HW 14_09_23
        var arr4 = new RomanNumber[] { null!, null!, null! };
        Assert.AreEqual(null, RomanNumber.Sum(arr4), "null!-null!-null! --> null!");


        Random rnd = new();
        for (int i = 0; i < 200; i++)
        {
            int x = rnd.Next(-3000, 3000);
            int y = rnd.Next(-3000, 3000);
            Assert.AreEqual(
                x+y,
                RomanNumber.Sum(new(x), new(y)).Value,
                $"{x} + {y} - rnd test"
                );
        }
        
        for (int i = 0; i < 200; i++)
        {
            RomanNumber rx = new(rnd.Next(-3000, 3000));
            RomanNumber ry = new(rnd.Next(-3000, 3000));
            Assert.AreEqual(
                rx.Plus(ry).Value,
                RomanNumber.Sum(rx, ry).Value,
                $"{rx} + {ry} - rnd test"
            );
        }
    }

    [TestMethod]
    public void TestEval()
    {
        Assert.IsNotNull(RomanNumber.Eval("V + I"));
        Assert.IsInstanceOfType(RomanNumber.Eval("X + V"), typeof(RomanNumber));
        Assert.AreEqual(10, RomanNumber.Eval("V + V").Value);
        Assert.AreEqual(17, RomanNumber.Eval("XX - III").Value);
        Assert.AreEqual(-23, RomanNumber.Eval("-XX - III").Value);
        Assert.AreEqual(-20, RomanNumber.Eval("-XX - N").Value);
        Assert.AreEqual(5, RomanNumber.Eval("V").Value);
        Assert.AreEqual(0, RomanNumber.Eval("N").Value);

        Random rnd = new();
        for (int i = 0; i < 200; i++)
        {
            int n1 = rnd.Next(-3000, 3000);
            int n2 = rnd.Next(-3000, 3000);
            RomanNumber r1 = new(n1);
            RomanNumber r2 = new(n2);
            Assert.AreEqual(r1.Plus(r2).Value, RomanNumber.Eval($"{r1.ToString()} + {r2.ToString()}").Value);
            Assert.AreEqual(r1.Minus(r2).Value, RomanNumber.Eval($"{r1.ToString()} - {r2.ToString()}").Value);
        }
        
        // "-V-X"
        //     *  "-V - -X"
        //     *  "-V--X"
        //     *  "-V+-X"
        //     *  "V + -X"

        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Eval("+V + X"));
        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Eval("V + +X"));
        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Eval("+V+X"));
        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Eval("-V + - X"));
        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Eval("-V - - X"));
        
    }
}