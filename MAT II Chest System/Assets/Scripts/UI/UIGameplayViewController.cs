using ChestSystem.Chests;
using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIGameplayViewController : MonoBehaviour
    {
        [Header("UI Popups - Chest Unlock")]
        [SerializeField] private GameObject chestUnlockUIPopup;
        [SerializeField] private Button unlockWithTimerButton;
        [SerializeField] private Button unlockWithGemsButton;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI gemsText;

        private Dictionary<int, ChestController> chestControllers = new Dictionary<int, ChestController>();

        [Header("UI Popups - Chest Slots full")]
        [SerializeField] private GameObject chestSlotsFullPopup;
        [SerializeField] private Button chestSlotsFullPopupCloseButton;

        private void Awake()
        {
            chestUnlockUIPopup?.SetActive(false);
            chestSlotsFullPopup?.SetActive(false);

            EventService.Instance.OnChestUnlockClicked.AddListener(ChestUnlockButtonClicked);
            EventService.Instance.OnChestUnlocked.AddListener(ChestUnlocked);
            EventService.Instance.OnChestSlotsFull.AddListener(OpenChestSlotsFullPopup);

            unlockWithTimerButton.onClick.AddListener(UnlockChestWithTimerButton);
            unlockWithGemsButton.onClick.AddListener(UnlockChestWithGemsButton);
            chestSlotsFullPopupCloseButton.onClick.AddListener(CloseChestSlotsFullPopup);
        }

        public void ChestUnlockButtonClicked(ChestController chestController)
        {
            if (!chestControllers.ContainsKey(chestController.ChestID))
            {
                chestControllers[chestController.ChestID] = chestController;
            }

            timerText.text = chestController.chestModel.chestModelSO.TimeRequiredToUnlockChest.ToString() + " sec";
            gemsText.text = chestController.chestModel.chestModelSO.GemsRequiredToUnlockChest.ToString() + " gems";

            chestUnlockUIPopup?.SetActive(true);            
        }

        private void UnlockChestWithTimerButton()
        {
            chestUnlockUIPopup?.SetActive(false);

            foreach (var chest in chestControllers.Values)
            {
                chest.UnlockingChestWithTimer();
            }
        }

        private void UnlockChestWithGemsButton()
        {
            // first look for an if condition - does player has required gems
            // just create an event

            chestUnlockUIPopup?.SetActive(false);

            foreach (var chest in chestControllers.Values)
            {
                chest.UnlockingChestWithGems();
            }
        }

        private void ChestUnlocked(ChestController controller)
        {
            if (chestControllers.ContainsKey(controller.ChestID))
            {
                chestControllers[controller.ChestID].UnlockedChest();
                chestControllers.Remove(controller.ChestID);
            }
        }

        private void OpenChestSlotsFullPopup()
        {
            chestSlotsFullPopup.gameObject?.SetActive(true);
        }

        private void CloseChestSlotsFullPopup()
        {
            chestSlotsFullPopup.gameObject?.SetActive(false);
        }
    }
}