using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingBin : InteractableObject
{
    public static int hourToShip = 18;
    public static List<ItemSlotData> itemsToShip = new List<ItemSlotData>();

    public override void Pickup()
    {

        ItemData handSlotItem = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Item);
        if (handSlotItem == null ) return;

        UIManager.Instance.TriggerYesNoPrompt($"Quieres vender {handSlotItem.name} ?", PlaceItemsInShippingBin );
    }

    void PlaceItemsInShippingBin()
    {
        ItemSlotData handSlot = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item);
        itemsToShip.Add(new ItemSlotData(handSlot));

        handSlot.Empty();

        InventoryManager.Instance.RenderHand();

        foreach(ItemSlotData item  in itemsToShip)
        {
            Debug.Log($"In the shiping bin: {item.itemData.name} x {item.quantity}");
        }
    }

    public static void shipItems()
    {
        int moneyToRecive = TallyItems(itemsToShip);

        PlayerStats.Earn(moneyToRecive);
        itemsToShip.Clear();
    }

    static int TallyItems(List<ItemSlotData> items)
    {
        int total = 0;
        foreach (ItemSlotData item in items)
        {
            total += item.quantity * item.itemData.cost;
        }

        return total;
    }
}
