using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chests
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Image chestImage;

        private ChestType chestType;

        private ChestController chestController;

        public void SetChestController(ChestController controller)
        {
            chestController = controller;

            // choose a proper function
            chestType = chestController.chestModelSO.ChestType;
            chestImage.sprite = chestController.chestModelSO.ChestSprite;
        }
    }
}
