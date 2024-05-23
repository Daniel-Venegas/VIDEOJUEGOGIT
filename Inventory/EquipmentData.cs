using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : ItemData
{
   public enum ToolType
    {
        Azada, Regadera, Hacha, Pico, Pala
    }
    public ToolType toolType;
}
