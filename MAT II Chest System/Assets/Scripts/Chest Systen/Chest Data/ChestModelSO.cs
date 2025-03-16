using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    [CreateAssetMenu(fileName = "Chest Model", menuName = "Chest System/ Chest Model")]
    public class ChestModelSO : ScriptableObject
    {
        [Header("Basic Chest Details")]
        [SerializeField] private ChestType chestType;
        public ChestType ChestType { get { return chestType; } }

        [SerializeField] private Sprite chestSprite;
        public Sprite ChestSprite { get { return chestSprite; } }


        [Header("Coins")]
        [SerializeField] private int minimumCoinsInChest;
        public int MinimumCoinsInChest { get { return minimumCoinsInChest; } }

        [SerializeField] private int maximumCoinsInChest;
        public int MaximumCoinsInChest { get { return maximumCoinsInChest; } }


        [Header("Gems")]
        [SerializeField] private int minimumGemsInChest;
        public int MinimuGemsInChest { get { return minimumGemsInChest; } }

        [SerializeField] private int maximumGemsInChest;
        public int MaximumGemsInChest { get { return maximumGemsInChest; } }


        [Header("Parameters related to unlocking the chest")]
        [SerializeField] private int timeRequiredToUnlockChest;
        public int TimeRequiredToUnlockChest { get { return timeRequiredToUnlockChest; } }

        [SerializeField] private int minimumGemsRequiredToUnlockChest;
        public int MinimumGemsRequiredToUnlockChest { get { return minimumGemsRequiredToUnlockChest; } }
    }
}