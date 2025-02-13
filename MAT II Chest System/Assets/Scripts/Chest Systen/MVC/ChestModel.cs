using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestModel
    {
        public ChestModelSO chestModelSO {  get; private set; }
        private ChestController chestController;

        public int numberOfCoinsInChest { get; private set; }
        public int numberOfGemsInChest { get; private set; }

        public ChestModel(ChestModelSO chestModelSO)
        {
            this.chestModelSO = chestModelSO;

            numberOfCoinsInChest = GenerateRandomCoinsInChest();
            numberOfGemsInChest = GenerateRandomGemsInChest();
        }

        public void SetChestController(ChestController controller)
        {
            chestController = controller;
        }

        private int GenerateRandomCoinsInChest()
        {
            int randCoins = Random.Range(chestModelSO.MinimumCoinsInChest, chestModelSO.MaximumCoinsInChest);
            return randCoins;
        }

        private int GenerateRandomGemsInChest()
        {
            int randGems = Random.Range(chestModelSO.MinimuGemsInChest, chestModelSO.MaximumGemsInChest);
            return randGems;
        }
    }
}
