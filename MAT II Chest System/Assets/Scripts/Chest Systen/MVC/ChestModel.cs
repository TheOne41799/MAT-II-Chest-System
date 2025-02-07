using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestModel
    {
        private ChestModelSO chestModelSO;
        private ChestController chestController;

        public ChestModel(ChestModelSO chestModelSO)
        {
            this.chestModelSO = chestModelSO;
        }

        public void SetChestController(ChestController controller)
        {
            chestController = controller;
        }
    }
}
