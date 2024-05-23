using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, ITimeTracker
{

    public static GameStateManager Instance { get; private set; }
    public APICONECTION apiConnection;

    // Tu código existente...

    // Método para llamar a SendPlayerStats desde GameStateManager
   
    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Set the static instance to this instance
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
    }

 /*   public void SomeMethod()
    {
        // Verifica si la referencia a apiConnection no es nula
        if (apiConnection != null)
        {
            // Llama al método SendPlayerStats en el componente APICONECTION
            apiConnection.SendPlayerStats();
        }
        else
        {
            Debug.LogError("APICONECTION no está asignado en GameStateManager");
        }
    }*/

    public void ClockUpdate(GameTimestamp timestamp)
    {
        UpdateShippingState(timestamp);
        UpdateFarmState(timestamp);

        if (timestamp.hour == 0 && timestamp.minute == 0)
        {
            OnDayReset();
        }
    }

    void OnDayReset()
    {
        Debug.Log("Day has been reset");
        foreach(NPCRelationshipState npc in RelationshipStates.relationships)
        {
            npc.hasTalkedToday = false;
        }
    }

    void UpdateShippingState(GameTimestamp timestamp)
    {
        if (timestamp.hour == ShippingBin.hourToShip && timestamp.minute == 0)
        {
            ShippingBin.shipItems();
        }
    }

    public void UpdateFarmState(GameTimestamp timestamp)
    {
        if (SceneTransitionManager.Instance.currentLocation != SceneTransitionManager.Location.FarmXC)
        {
            List<LandSaveState> landData = LandManager.farmData.Item1;
            List<CropSaveState> cropData = LandManager.farmData.Item2;

            if (cropData.Count == 0) return;

            for (int i = 0; i < cropData.Count; i++)
            {
                CropSaveState crop = cropData[i];
                LandSaveState land = landData[crop.landID];

                if (crop.cropState == CropBehavior.CropState.Wilted) continue;

                land.ClockUpdate(timestamp);

                if (land.landStatus == Land.LandStatus.Watered)
                {
                    crop.Grow();
                }
                else if (crop.cropState != CropBehavior.CropState.Seed)
                {
                    crop.Wither();
                }

                cropData[i] = crop;
                landData[crop.landID] = land;
            }
            LandManager.farmData.Item2.ForEach((CropSaveState crop) =>
            {
                Debug.Log(crop.seedToGrow + "\n Health " + crop.health + "\n growth " + crop.growth + "\n State  " + crop.cropState.ToString());
            });
        }
    }
    public void Sleep()
    {
        GameTimestamp timestampOfNextDay = TimeManager.Instance.GetGameTimestamp();
        timestampOfNextDay.day += 1;
        timestampOfNextDay.hour = 6;
        timestampOfNextDay.minute = 0;
        Debug.Log(timestampOfNextDay.day + " " + timestampOfNextDay.hour + " " + timestampOfNextDay.minute);

        
        TimeManager.Instance.SkipTime(timestampOfNextDay);

        SaveManager.Save(ExportSaveState());

    }

    public GameSaveState ExportSaveState()
    {
        List<LandSaveState> landData = LandManager.farmData.Item1;
        List<CropSaveState> cropData = LandManager.farmData.Item2;

        ItemSlotData[] toolSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] itemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);


        ItemSlotData equippedToolSlot = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool);
        ItemSlotData equippedItemSlot = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item);


        GameTimestamp timestamp = TimeManager.Instance.GetGameTimestamp();

 
        return  new GameSaveState(landData, cropData, toolSlots, itemSlots, equippedToolSlot, equippedItemSlot, timestamp, PlayerStats.Money, RelationshipStates.relationships);
    }

    public void LoadSave()
    {
        // Cargar el estado del juego desde el archivo guardado
        GameSaveState save = SaveManager.Load();



        TimeManager.Instance.LoadTime(save.timestamp);

        ItemSlotData[] toolSlots= ItemSlotData.DeserializeArray(save.toolSlots);
        ItemSlotData equippedToolSlot = ItemSlotData.DeserializeData(save.equippedToolSlot);
        ItemSlotData[] itemSlots = ItemSlotData.DeserializeArray(save.itemSlots);
        ItemSlotData equippedItemSlot = ItemSlotData.DeserializeData(save.equippedItemSlot);

        InventoryManager.Instance.LoadInventory(toolSlots, equippedToolSlot, itemSlots, equippedItemSlot );

        LandManager.farmData = new System.Tuple<List<LandSaveState>, List<CropSaveState>>(save.landData, save.cropData);

        PlayerStats.LoadStats(save.money);

        RelationshipStates.LoadStats(save.relationsihps);
    }
}
