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
        private Dictionary<int, UIChestSlotViewController> chestSlotDictionary;


        private void Awake()
        {
            uIChestSlotViewControllers = new List<UIChestSlotViewController>();

            chestSlotDictionary = new Dictionary<int, UIChestSlotViewController>();

            for (int i = 0; i < totalChestSlots; i++)
            {
                GameObject gameObject = Instantiate(chestSlotViewControllerPrefab.gameObject);
                gameObject.transform.SetParent(uiChestSlotViewHolder.transform, false);

                UIChestSlotViewController slotController = gameObject.GetComponent<UIChestSlotViewController>();

                uIChestSlotViewControllers.Add(slotController);
            }

            generateChestButton.onClick.AddListener(GenerateChestButtonClicked);           
        }

        private void OnEnable()
        {
            EventService.Instance.OnChestAdded.AddListener(OnChestAdded);

            EventService.Instance.OnChestRemoved.AddListener(OnChestRemoved);
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

            EventService.Instance.OnUIPopupActivate.InvokeEvent(UIPopups.UI_CHEST_SLOTS_FULL_POPUP);
        }

        private void OnChestAdded(ChestController controller)
        {
            if (currentEmptyUIChestSlotViewControllerToBeFilled == null) return;

            if (!chestSlotDictionary.ContainsKey(controller.ChestID))
            {
                chestSlotDictionary[controller.ChestID] = currentEmptyUIChestSlotViewControllerToBeFilled;
                currentEmptyUIChestSlotViewControllerToBeFilled.OnChestAdded(controller);

                //Debug.Log("Add " + controller.ChestID);
            }

            
        }

        private void OnChestRemoved(ChestController controller)
        {
            //Debug.Log("Chest Removed");
            //Debug.Log(controller.ChestID);

            //if (currentEmptyUIChestSlotViewControllerToBeFilled != null) return;

            if(chestSlotDictionary.ContainsKey(controller.ChestID))
            {
                UIChestSlotViewController controllerToBeRemoved = chestSlotDictionary[controller.ChestID];

                controllerToBeRemoved.OnChestRemoved();

                chestSlotDictionary.Remove(controller.ChestID);

                
            }

            //Debug.Log("Removal " + controller.ChestID);
        }
    }
}