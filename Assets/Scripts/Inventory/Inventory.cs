using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public static Action OnInventoryUpdated;

    public Inventory()
    {
        itemList = new List<Item>();
        
        AddItem(new Item{itemType = Item.ItemType.Plant, amount = 1});
        AddItem(new Item{itemType = Item.ItemType.Meat, amount = 3});
        AddItem(new Item{itemType = Item.ItemType.Seed, amount = 2});
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount--;
                    itemInInventory = inventoryItem;
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
            Debug.Log(item.amount);
        }
        else
        {
            itemList.Remove(item);
        }
        InventoryUpdated();
    }

    public void InventoryUpdated() => OnInventoryUpdated?.Invoke();

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
