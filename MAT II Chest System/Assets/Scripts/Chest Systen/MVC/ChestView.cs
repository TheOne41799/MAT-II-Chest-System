using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem.Chests
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Image chestImage;
        [SerializeField] private TextMeshProUGUI chestTypeText;
        [SerializeField] private TextMeshProUGUI timeToUnlockChestText;

        private ChestController chestController;

        public void SetChestController(ChestController controller)
        {
            chestController = controller;
        }

        public void ChestLockedStateUI()
        {
            chestImage.sprite = chestController.chestModel.chestModelSO.ChestSprite;
            chestTypeText.text = chestController.chestModel.chestModelSO.ChestType.ToString();

            timeToUnlockChestText.gameObject.SetActive(false);
            timeToUnlockChestText.text = chestController.chestModel.chestModelSO.TimeRequiredToUnlockChest.ToString() + " s";
        }
    }
}
