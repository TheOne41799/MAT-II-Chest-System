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
        }

        private void OnEnable()
        {
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
    }
}