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
        [SerializeField] private GameObject uiChestAlreadyUnlockedPopup;
        [SerializeField] private GameObject uiPlayerHasInsufficientGemsPopup;

        private List<GameObject> allUIPopupsList = new List<GameObject>();

        #region UI Popups Close Buttons
        [SerializeField] private Button uiPopupUnlockChestWithTimerButton;
        [SerializeField] private Button uiPopupUnlockChestWithGemsButton;
        [SerializeField] private Button uiPopupChestSlotsFullCloseButton;
        [SerializeField] private Button uiPopupChestAlreadyUnlockingCloseButton;
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
            uiPopupChestAlreadyUnlockedCloseButton.onClick.AddListener (DeactivateUIPopups);
            uiPopupPlayerHasInsufficientGemsCloseButton.onClick.AddListener(DeactivateUIPopups);

            EventService.Instance.OnUIPopupActivate.AddListener(UIPopupManager);
            EventService.Instance.OnUIPopupChestUnlockActivate.AddListener(UIPopupUnlockChestManager);
            //EventService.Instance.OnAllUIPopupsDeactivate.AddListener(DeactivateUIPopups);



            //test
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

            /*uiPopupUnlockChestWithTimerText.text = controller.ChestModel.TimeRequiredToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = controller.ChestModel.MinimumGemsRequiredToUnlockChest.ToString() + " gems";*/

            UpdateTimeAndGemsRequiredTextOnChestLocked(controller);

            //UpdateTimeAndGemsRequiredText();

            DeactivateUIPopups();
            uiChestUnlockPopup.SetActive(true);
        }

        /*private void UpdateTimeAndGemsRequiredText()
        {
            uiPopupUnlockChestWithTimerText.text = chestController.ChestModel.TimeRequiredToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = chestController.ChestModel.MinimumGemsRequiredToUnlockChest.ToString() + " gems";
        }*/

        /*private void UpdateTimeAndGemsRequiredText(ChestController chestController)
        {
            uiPopupUnlockChestWithTimerText.text = chestController.ChestModel.TimeRequiredToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = chestController.ChestModel.MinimumGemsRequiredToUnlockChest.ToString() + " gems";
        }*/

        private void UpdateTimeAndGemsRequiredTextOnChestLocked(ChestController chestController)
        {
            //Debug.Log("Locked");

            /*uiPopupUnlockChestWithTimerText.text = chestController.ChestModel.TimeRequiredToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = chestController.ChestModel.MinimumGemsRequiredToUnlockChest.ToString() + " gems";*/

            uiPopupUnlockChestWithTimerText.text = activeChestController.UpdatedTimeRemainingToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = activeChestController.UpdatedGemsRequiredToUnlockChest.ToString() + " gems";
        }

        private void UpdateTimeAndGemsRequiredTextOnChestLocking(ChestController controller)
        {
            //Debug.Log("Unlocking");

            if (activeChestController != controller) return;

            uiPopupUnlockChestWithTimerText.text = activeChestController.UpdatedTimeRemainingToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = activeChestController.UpdatedGemsRequiredToUnlockChest.ToString() + " gems";
        }









        private void UnlockChestWithTimer()
        {
            DeactivateUIPopups();

            //EventService.Instance.OnUnlockChest.InvokeEvent(chestController, ChestUnlockMethod.WITH_TIMER);


            /*if(activeChestController.ChestStateMachine.CurrentState is not ChestUnlockingState)
            {
                EventService.Instance.OnUnlockChest.InvokeEvent(activeChestController, ChestUnlockMethod.WITH_TIMER);
            }
            else
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKING);
            }*/


            if (activeChestController.ChestStateMachine.CurrentState is ChestLockedState)
            {
                EventService.Instance.OnUnlockChest.InvokeEvent(activeChestController, ChestUnlockMethod.WITH_TIMER);
            }
            else if(activeChestController.ChestStateMachine.CurrentState is ChestUnlockingState)
            {
                //queue related code maybe used here

                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKING);
            }
            else if(activeChestController.ChestStateMachine.CurrentState is ChestUnlockedState)
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKED);
            }


            /*Debug.Log("Unlock with Timer");
            Debug.Log(chestController.ChestID);*/
        }

        private void UnlockChestWithGems()
        {
            DeactivateUIPopups();


            //EventService.Instance.OnUnlockChest.InvokeEvent(chestController, ChestUnlockMethod.WITH_GEMS);



            //3 scaenarios

            
            if(activeChestController.ChestStateMachine.CurrentState.ChestState == ChestState.LOCKED
               || activeChestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKING)
            {
                EventService.Instance.OnUnlockChest.InvokeEvent(activeChestController, ChestUnlockMethod.WITH_GEMS);
            }
            else if(activeChestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKED)
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKED);
            }


            //



            /*Debug.Log("Unlock with Gems");
            Debug.Log(chestController.ChestID);*/
        }
    }    
}