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
            { "III", 3 },
            {"IIII", 4},
            {"IV", 4},
            {"V", 5},
            {"VI", 6},
            {"VII", 7},
            {"VIII", 8},
            {"VIIII", 9},
            {"IX", 9},
            {"X", 10},
            {"XI", 11},
            {"XII", 12},
            {"XIII", 13},
            {"XIIII", 14},
            {"XIV", 14},
            {"XV", 15},
            {"XIIIII", 15},
            {"XVI", 16},
            {"XVII", 17},
            {"XX", 20},
            {"XXIIIII", 25},
            {"XXV", 25},
            {"XXX", 30},
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
}