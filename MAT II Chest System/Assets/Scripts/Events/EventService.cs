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

        public GameEventController OnGenerateChestButtonClicked { get; private set; }
        public GameEventController<ChestController> OnChestAdded { get; private set; }
        public GameEventController<int> OnChestUnlockButtonClicked { get; private set; }


        public EventService()
        {
            OnGenerateChestButtonClicked = new GameEventController();
            OnChestAdded = new GameEventController<ChestController>();
            OnChestUnlockButtonClicked = new GameEventController<int>();
        }
    }
}
