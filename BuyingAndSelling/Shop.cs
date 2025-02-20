using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractableObject
{
    public List<ItemData> shopItems;

    [Header("Dialogues")]
    public List<DialogueLine> dialogueShopOpen;

    public static void Purchase(ItemData item, int quantity)
    {
        int totalCost = item.cost * quantity;
        if (PlayerStats.Money >= totalCost)
        {
            PlayerStats.Spend(totalCost);
            ItemSlotData purchasedItem = new ItemSlotData(item, quantity);

            InventoryManager.Instance.ShopToInventory(purchasedItem);
        }
    }

    public override void Pickup()
    {
        DialogueManager.Instance.StartDialogue(dialogueShopOpen, OpenShop);

    }

    void OpenShop()
    {

        UIManager.Instance.OpenShop(shopItems);
    }
}
