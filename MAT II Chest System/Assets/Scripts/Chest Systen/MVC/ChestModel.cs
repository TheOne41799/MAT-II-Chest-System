using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestModel
    {
        public ChestType ChestType { get; }
        public Sprite ChestSprite { get; }
        public int CoinsInTheChest { get; }
        public int GemsInChest { get; }
        public int TimeRequiredToUnlockChest { get; }
        public int MinimumGemsRequiredToUnlockChest { get; }


        public ChestModel(ChestModelSO chestModelSO, int coins, int gems)
        {
            this.ChestType = chestModelSO.ChestType;
            this.ChestSprite = chestModelSO.ChestSprite;

            this.CoinsInTheChest = coins;
            this.GemsInChest = gems;            

            this.TimeRequiredToUnlockChest = chestModelSO.TimeRequiredToUnlockChest;
            this.MinimumGemsRequiredToUnlockChest = chestModelSO.MinimumGemsRequiredToUnlockChest;
        }
    }
}
