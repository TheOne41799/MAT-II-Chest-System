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
            chestButton.onClick.AddListener(ChestButtonClicked);

            EventService.Instance.OnChestAdded.AddListener(UIChestSlotViewLockedState);
            EventService.Instance.OnUnlockingChest.AddListener(UIChestSlotViewUnlockingState);
            EventService.Instance.OnChestUnlocked.AddListener(UIChestSlotViewUnlockedState);



            //temp method for chest queue text
            EventService.Instance.OnChestQueuedToUnlock.AddListener(OnChestAddedToQueue);
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

            ActivateDeactivateChestSlotViewChildrenGameObjects();
        }

        public void OnChestRemoved()
        {
            ClearSlot();
            ActivateDeactivateChestSlotViewChildrenGameObjects();
        }

        private void ChestButtonClicked()
        {
            if (chestController != null)
            {
                if (chestController.ChestStateMachine.CurrentState.ChestState == ChestState.LOCKED)
                {
                    EventService.Instance.OnChestUnlockButtonClicked.InvokeEvent(chestController.ChestID);
                }
                else if(chestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKING)
                {
                    EventService.Instance.OnChestUnlockButtonClicked.InvokeEvent(chestController.ChestID);
                }
                else if(chestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKED)
                {
                    EventService.Instance.OnUIPopupCollectRewardsOrUndoChestUnlock.InvokeEvent
                        (
                                chestController,
                                UIPopups.UI_COLLECT_REWARDS_OR_UNDO_CHEST_UNLOCK
                        );
                }
            }
        }

        public void UIChestSlotViewLockedState(ChestController controller)
        {
            if (chestController == null || chestController.ChestID != controller.ChestID) return;

            chestImage.sprite = chestController.ChestModel.ChestSprite;
            chestType.text = chestController.ChestModel.ChestType.ToString();

            chestState.text = chestController.ChestStateMachine.CurrentState.ChestState.ToString();

            coinsInChest.text = chestController.ChestModel.CoinsInTheChest.ToString();
            gemsInChest.text = chestController.ChestModel.GemsInChest.ToString();
            timer.gameObject.SetActive(false);
        }


        //temp method for chest queue text
        public void OnChestAddedToQueue(ChestController controller)
        {
            if (chestController == null || chestController.ChestID != controller.ChestID) return;

            chestState.text = "Queued";
        }


        public void UIChestSlotViewUnlockingState(ChestController controller, int timeRemainingToUnlockChest)
        {
            if (chestController == null || chestController.ChestID != controller.ChestID) return;

            timer.gameObject.SetActive(true);
            timer.text = timeRemainingToUnlockChest.ToString() + " sec";

            chestState.text = chestController.ChestStateMachine.CurrentState.ChestState.ToString();
        }


        public void UIChestSlotViewUnlockedState(ChestController controller)
        {
            if (chestController == null || chestController.ChestID != controller.ChestID) return;

            timer.gameObject.SetActive(false);

            chestState.text = chestController.ChestStateMachine.CurrentState.ChestState.ToString();
        }
    }
}

