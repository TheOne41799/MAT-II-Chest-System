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
        [SerializeField] private int totalChestSlots;

        [SerializeField] private Button generateChestButton;

        [SerializeField] private UIChestSlotViewController chestSlotViewControllerPrefab;
        [SerializeField] private GameObject uiChestSlotViewHolder;

        private List<UIChestSlotViewController> uIChestSlotViewControllers;
        private UIChestSlotViewController currentEmptyUIChestSlotViewControllerToBeFilled;


        private void Awake()
        {
            uIChestSlotViewControllers = new List<UIChestSlotViewController>();

            for (int i = 0; i < totalChestSlots; i++)
            {
                GameObject gameObject = Instantiate(chestSlotViewControllerPrefab.gameObject);
                gameObject.transform.SetParent(uiChestSlotViewHolder.transform, false);

                uIChestSlotViewControllers.Add(gameObject.GetComponent<UIChestSlotViewController>());
            }

            generateChestButton.onClick.AddListener(GenerateChestButtonClicked);

            EventService.Instance.OnChestAdded.AddListener(OnChestAdded);
        }

        private void GenerateChestButtonClicked()
        {
            foreach (UIChestSlotViewController controller in uIChestSlotViewControllers)
            {
                if(controller.ChestController == null)
                {
                    currentEmptyUIChestSlotViewControllerToBeFilled = controller;

                    EventService.Instance.OnGenerateChestButtonClicked.InvokeEvent();
                                        
                    return;
                }
            }

            currentEmptyUIChestSlotViewControllerToBeFilled = null;

            Debug.Log("Chest Slots are full");
        }

        private void OnChestAdded(ChestController controller)
        {
            currentEmptyUIChestSlotViewControllerToBeFilled.OnChestAdded(controller);
        }





        /*[SerializeField] private GameObject emptyUIChestSlot;

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
                EventService.Instance.OnChestSlotsFull.InvokeEvent();
                return;
            }

            EventService.Instance.OnGenerateChestButtonClicked.InvokeEvent();
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
            Debug.Log(chestID);

            if (chestControllers.ContainsKey(chestID))
            {
                currentChestSlotNumber--;
                Destroy(chestControllers[chestID].chestView.gameObject);
                chestControllers.Remove(chestID);

                GameObject newEmptySlot = Instantiate(emptyUIChestSlot, uiChestSlotsContainer);
                uiChestSlots[chestID] = newEmptySlot;
                newEmptySlot.transform.SetSiblingIndex(chestID);
            }
        }*/


    }
}