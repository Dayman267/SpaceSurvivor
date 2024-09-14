using System;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite plantSprite;
    public Sprite meatSprite;
    public Sprite seedSprite;
}
