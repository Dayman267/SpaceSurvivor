using System.Collections.Generic;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        
        AddItem(new Item{itemType = Item.ItemType.Plant, amount = 1});
        AddItem(new Item{itemType = Item.ItemType.Meat, amount = 1});
        AddItem(new Item{itemType = Item.ItemType.Seed, amount = 1});
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
