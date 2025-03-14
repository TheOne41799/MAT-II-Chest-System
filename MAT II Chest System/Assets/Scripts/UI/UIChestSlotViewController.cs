using ChestSystem.Chests;
using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChestSlotViewController : MonoBehaviour
{
    [SerializeField] private GameObject uiChestSlotEmpty;
    [SerializeField] private GameObject uiChestSlotFilled;

    [SerializeField] private Image chestImage;
    [SerializeField] private Button chestButton;

    [SerializeField] private TextMeshProUGUI chestType;
    [SerializeField] private TextMeshProUGUI chestState;
    [SerializeField] private TextMeshProUGUI timer;

    [SerializeField] private TextMeshProUGUI coinsInChest;
    [SerializeField] private TextMeshProUGUI gemsInChest;

    private ChestController chestController;
    public ChestController ChestController { get { return chestController; } }


    //chests must be added in chestsystemviewcontroller


    private void Awake()
    {
        ClearSlot();
        ActivateDeactivateChestSlotViewChildrenGameObjects();    
    }

    private void ClearSlot()
    {
        chestController = null;
    }

    private void ActivateDeactivateChestSlotViewChildrenGameObjects()
    {
        if (chestController == null)
        {
            uiChestSlotEmpty.SetActive(true);
            uiChestSlotFilled.SetActive(false);
        }
        else
        {
            uiChestSlotEmpty.SetActive(false);
            uiChestSlotFilled.SetActive(true);
        }
    }

    public void OnChestAdded(ChestController controller)
    {
        chestController = controller;

        UpdateChestSlotViewWhenFilled();
    }

    private void UpdateChestSlotViewWhenFilled()
    {
        if (chestController == null) return;

        ActivateDeactivateChestSlotViewChildrenGameObjects();

        chestImage.sprite = chestController.ChestModel.ChestSprite;
        chestType.text = chestController.ChestModel.ChestType.ToString();

        coinsInChest.text = chestController.ChestModel.CoinsInTheChest.ToString();
        gemsInChest.text = chestController.ChestModel.GemsInChest.ToString();

        //chest state

        //timer
    }
}


