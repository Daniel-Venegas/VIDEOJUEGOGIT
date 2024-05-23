using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegrowableHarvestBehabior : InteractableObject
{
    CropBehavior parentCrop;

    public void SetParent(CropBehavior parentCrop)
    {
        this.parentCrop = parentCrop;
    }
    // Start is called before the first frame update
    public override void Pickup()
    {
        InventoryManager.Instance.EquipHandSlot(item);
        //Update the changes in the scene
        InventoryManager.Instance.RenderHand();
        parentCrop.Regrow();
    }
}
