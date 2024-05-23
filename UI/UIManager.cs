using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ITimeTracker
{

    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    public Image toolEquipSlot;
    public Text toolQuantitySlot;
    public Text timeText;
    public Text dateText;


    [Header("Inventory System")]
    public GameObject inventoryPanel;
    public HandInventorySlot toolHandSlot;
    public InventorySlot[] toolSlots;
    public HandInventorySlot itemHandSlot;
    public InventorySlot[] itemsSlots;

    [Header("Item Info Box")]
    public GameObject itemInfoBox;
    //Info del item
    public Text itemNameText;
    public Text itemDescriptionText;

    [Header("Yes No Promt")]
    public YesNoPromt yesNoPromt;

    [Header("Player Stats")]
    public Text MoneyText;

    [Header("Shop")]
    public ShopListingManager ShopListingManager;

    private void Awake()
    {

        //Si hay más de una instancia, destruye la otra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Establece la estancia estatica
            Instance = this;
        }
    }



    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();
        RenderPlayerStats();
        DisplayItemInfo(null);

       // APICONECTION.Instance.SendPlayerStats();
        TimeManager.Instance.RegisterTracker(this);
    }

    public void TriggerYesNoPrompt(string message, System.Action onYesCallback)
    {
        yesNoPromt.gameObject.SetActive(true);
        yesNoPromt.CreatePrompt(message, onYesCallback);
    }

    public void AssignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemsSlots[i].AssignIndex(i);
        }
    }

    // Start is called before the first frame update
    public void RenderInventory()
    {
        ItemSlotData[] inventoryToolSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] inventoryItemsSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);

        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        RenderInventoryPanel(inventoryItemsSlots, itemsSlots);

        toolHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
        itemHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item));

        ItemData equippedTool = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);

        toolQuantitySlot.text = "";

        if (equippedTool != null)
        {
            toolEquipSlot.sprite = equippedTool.thumbnail;

            toolEquipSlot.gameObject.SetActive(true);

            int quantity = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool).quantity;
            if (quantity > 1)
            {
                toolQuantitySlot.text = quantity.ToString();
            }

            return;
        }

        toolEquipSlot.gameObject.SetActive(false);

    }

    //Verifica si el item está en pantalla



    void RenderInventoryPanel(ItemSlotData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();

    }



    public void DisplayItemInfo(ItemData data)
    {
        if (data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            itemInfoBox.SetActive(false);
            return;
        }
        itemInfoBox.SetActive(true);
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        int hours = timestamp.hour;
        int minutes = timestamp.minute;


        string prefix = "AM ";

        if (hours > 12)
        {
            prefix = "PM ";
            hours -= 12;
        }

        timeText.text = prefix + hours + ":" + minutes.ToString("00");

        int day = timestamp.day;
        string season = timestamp.season.ToString();
        string dayOfTheWeek = timestamp.GetDayOfTheWeek().ToString();

        dateText.text = season + " " + day + " (" + dayOfTheWeek + ")";

    }

    public void RenderPlayerStats()
    {
        MoneyText.text = PlayerStats.Money + PlayerStats.CURRENCY;
 
    }

    public void OpenShop(List<ItemData> shopItems)
    {
        ShopListingManager.gameObject.SetActive(true);
        ShopListingManager.RenderShop(shopItems);   
    }
}
