using ChestSystem.Chests;
using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIPopupsViewController : MonoBehaviour
    {
        [SerializeField] private GameObject uiChestUnlockPopup;
        [SerializeField] private GameObject uiChestSlotsFullPopup;
        [SerializeField] private GameObject uiChestAlreadyUnlockingPopup;
        [SerializeField] private GameObject uiChestAlreadyQueuedPopup;
        [SerializeField] private GameObject uiChestAlreadyUnlockedPopup;
        [SerializeField] private GameObject uiPlayerHasInsufficientGemsPopup;

        private List<GameObject> allUIPopupsList = new List<GameObject>();

        #region UI Popups Close Buttons
        [SerializeField] private Button uiPopupUnlockChestWithTimerButton;
        [SerializeField] private Button uiPopupUnlockChestWithGemsButton;
        [SerializeField] private Button uiPopupChestSlotsFullCloseButton;
        [SerializeField] private Button uiPopupChestAlreadyUnlockingCloseButton;
        [SerializeField] private Button uiPopupChestAlreadyQueuedCloseButton;
        [SerializeField] private Button uiPopupChestAlreadyUnlockedCloseButton;
        [SerializeField] private Button uiPopupPlayerHasInsufficientGemsCloseButton;
        #endregion

        #region UI Popups Close Buttons
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithTimerText;
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithGemsText;
        #endregion

        private ChestController activeChestController;


        private void Awake()
        {
            InitializeAllUIPopupsList();
            DeactivateUIPopups();
        }

        private void InitializeAllUIPopupsList()
        {
            allUIPopupsList.Add(uiChestUnlockPopup);
            allUIPopupsList.Add(uiChestSlotsFullPopup);
            allUIPopupsList.Add(uiChestAlreadyUnlockingPopup);
            allUIPopupsList.Add(uiChestAlreadyQueuedPopup);
            allUIPopupsList.Add(uiChestAlreadyUnlockedPopup);
            allUIPopupsList.Add(uiPlayerHasInsufficientGemsPopup);
        }

        private void DeactivateUIPopups()
        {
            for (int i = 0; i < allUIPopupsList.Count; i++)
            {
                allUIPopupsList[i].SetActive(false);
            }
        }

        private void OnEnable()
        {
            uiPopupUnlockChestWithTimerButton.onClick.AddListener(UnlockChestWithTimer);
            uiPopupUnlockChestWithGemsButton.onClick.AddListener(UnlockChestWithGems);

            uiPopupChestSlotsFullCloseButton.onClick.AddListener(DeactivateUIPopups);
            uiPopupChestAlreadyUnlockingCloseButton.onClick.AddListener(DeactivateUIPopups);
            uiPopupChestAlreadyQueuedCloseButton.onClick.AddListener(DeactivateUIPopups);
            uiPopupChestAlreadyUnlockedCloseButton.onClick.AddListener (DeactivateUIPopups);
            uiPopupPlayerHasInsufficientGemsCloseButton.onClick.AddListener(DeactivateUIPopups);

            EventService.Instance.OnUIPopupActivate.AddListener(UIPopupManager);
            EventService.Instance.OnUIPopupChestUnlockActivate.AddListener(UIPopupUnlockChestManager);
            
            EventService.Instance.OnUpdateGemsAndTimeRequiredToUnlockChest.AddListener(UpdateTimeAndGemsRequiredTextOnChestLocking);
        }

        private void UIPopupManager(UIPopups uiPopup)
        {
            switch(uiPopup)
            {
                case UIPopups.UI_CHEST_SLOTS_FULL_POPUP:
                    DeactivateUIPopups();
                    uiChestSlotsFullPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ALREADY_UNLOCKING:
                    DeactivateUIPopups();
                    uiChestAlreadyUnlockingPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ALREADY_QUEUED:
                    DeactivateUIPopups();
                    uiChestAlreadyQueuedPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ALREADY_UNLOCKED:
                    DeactivateUIPopups();
                    uiChestAlreadyUnlockedPopup.SetActive(true);
                    break;
                case UIPopups.UI_PLAYER_HAS_INSUFFICIENT_GEMS:
                    DeactivateUIPopups();
                    uiPlayerHasInsufficientGemsPopup.SetActive(true);
                    break;
            }
        }

        private void UIPopupUnlockChestManager(ChestController controller, UIPopups popup)
        {
            activeChestController = controller;

            UpdateTimeAndGemsRequiredTextOnChestLocked(controller);

            DeactivateUIPopups();
            uiChestUnlockPopup.SetActive(true);
        }

        private void UpdateTimeAndGemsRequiredTextOnChestLocked(ChestController chestController)
        {            
            uiPopupUnlockChestWithTimerText.text = activeChestController.UpdatedTimeRemainingToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = activeChestController.UpdatedGemsRequiredToUnlockChest.ToString() + " gems";
        }

        private void UpdateTimeAndGemsRequiredTextOnChestLocking(ChestController controller)
        {
            if (activeChestController != controller) return;

            uiPopupUnlockChestWithTimerText.text = activeChestController.UpdatedTimeRemainingToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = activeChestController.UpdatedGemsRequiredToUnlockChest.ToString() + " gems";
        }


        private void UnlockChestWithTimer()
        {
            DeactivateUIPopups();

            if (activeChestController.ChestStateMachine.CurrentState is ChestLockedState)
            {
                EventService.Instance.OnUnlockChest.InvokeEvent(activeChestController, ChestUnlockMethod.WITH_TIMER);
            }
            else if(activeChestController.ChestStateMachine.CurrentState is ChestUnlockingState)
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKING);
            }
            else if(activeChestController.ChestStateMachine.CurrentState is ChestUnlockedState)
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKED);
            }
        }

        private void UnlockChestWithGems()
        {
            DeactivateUIPopups();
            
            if(activeChestController.ChestStateMachine.CurrentState.ChestState == ChestState.LOCKED
               || activeChestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKING)
            {
                EventService.Instance.OnUnlockChest.InvokeEvent(activeChestController, ChestUnlockMethod.WITH_GEMS);
            }
            else if(activeChestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKED)
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKED);
            }
        }
    }    
}