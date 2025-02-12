using ChestSystem.Chests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Events;

namespace ChestSystem.UI
{
    public class UIChestSystemViewController : MonoBehaviour
    {        
        [SerializeField] private GameObject emptyUIChestSlot;

        [SerializeField] private Button generateChestButton;

        [SerializeField] private Transform uiChestSlotsContainer;

        private Dictionary<int, ChestController> chestControllers = new Dictionary<int, ChestController>();
        private Dictionary<int, GameObject> uiChestSlots = new Dictionary<int, GameObject>();

        [SerializeField] private int totalNumberOfUIChestSlots;
        private int currentChestSlotNumber = 0;

        private void Start()
        {
            InitializeVaribales();
            CreateEmptyUIChestSlots();
        }

        private void InitializeVaribales()
        {
            generateChestButton.onClick.AddListener(GenerateChest);
            currentChestSlotNumber = 0;
        }

        private void CreateEmptyUIChestSlots()
        {
            for (int i = 0; i < totalNumberOfUIChestSlots; i++)
            {
                GameObject emptyChestSlot = Instantiate(emptyUIChestSlot, uiChestSlotsContainer);
                uiChestSlots[i] = emptyChestSlot;
            }
        }

        public void GenerateChest()
        {
            if (currentChestSlotNumber >= totalNumberOfUIChestSlots)
            {
                // Create a popup for no extra slots
                return;
            }

            //EventService.Instance.OnGenerateChestButtonClicked.InvokeEvent(currentChestSlotNumber);
            EventService.Instance.OnGenerateChestButtonClicked.InvokeEvent();
            //currentChestSlotNumber++;
        }        

        public void ChestAdded(ChestController chestController)
        {
            if (currentChestSlotNumber >= totalNumberOfUIChestSlots)
            {
                Debug.Log("No empty slot available!");
                return;
            }

            GameObject emptySlot = uiChestSlots[currentChestSlotNumber];

            Destroy(emptySlot);
            uiChestSlots[currentChestSlotNumber] = chestController.chestView.gameObject;

            chestControllers[currentChestSlotNumber] = chestController;

            chestController.chestView.transform.SetParent(uiChestSlotsContainer);
            chestController.chestView.transform.SetSiblingIndex(currentChestSlotNumber);

            currentChestSlotNumber++;
        }

        public void RemoveChest(int chestID)
        {
            if (chestControllers.ContainsKey(chestID))
            {
                Destroy(chestControllers[chestID].chestView.gameObject);
                chestControllers.Remove(chestID);

                GameObject newEmptySlot = Instantiate(emptyUIChestSlot, uiChestSlotsContainer);
                uiChestSlots[chestID] = newEmptySlot;
                newEmptySlot.transform.SetSiblingIndex(chestID);
            }
        }
    }
}