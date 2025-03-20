using ChestSystem.Audio;
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
        [SerializeField] private GameObject uiChestAddedToQueuePopup;
        [SerializeField] private GameObject uiChestAlreadyQueuedPopup;
        [SerializeField] private GameObject uiChestAlreadyUnlockedPopup;

        [SerializeField] private GameObject uiPlayerHasInsufficientGemsPopup;
        [SerializeField] private GameObject uiChestUnlockedWithGemsPopup;
        [SerializeField] private GameObject uiUndoChestUnlockedWithGemsPopup;
        [SerializeField] private GameObject uiCantUndoChestUnlockedWithTimerPopup;

        [SerializeField] private GameObject uiCollectRewardsOrUndoChestUnlockPopup;

        private List<GameObject> allUIPopupsList = new List<GameObject>();

        #region UI Popups Close Buttons
        [SerializeField] private Button uiPopupUnlockChestWithTimerButton;
        [SerializeField] private Button uiPopupUnlockChestWithGemsButton;
        [SerializeField] private Button uiPopupChestSlotsFullCloseButton;
        [SerializeField] private Button uiPopupChestAlreadyUnlockingCloseButton;
        [SerializeField] private Button uiPopupChestAddedToQueueCloseButton;
        [SerializeField] private Button uiPopupChestAlreadyQueuedCloseButton;
        [SerializeField] private Button uiPopupChestAlreadyUnlockedCloseButton;
        [SerializeField] private Button uiPopupPlayerHasInsufficientGemsCloseButton;
        #endregion

        #region UI Popups Close Buttons
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithTimerText;
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithGemsText;
        #endregion

        #region UI Popups Collect Rewards
        [SerializeField] private Button uiPopupUndoChestUnlockButton;
        [SerializeField] private Button uiPopupCollectRewardsButton;        
        [SerializeField] private TextMeshProUGUI uiPopupCollectRewardsCoinsText;
        [SerializeField] private TextMeshProUGUI uiPopupCollectRewardsGemsText;
        #endregion

        #region UI Popups Chest Unlock/ Undo Unlock with gems or timer
        [SerializeField] private TextMeshProUGUI uiPopupChestUnlockedWithGemsText;
        [SerializeField] private TextMeshProUGUI uiPopupUndoChestUnlockedWithGemsText;
        [SerializeField] private Button uiPopupCantUndoChestUnlockedWithTimerCloseButton;
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
            allUIPopupsList.Add(uiChestAddedToQueuePopup);
            allUIPopupsList.Add(uiChestAlreadyQueuedPopup);
            allUIPopupsList.Add(uiChestAlreadyUnlockedPopup);
            allUIPopupsList.Add(uiPlayerHasInsufficientGemsPopup);
            allUIPopupsList.Add(uiCollectRewardsOrUndoChestUnlockPopup);
            allUIPopupsList.Add(uiChestUnlockedWithGemsPopup);
            allUIPopupsList.Add(uiUndoChestUnlockedWithGemsPopup);
            allUIPopupsList.Add(uiCantUndoChestUnlockedWithTimerPopup);
        }

        private void DeactivateUIPopups()
        {
            for (int i = 0; i < allUIPopupsList.Count; i++)
            {
                allUIPopupsList[i].SetActive(false);
            }

            PlayPopupMusic();
        }

        private void OnEnable()
        {
            uiPopupUnlockChestWithTimerButton.onClick.AddListener(UnlockChestWithTimer);
            uiPopupUnlockChestWithGemsButton.onClick.AddListener(UnlockChestWithGems);

            uiPopupChestSlotsFullCloseButton.onClick.AddListener(DeactivateUIPopups);
            uiPopupChestAlreadyUnlockingCloseButton.onClick.AddListener(DeactivateUIPopups);
            uiPopupChestAddedToQueueCloseButton.onClick.AddListener(DeactivateUIPopups);
            uiPopupChestAlreadyQueuedCloseButton.onClick.AddListener(DeactivateUIPopups);
            uiPopupChestAlreadyUnlockedCloseButton.onClick.AddListener (DeactivateUIPopups);
            uiPopupPlayerHasInsufficientGemsCloseButton.onClick.AddListener(DeactivateUIPopups);

            uiPopupUndoChestUnlockButton.onClick.AddListener(UndoChestUnlock);
            uiPopupCantUndoChestUnlockedWithTimerCloseButton.onClick.AddListener(CloseUIPopupCantUndoChestUnlockedWithTimer);
            uiPopupCollectRewardsButton.onClick.AddListener(CollectRewards);

            EventService.Instance.OnUIPopupActivate.AddListener(UIPopupManager);
            EventService.Instance.OnUIPopupChestUnlockActivate.AddListener(UIPopupUnlockChestManager);
            
            EventService.Instance.OnUpdateGemsAndTimeRequiredToUnlockChest.AddListener(UpdateTimeAndGemsRequiredTextOnChestLocking);

            EventService.Instance.OnUIPopupCollectRewardsOrUndoChestUnlock.AddListener(UIPopupCollectRewardsOrUndoChestUnlock);

            EventService.Instance.OnCloseUIPopups.AddListener(DeactivateUIPopups);
        }

        private void UIPopupManager(UIPopups uiPopup)
        {
            PlayPopupMusic();

            switch (uiPopup)
            {
                case UIPopups.UI_CHEST_UNLOCK_POPUP:
                    DeactivateUIPopups();
                    uiChestUnlockPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_SLOTS_FULL_POPUP:
                    DeactivateUIPopups();
                    uiChestSlotsFullPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ALREADY_UNLOCKING:
                    DeactivateUIPopups();
                    uiChestAlreadyUnlockingPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ADDED_TO_QUEUE:
                    DeactivateUIPopups();
                    uiChestAddedToQueuePopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ALREADY_QUEUED:
                    DeactivateUIPopups();
                    uiChestAlreadyQueuedPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_UNLOCKED_WITH_GEMS:                    
                    DeactivateUIPopups();
                    ChestUnlockWithGemsTextUpdate();
                    uiChestUnlockedWithGemsPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ALREADY_UNLOCKED:
                    DeactivateUIPopups();
                    uiChestAlreadyUnlockedPopup.SetActive(true);
                    break;
                case UIPopups.UI_PLAYER_HAS_INSUFFICIENT_GEMS:
                    DeactivateUIPopups();
                    uiPlayerHasInsufficientGemsPopup.SetActive(true);
                    break;
                case UIPopups.UI_COLLECT_REWARDS_OR_UNDO_CHEST_UNLOCK:
                    DeactivateUIPopups();
                    uiCollectRewardsOrUndoChestUnlockPopup.SetActive(true);
                    break;
                case UIPopups.UI_UNDO_CHEST_UNLOCK_WITH_GEMS:
                    DeactivateUIPopups();
                    UndoChestUnlockWithGemsTextUpdate();
                    uiUndoChestUnlockedWithGemsPopup.SetActive(true);
                    break;
                case UIPopups.UI_CANT_UNDO_CHEST_UNLOCKED_WITH_TIMER:
                    uiCantUndoChestUnlockedWithTimerPopup.SetActive(true);
                    break;
            }
        }

        private void PlayPopupMusic()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.BUTTON_CLICKED, false);
        }

        private void UIPopupUnlockChestManager(ChestController controller, UIPopups popup)
        {
            activeChestController = controller;

            UpdateTimeAndGemsRequiredTextOnChestLocked(controller);

            UIPopupManager(popup);
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

        private void UIPopupCollectRewardsOrUndoChestUnlock(ChestController controller, UIPopups popup)
        {
            activeChestController = controller;

            UpdateRewardsCoinsAndGemsInChest(activeChestController);

            UIPopupManager(popup);
        }

        private void UpdateRewardsCoinsAndGemsInChest(ChestController controller)
        {
            uiPopupCollectRewardsCoinsText.text = controller.ChestModel.CoinsInTheChest.ToString();
            uiPopupCollectRewardsGemsText.text = controller.ChestModel.GemsInChest.ToString();
        }

        private void UndoChestUnlock()
        {
            EventService.Instance.OnUndoChestUnlockWithGems.InvokeEvent(activeChestController);
        }

        private void ChestUnlockWithGemsTextUpdate()
        {
            uiPopupChestUnlockedWithGemsText.text = activeChestController.UpdatedGemsRequiredToUnlockChest.ToString();
        }

        private void UndoChestUnlockWithGemsTextUpdate()
        {
            uiPopupUndoChestUnlockedWithGemsText.text = activeChestController.UpdatedGemsRequiredToUnlockChest.ToString();
        }

        private void CollectRewards()
        {
            DeactivateUIPopups();

            EventService.Instance.OnChestCollected.InvokeEvent(activeChestController.ChestModel.CoinsInTheChest,
                                                               activeChestController.ChestModel.GemsInChest);

            EventService.Instance.OnChestRemoved.InvokeEvent(activeChestController);
        }        

        private void CloseUIPopupCantUndoChestUnlockedWithTimer()
        {
            uiCantUndoChestUnlockedWithTimerPopup.SetActive(false);

            PlayPopupMusic();
        }
    }    
}