using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChestSystem.Events;

namespace ChestSystem.Chests
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Button unlockButton;
        [SerializeField] private Image chestImage;
        [SerializeField] private TextMeshProUGUI chestTypeText;
        [SerializeField] private TextMeshProUGUI timeToUnlockChestText;

        private ChestController chestController;      

        public void SetChestController(ChestController controller)
        {
            chestController = controller;
        }

        public void InitializeVariables()
        {
            unlockButton.onClick.AddListener(ChestUnlockButtonClicked);
        }

        public void ChestLockedStateUI()
        {
            chestImage.sprite = chestController.chestModel.chestModelSO.ChestSprite;
            chestTypeText.text = chestController.chestModel.chestModelSO.ChestType.ToString();

            timeToUnlockChestText.gameObject.SetActive(false);
            timeToUnlockChestText.text = chestController.chestModel.chestModelSO.TimeRequiredToUnlockChest.ToString() + " s";
        }      
        
        private void ChestUnlockButtonClicked()
        {
            EventService.Instance.OnChestUnlockClicked.InvokeEvent(chestController);
        }
    }
}
