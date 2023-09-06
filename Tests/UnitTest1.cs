using App;

namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestRomanNumberParse()
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
    public void TestRomanNumberParseInvalid()
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
            {"iiV", 'i'},
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
    }
}