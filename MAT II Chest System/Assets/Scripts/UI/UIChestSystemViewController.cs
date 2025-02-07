using ChestSystem.Chests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UIChestSystemViewController : MonoBehaviour
    {
        [SerializeField] private int totalNumberOfUIChestSlots;
        [SerializeField] private GameObject UIChestSlot;

        [SerializeField] private Transform uiChestSlotsContainer;
        public Transform UIChestSlotsContainer { get { return uiChestSlotsContainer; } }
        
        private List<GameObject> uiChestSlots = new List<GameObject>();
        private List<ChestView> uiChestViewSlots = new List<ChestView>();
        private int currentChestSlotNumber = 1;

        private void Start()
        {
            CreateUIChestSlots();
        }

        private void CreateUIChestSlots()
        {
            for (int i = 0; i < totalNumberOfUIChestSlots; i++)
            {
                GameObject chestSlot = Instantiate(UIChestSlot, uiChestSlotsContainer);
                uiChestSlots.Add(chestSlot);
            }
        }

        /*public void ChestAdded(ChestView chestView)
        {
            Debug.Log("Helo");
            chestView.gameObject.transform.parent = uiChestSlotsContainer;
            uiChestViewSlots.Add(chestView);            
        }*/

        private void ChestRemoved()
        {

        }
    }
}