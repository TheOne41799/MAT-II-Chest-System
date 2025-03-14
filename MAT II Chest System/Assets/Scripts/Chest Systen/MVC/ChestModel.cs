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
        public int GemsRequiredToUnlockChest { get; }


        public ChestModel(ChestModelSO chestModelSO, int coins, int gems)
        {
            ChestType = chestModelSO.ChestType;
            ChestSprite = chestModelSO.ChestSprite;

            CoinsInTheChest = coins;
            GemsInChest = gems;            

            TimeRequiredToUnlockChest = chestModelSO.TimeRequiredToUnlockChest;
            GemsRequiredToUnlockChest = chestModelSO.GemsRequiredToUnlockChest;
        }
    }
}
