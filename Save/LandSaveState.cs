using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Land;

[System.Serializable]
public struct LandSaveState
{
    public Land.LandStatus landStatus;
    public GameTimestamp lastWatered;

    public LandSaveState(Land.LandStatus landStatus, GameTimestamp lastWatered)
    {
        this.landStatus = landStatus;
        this.lastWatered = lastWatered;
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        //Checked if 24 hours has passed since last watered
        if (landStatus == LandStatus.Watered)
        {
            //Hours since the land was watered
            int hoursElapsed = GameTimestamp.ComparesTimestamps(lastWatered, timestamp);
            Debug.Log(hoursElapsed + " hours since this was watered");


            if (hoursElapsed > 24)
            {
                //Dry up (Switch back to farmland)
                landStatus = Land.LandStatus.Farmland;
            }
        }

        
    }
   
    
}