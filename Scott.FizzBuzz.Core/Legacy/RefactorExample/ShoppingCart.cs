namespace Scott.FizzBuzz.Core.RefactorExample;

public class ShoppingCart
{
    public int GetDiscountPercentage(List<string> items)
    {
        return items.Contains("Book") ? 5 : 0;
    }
}
