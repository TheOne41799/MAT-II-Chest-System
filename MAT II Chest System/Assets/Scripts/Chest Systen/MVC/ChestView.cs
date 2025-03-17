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
        /*[SerializeField] private Button unlockButton;
        [SerializeField] private Image chestImage;
        [SerializeField] private TextMeshProUGUI chestTypeText;
        [SerializeField] private TextMeshProUGUI chestStateText;
        [SerializeField] private TextMeshProUGUI timeToUnlockChestText;

        [SerializeField] private TextMeshProUGUI numberOfCoinsInChestText;
        [SerializeField] private TextMeshProUGUI numberOfGemsInChestText;

        private ChestController chestController;
        public ChestState currentChestState { get; set; }

        public void SetChestController(ChestController controller)
        {
            chestController = controller;
        }

        public void InitializeVariables()
        {
            unlockButton.onClick.AddListener(ChestUnlockButtonClicked);

            numberOfCoinsInChestText.text = chestController.chestModel.numberOfCoinsInChest.ToString();
            numberOfGemsInChestText.text = chestController.chestModel.numberOfGemsInChest.ToString();
        }

        public void ChestLockedStateUI()
        {
            chestImage.sprite = chestController.chestModel.chestModelSO.ChestSprite;
            chestTypeText.text = chestController.chestModel.chestModelSO.ChestType.ToString();

            chestStateText.text = currentChestState.ToString();

            timeToUnlockChestText.gameObject.SetActive(false);
        }      
        
        private void ChestUnlockButtonClicked()
        {
            EventService.Instance.OnChestUnlockClicked.InvokeEvent(chestController);
        }

        public void ChestUnlockingStateUI()
        {            
            timeToUnlockChestText.gameObject.SetActive(true);
        }

        public void UpdateTimer(float remainingTime)
        {
            if(remainingTime > 0)
            {
                chestStateText.text = currentChestState.ToString();
                timeToUnlockChestText.text = remainingTime.ToString() + " secs";
            }
        }

        public void ChestUnlockedStateUI()
        {
            timeToUnlockChestText.gameObject.SetActive(false);
            chestStateText.text = currentChestState.ToString();
        }*/
    }
}
