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

        private Dictionary<int, ChestView> chestSlots = new Dictionary<int, ChestView>();

        [SerializeField] private int totalNumberOfUIChestSlots;
        private int currentChestSlotNumber = 0;

        private List<GameObject> allUIChestSlots;

        private void Start()
        {
            InitializeVaribales();
            CreateEmptyUIChestSlots();
        }

        private void InitializeVaribales()
        {
            generateChestButton.onClick.AddListener(GenerateChest);
            allUIChestSlots = new List<GameObject>();
            currentChestSlotNumber = 0;
        }        

        private void CreateEmptyUIChestSlots()
        {
            for (int i = 0; i < totalNumberOfUIChestSlots; i++)
            {                
                GameObject emptyChestSlot = Instantiate(emptyUIChestSlot, uiChestSlotsContainer);
                allUIChestSlots.Add(emptyChestSlot);
            }
        }

        public void GenerateChest()
        {
            if (currentChestSlotNumber >= totalNumberOfUIChestSlots)
            {
                Debug.LogWarning("No more available slots!");
                return;
            }

            EventService.Instance.OnGenerateChestButtonClicked.InvokeEvent(currentChestSlotNumber);
        }

        public void ChestAdded(ChestView chestView)
        {
            Debug.Log("Chest has been added at index: " + currentChestSlotNumber);

            if (allUIChestSlots[currentChestSlotNumber] != null)
            {
                Destroy(allUIChestSlots[currentChestSlotNumber]);
            }

            chestSlots[currentChestSlotNumber] = chestView;

            chestView.transform.SetParent(uiChestSlotsContainer);
            chestView.transform.SetSiblingIndex(currentChestSlotNumber);

            currentChestSlotNumber++;
        }
    }
}