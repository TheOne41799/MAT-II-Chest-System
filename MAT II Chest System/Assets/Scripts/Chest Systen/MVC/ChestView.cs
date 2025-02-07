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
        [SerializeField] private int timeToUnlockChest;

        //private ChestType chestType;

        private ChestController chestController;

        public void SetChestController(ChestController controller)
        {
            chestController = controller;

            // choose a proper function
            //chestType = chestController.chestModelSO.ChestType;
            //chestImage.sprite = chestController.chestModelSO.ChestSprite;

            //choose appropriate location to call
        }

        public void UpdateVariables()
        {
            ////???????????
            chestTypeText.text = chestController.chestModel.chestModelSO.ChestType.ToString();
            
        }
    }
}
