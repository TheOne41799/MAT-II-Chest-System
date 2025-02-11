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

        private ChestController chestController;

        private void Awake()
        {
            chestUnlockUIPopup?.SetActive(false);

            EventService.Instance.OnChestUnlockClicked.AddListener(ChestUnlockButtonClicked);

            unlockWithTimerButton.onClick.AddListener(UnlockChestWithTimerButton);
            unlockWithGemsButton.onClick.AddListener(UnlockChestWithGemsButton);
        }

        public void ChestUnlockButtonClicked(ChestController chestController)
        {
            this.chestController = chestController;
            chestUnlockUIPopup?.SetActive(true);            
        }

        private void UnlockChestWithTimerButton()
        {
            chestController?.UnlockingChestWithTimer();
        }

        private void UnlockChestWithGemsButton()
        {
            chestController?.UnlockingChestWithGems();
        }
    }
}