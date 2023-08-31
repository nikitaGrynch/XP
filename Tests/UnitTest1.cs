using App;

namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestRomanNumberParse()
    {
        Assert.AreEqual(               // Элемент теста - увтерждение (Assert)
            1,                 // ожидаемое значение 
            RomanNumber          
                .Parse("I")           // фактическое значение 
                .Value,               // 
            "1 == I"
        );
        Assert.AreEqual(      
            2,                
            RomanNumber       
                .Parse("II")   
                .Value,       
            "2 == II"
        );
    }
}