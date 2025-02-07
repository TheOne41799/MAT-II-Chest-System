using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;

        public ChestModelSO chestModelSO {  get; private set; }

        public ChestController(ChestModelSO chestModelSO, ChestView chestViewPrefab)
        {
            this.chestModelSO = chestModelSO;

            chestModel = new ChestModel(chestModelSO);
            chestView = GameObject.Instantiate(chestViewPrefab);

            chestModel.SetChestController(this);
            chestView.SetChestController(this);
        }
    }
}
