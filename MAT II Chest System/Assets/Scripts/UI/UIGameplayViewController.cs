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

        [Header("Player Coins and Gems")]
        private int playerCoins = 0;
        private int playerGems = 0;
        [SerializeField] private TextMeshProUGUI playerCoinsText;
        [SerializeField] private TextMeshProUGUI playerGemsText;

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

            UpdatePlayerDetails(playerCoins, playerGems);
        }

        private void UpdatePlayerDetails(int coins, int gems)
        {
            playerCoins += coins;
            playerGems += gems;

            playerCoinsText.text = playerCoins.ToString();
            playerGemsText.text = playerGems.ToString();
        }

        public void ChestUnlockButtonClicked(ChestController chestController)
        {
            if (!chestControllers.ContainsKey(chestController.ChestID))
            {
                chestControllers[chestController.ChestID] = chestController;
            }

            ChestStateCheck(chestController);
        }

        private void ChestStateCheck(ChestController chestController)
        {
            switch (chestController.chestView.currentChestState)
            {
                case ChestState.LOCKED:
                    timerText.text = chestController.chestModel.chestModelSO.TimeRequiredToUnlockChest.ToString() + " sec";
                    gemsText.text = chestController.chestModel.chestModelSO.GemsRequiredToUnlockChest.ToString() + " gems";
                    chestUnlockUIPopup?.SetActive(true);
                    break;
                case ChestState.UNLOCKING:
                    // Note that this popup must have two buttons
                    // 1 - one for close
                    // 2 - another for purchase with gems
                    Debug.Log("Unlocking");
                    break;
                case ChestState.UNLOCKED:
                    Debug.Log("Unlocked");
                    break;
            }
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