using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    
    [SerializeField] private Transform mainCanvas;
    private Vector2 inventoryTargetPosition;
    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        
        inventoryTargetPosition = itemSlotTemplate.transform.position;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = 
                Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }

    private void DeleteSeed()
    {
        List<Item> itemsToRemove = new List<Item>();

        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Seed)
            {
                itemsToRemove.Add(item);
            }
        }

        foreach (Item item in itemsToRemove)
        {
            inventory.RemoveItem(item);
        }
    }

    private void GatherPlant()
    {
        inventory.AddItem(new Item{itemType = Item.ItemType.Plant, amount = 1});
        RefreshInventoryItems();
        StartCoroutine(AnimateResource("Plant", inventoryTargetPosition));
    }
    
    private void GatherMeat()
    {
        inventory.AddItem(new Item{itemType = Item.ItemType.Meat, amount = 1});
        RefreshInventoryItems();
        StartCoroutine(AnimateResource("Meat", inventoryTargetPosition));
    }
    
    private IEnumerator AnimateResource(string resourceType, Vector2 targetPosition)
    {
        GameObject resourceImageGO = new GameObject("ResourceImage");
        Image resourceImage = resourceImageGO.AddComponent<Image>();
        resourceImage.transform.SetParent(mainCanvas);
        resourceImage.rectTransform.sizeDelta = new Vector2(50, 50);

        Sprite resourceSprite = Resources.Load<Sprite>($"Sprites/{resourceType}");
        resourceImage.sprite = resourceSprite;

        resourceImage.rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2);

        float duration = 1f;
        float elapsedTime = 0f;
        Vector2 startPosition = resourceImage.rectTransform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            resourceImage.rectTransform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        Destroy(resourceImageGO);
    }

    private void OnEnable()
    {
        GardenCheckerAndController.OnSeedPlanted += DeleteSeed;
        Inventory.OnInventoryUpdated += RefreshInventoryItems;
        Gatherable.OnGatheringPlant += GatherPlant;
        Gatherable.OnGatheringMeat += GatherMeat;
    }

    private void OnDisable()
    {
        GardenCheckerAndController.OnSeedPlanted -= DeleteSeed;
        Inventory.OnInventoryUpdated -= RefreshInventoryItems;
        Gatherable.OnGatheringPlant -= GatherPlant;
        Gatherable.OnGatheringMeat -= GatherMeat;
    }
}
