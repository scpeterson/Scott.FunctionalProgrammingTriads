namespace Scott.FizzBuzz.Core.RefactorExample;

public class ShoppingCart
{
    //private readonly List<string> _items = new();
    //private bool _bookAdded = false;

    // public void AddItem(string item)
    // {
    //     _items.Add(item);
    //     // if (item == "Book")
    //     // {
    //     //     _bookAdded = true;
    //     // }
    // }

    public int GetDiscountPercentage(List<string> items)
    {
        return items.Contains("Book") ? 5 : 0;

        //return _bookAdded ? 5 : 0;
    }

    // public List<string> GetItems()
    // {
    //     //return _items;
    //     return new List<string>(_items);
    // }

    // public void RemoveItem(string item)
    // {
    //     _items.Remove(item);
    //     // if (item == "Book")
    //     // {
    //     //     _bookAdded = false;
    //     // }
    // }
}