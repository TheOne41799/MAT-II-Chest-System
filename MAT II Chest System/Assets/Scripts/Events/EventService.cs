using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using ChestSystem.Chests;

namespace ChestSystem.Events
{
    public class EventService
    {
        private static EventService instance;
        public static EventService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventService();
                }
                return instance;
            }
        }

        //public GameEventController<int> OnGenerateChestButtonClicked { get; private set; }
        public GameEventController OnGenerateChestButtonClicked { get; private set; }
        public GameEventController<ChestController> OnChestCreated { get; private set; }
        public GameEventController<ChestController> OnChestUnlockClicked { get; private set; }
        public GameEventController<ChestController> OnChestUnlocked { get; private set; }
        public GameEventController OnChestSlotsFull { get; private set; }
        //public GameEventController<int> OnUnlockedChestOpened { get; private set; }

        public EventService()
        {
            //OnGenerateChestButtonClicked = new GameEventController<int>();
            OnGenerateChestButtonClicked = new GameEventController();
            OnChestCreated = new GameEventController<ChestController>();
            OnChestUnlockClicked = new GameEventController<ChestController>();
            OnChestUnlocked = new GameEventController<ChestController>();
            OnChestSlotsFull = new GameEventController();
            //OnUnlockedChestOpened = new GameEventController<int>();
        }
    }
}
