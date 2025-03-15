using ChestSystem.Chests;
using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
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


        private void Awake()
        {
            ClearSlot();
            ActivateDeactivateChestSlotViewChildrenGameObjects();
        }

        private void OnEnable()
        {
            chestButton.onClick.AddListener(CheckUnlockButtonClicked);
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

            UIChestSlotViewLockedState(chestController);

            ActivateDeactivateChestSlotViewChildrenGameObjects();
        }

        private void CheckUnlockButtonClicked()
        {
            EventService.Instance.OnChestUnlockButtonClicked.InvokeEvent(chestController.ChestID);
        }

        public void UIChestSlotViewLockedState(ChestController controller)
        {
            chestImage.sprite = chestController.ChestModel.ChestSprite;
            chestType.text = chestController.ChestModel.ChestType.ToString();

            coinsInChest.text = chestController.ChestModel.CoinsInTheChest.ToString();
            gemsInChest.text = chestController.ChestModel.GemsInChest.ToString();

            timer.gameObject.SetActive(false);
        }
    }
}

