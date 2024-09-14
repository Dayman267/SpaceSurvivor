using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Plant,
        Meat,
        Seed,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Meat: return ItemAssets.Instance.meatSprite;
            case ItemType.Plant: return ItemAssets.Instance.plantSprite;
            case ItemType.Seed: return ItemAssets.Instance.seedSprite;
        }
    }
}
