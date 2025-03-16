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

        private List<GameObject> allUIPopupsList = new List<GameObject>();

        #region UI Popups Close Buttons
        [SerializeField] private Button uiPopupUnlockChestWithTimerButton;
        [SerializeField] private Button uiPopupUnlockChestWithGemsButton;
        [SerializeField] private Button uiPopupChestSlotsFullCloseButton;
        [SerializeField] private Button uiChestAlreadyUnlockingCloseButton;
        [SerializeField] private Button uichestAlreadyUnlockedCloseButton;
        #endregion

        #region UI Popups Close Buttons
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithTimerText;
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithGemsText;
        #endregion

        private ChestController chestController;


        private void Awake()
        {
            InitializeAllUIPopupsList();
            DeactivateUIPopups();
        }

        private void InitializeAllUIPopupsList()
        {
            allUIPopupsList.Add(uiChestUnlockPopup);
            allUIPopupsList.Add(uiChestSlotsFullPopup);
            allUIPopupsList.Add (uiChestAlreadyUnlockingPopup);
            allUIPopupsList.Add(uiChestAlreadyUnlockedPopup);
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
            uiChestAlreadyUnlockingCloseButton.onClick.AddListener(DeactivateUIPopups);
            uichestAlreadyUnlockedCloseButton.onClick.AddListener (DeactivateUIPopups);

            EventService.Instance.OnUIPopupActivate.AddListener(UIPopupManager);
            EventService.Instance.OnUIPopupChestUnlockActivate.AddListener(UIPopupUnlockChestManager);
            //EventService.Instance.OnAllUIPopupsDeactivate.AddListener(DeactivateUIPopups);
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
                    //Debug.Log("Chest is already unlocking");
                    uiChestAlreadyUnlockingPopup.SetActive(true);
                    break;
                case UIPopups.UI_CHEST_ALREADY_UNLOCKED:
                    DeactivateUIPopups();
                    uiChestAlreadyUnlockedPopup.SetActive(true);
                    break;
            }
        }

        private void UIPopupUnlockChestManager(ChestController controller, UIPopups popup)
        {
            chestController = controller;

            uiPopupUnlockChestWithTimerText.text = controller.ChestModel.TimeRequiredToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = controller.ChestModel.GemsRequiredToUnlockChest.ToString() + " gems";

            DeactivateUIPopups();
            uiChestUnlockPopup.SetActive(true);
        }

        private void UnlockChestWithTimer()
        {
            DeactivateUIPopups();

            //EventService.Instance.OnUnlockChest.InvokeEvent(chestController, ChestUnlockMethod.WITH_TIMER);


            if(!chestController.IsChestUnlocking)
            {
                EventService.Instance.OnUnlockChest.InvokeEvent(chestController, ChestUnlockMethod.WITH_TIMER);
            }
            else
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKING);
            }



            
            /*Debug.Log("Unlock with Timer");
            Debug.Log(chestController.ChestID);*/
        }

        private void UnlockChestWithGems()
        {
            DeactivateUIPopups();


            //EventService.Instance.OnUnlockChest.InvokeEvent(chestController, ChestUnlockMethod.WITH_GEMS);



            //3 scaenarios
            
            if(chestController.ChestStateMachine.CurrentState.ChestState == ChestState.LOCKED
               || chestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKING)
            {
                //Debug.Log("Chest state is locked or unlocked");

                EventService.Instance.OnUnlockChest.InvokeEvent(chestController, ChestUnlockMethod.WITH_GEMS);
            }
            else if(chestController.ChestStateMachine.CurrentState.ChestState == ChestState.UNLOCKED)
            {
                UIPopupManager(UIPopups.UI_CHEST_ALREADY_UNLOCKED);
            }


            //



            /*Debug.Log("Unlock with Gems");
            Debug.Log(chestController.ChestID);*/
        }
    }    
}