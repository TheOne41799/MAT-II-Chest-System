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

        private List<GameObject> allUIPopupsList = new List<GameObject>();

        #region UI Popups Close Buttons
        [SerializeField] private Button uiPopupUnlockChestWithTimerButton;
        [SerializeField] private Button uiPopupUnlockChestWithGemsButton;
        [SerializeField] private Button uiPopupChestSlotsFullButton;
        #endregion

        #region UI Popups Close Buttons
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithTimerText;
        [SerializeField] private TextMeshProUGUI uiPopupUnlockChestWithGemsText;
        #endregion


        private void Awake()
        {
            InitializeAllUIPopupsList();
            DeactivateUIPopups();
        }

        private void InitializeAllUIPopupsList()
        {
            allUIPopupsList.Add(uiChestUnlockPopup);
            allUIPopupsList.Add(uiChestSlotsFullPopup);

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
            uiPopupChestSlotsFullButton.onClick.AddListener(DeactivateUIPopups);            

            EventService.Instance.OnUIPopupActivate.AddListener(UIPopupManager);
            EventService.Instance.OnUIPopupChestUnlockActivate.AddListener(UIPopupUnlockChestManager);
            EventService.Instance.OnAllUIPopupsDeactivate.AddListener(DeactivateUIPopups);
        }

        private void UIPopupManager(UIPopups uiPopup)
        {
            switch(uiPopup)
            {
                case UIPopups.UI_CHEST_SLOTS_FULL_POPUP:
                    DeactivateUIPopups();
                    uiChestSlotsFullPopup.SetActive(true);
                    break;
            }
        }

        private void UIPopupUnlockChestManager(ChestController controller, UIPopups popup)
        {
            uiPopupUnlockChestWithTimerText.text = controller.ChestModel.TimeRequiredToUnlockChest.ToString() + " secs";
            uiPopupUnlockChestWithGemsText.text = controller.ChestModel.GemsRequiredToUnlockChest.ToString() + " gems";

            DeactivateUIPopups();
            uiChestUnlockPopup.SetActive(true);
        }

        private void UnlockChestWithTimer()
        {
            DeactivateUIPopups();
        }

        private void UnlockChestWithGems()
        {
            DeactivateUIPopups();
        }
    }    
}