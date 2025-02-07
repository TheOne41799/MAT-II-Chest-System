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

        public GameEventController<int> OnGenerateChestButtonClicked { get; private set; }
        public GameEventController<ChestView> OnChestCreated { get; private set; }



        public EventService()
        {
            OnGenerateChestButtonClicked = new GameEventController<int>();
            OnChestCreated = new GameEventController<ChestView>();
        }
    }
}
