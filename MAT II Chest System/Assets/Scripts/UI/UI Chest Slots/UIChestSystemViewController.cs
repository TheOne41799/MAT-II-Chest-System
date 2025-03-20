using ChestSystem.Chests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Events;
using ChestSystem.Audio;

namespace ChestSystem.UI
{
    public class UIChestSystemViewController : MonoBehaviour
    {
        // this script controlls all the chest slots
        // essentially an UI part which tracks all the chests
        // doesnot perform any control logic at the moment but you do it if needed
        [SerializeField] private int totalChestSlots;

        [SerializeField] private Button generateChestButton;

        [SerializeField] private UIChestSlotViewController chestSlotViewControllerPrefab;

        //game object under which all the chest controllers will be added as childrem
        [SerializeField] private GameObject uiChestSlotViewHolder;

        private List<UIChestSlotViewController> uIChestSlotViewControllers;

        //tracking what is the first available empty slot in the scene
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

                    EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.BUTTON_CLICKED, false);
                                        
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
            }

            
        }

        private void OnChestRemoved(ChestController controller)
        {
            if(chestSlotDictionary.ContainsKey(controller.ChestID))
            {
                UIChestSlotViewController controllerToBeRemoved = chestSlotDictionary[controller.ChestID];

                controllerToBeRemoved.OnChestRemoved();

                chestSlotDictionary.Remove(controller.ChestID);
            }
        }
    }
}