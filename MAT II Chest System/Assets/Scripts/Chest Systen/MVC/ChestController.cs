using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Events;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;
        private UIService uiService;

        public ChestModelSO chestModelSO {  get; private set; }

        public ChestController(ChestModelSO chestModelSO, ChestView chestViewPrefab, UIService uiService)
        {
            this.chestModelSO = chestModelSO;
            this.uiService = uiService;

            chestModel = new ChestModel(chestModelSO);
            chestView = GameObject.Instantiate(chestViewPrefab, uiService.uiChestSystemViewController.UIChestSlotsContainer); ///////////

            chestModel.SetChestController(this);
            chestView.SetChestController(this);

            chestView.UpdateVariables();
            
        }
    }
}
