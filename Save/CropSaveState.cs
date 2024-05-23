using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CropBehavior;

[System.Serializable]
public struct CropSaveState
{
    public int landID;

    public string seedToGrow;
    public CropBehavior.CropState cropState;
    public int growth;
    public int health;

    public CropSaveState(int landID, string seedToGrow, CropBehavior.CropState cropState, int growth, int health)
    {
        this.landID = landID;
        this.seedToGrow = seedToGrow;
        this.cropState = cropState;
        this.growth = growth;
        this.health = health;
    }

    public void Grow()
    {
        //Increase the growth point by 1
        growth++;

        SeedData seedInfo = (SeedData) InventoryManager.Instance.itemIndex.GetItemFromString(seedToGrow);
        int maxGrowth = GameTimestamp.HoursToMinutes(GameTimestamp.DaysToHours(seedInfo.daysToGrow));
        int maxHealth = GameTimestamp.HoursToMinutes(48);


        //Restore the health of the plant when it is watered
        if (health < maxHealth)
        {
            health++;
        }

        //The seed will sprout into a seedling when the growth is at 50%
        if (growth >= maxGrowth / 2 && cropState == CropBehavior.CropState.Seed)
        {
            cropState = CropBehavior.CropState.Seedling;
        }

        //Grow from seedling to harvestable
        if (growth >= maxGrowth && cropState == CropState.Seedling)
        {
            cropState = CropBehavior.CropState.Harvestable;
        }
    }
    public void Wither()
    {
        health--;
        //If the health is below 0 and the crop has germinated, kill it
        if (health <= 0 && cropState != CropBehavior.CropState.Seed)
        {
            cropState = CropBehavior.CropState.Wilted;
        }
    }
}
