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

            EventService.Instance.OnUnlockingChest.AddListener(UIChestSlotViewUnlockingState);
        }

        private void ClearSlot()
        {
            chestController = null;
        }

        private void ActivateDeactivateChestSlotViewChildrenGameObjects()
        {
            uiChestSlotEmpty.SetActive(chestController == null);
            uiChestSlotFilled.SetActive(chestController != null);
        }

        public void OnChestAdded(ChestController controller)
        {
            if (chestController != null) return;

            chestController = controller;
            UIChestSlotViewLockedState();
            ActivateDeactivateChestSlotViewChildrenGameObjects();
        }

        private void CheckUnlockButtonClicked()
        {
            if (chestController != null)
            {
                EventService.Instance.OnChestUnlockButtonClicked.InvokeEvent(chestController.ChestID);
            }
        }

        public void UIChestSlotViewLockedState()
        {
            chestImage.sprite = chestController.ChestModel.ChestSprite;
            chestType.text = chestController.ChestModel.ChestType.ToString();
            coinsInChest.text = chestController.ChestModel.CoinsInTheChest.ToString();
            gemsInChest.text = chestController.ChestModel.GemsInChest.ToString();
            timer.gameObject.SetActive(false);
        }


        public void UIChestSlotViewUnlockingState(ChestController controller, int timeRemainingToUnlockChest)
        {
            if (chestController == null || chestController.ChestID != controller.ChestID) return;

            timer.gameObject.SetActive(true);
            timer.text = timeRemainingToUnlockChest.ToString();

            chestState.text = "Unlocking";
        }
    }
}

