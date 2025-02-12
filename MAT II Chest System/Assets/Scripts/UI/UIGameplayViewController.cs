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

        //this is a mistake
        private ChestController chestController;

        private void Awake()
        {
            chestUnlockUIPopup?.SetActive(false);

            EventService.Instance.OnChestUnlockClicked.AddListener(ChestUnlockButtonClicked);
            EventService.Instance.OnChestUnlocked.AddListener(ChestUnlocked);

            unlockWithTimerButton.onClick.AddListener(UnlockChestWithTimerButton);
            unlockWithGemsButton.onClick.AddListener(UnlockChestWithGemsButton);
        }

        public void ChestUnlockButtonClicked(ChestController chestController)
        {
            this.chestController = chestController;

            timerText.text = chestController.chestModel.chestModelSO.TimeRequiredToUnlockChest.ToString() + " sec";
            gemsText.text = chestController.chestModel.chestModelSO.GemsRequiredToUnlockChest.ToString() + " gems";

            chestUnlockUIPopup?.SetActive(true);            
        }

        private void UnlockChestWithTimerButton()
        {
            chestUnlockUIPopup?.SetActive(false);
            chestController?.UnlockingChestWithTimer();
        }

        private void UnlockChestWithGemsButton()
        {
            // first look for an if condition - does player has required gems
            // just create an event

            chestUnlockUIPopup?.SetActive(false);
            chestController?.UnlockingChestWithGems();
        }

        private void ChestUnlocked(ChestController controller)
        {
            // this is a mistake
            if (chestController == controller)
            {
                chestController.UnlockedChest();
            }
            //chestController.UnlockedChest(controller);
        }
    }
}