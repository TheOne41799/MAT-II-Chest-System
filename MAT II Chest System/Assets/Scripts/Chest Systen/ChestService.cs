using ChestSystem.Events;
using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private ChestModelDatabaseSO chestModelDatabaseSO;
        private List<ChestController> chestControllers = new List<ChestController>();
        private ChestView chestView;
        private Canvas canvas;
        private UIService uiService;

        public ChestService(ChestModelDatabaseSO chestModelDatabaseSO, ChestView chestViewPrefab, Canvas canvas, UIService uiService)
        {
            this.chestModelDatabaseSO = chestModelDatabaseSO;
            this.chestView = chestViewPrefab;
            this.canvas = canvas;
            this.uiService = uiService;

            //PrintTest();
            //CreateChest();

            //EventService.Instance.OnGenerateChestButtonClicked.AddListener(CreateChest);

            EventService.Instance.OnGenerateChestButtonClicked.AddListener(CreateChest);
        }

        /*public void CreateChest(int currentEmptySlot)
        {
            ChestModelSO chestModelSO = ChooseARandomChestModel();
            ChestController chestController = new ChestController(chestModelSO, chestView, uiService);

            chestControllers.Add(chestController);
        }*/

        public void CreateChest()
        {
            ChestModelSO chestModelSO = ChooseARandomChestModel();
            ChestController chestController = new ChestController(chestModelSO, chestView, uiService);

            chestControllers.Add(chestController);
        }

        private ChestModelSO ChooseARandomChestModel()
        {
            int rand = Random.Range(0, chestModelDatabaseSO.ChestModelSOsList.Count);
            return chestModelDatabaseSO.ChestModelSOsList[rand];
        }

        /*private void PrintTest()
        {
            for (int i = 0; i < chestModelDatabaseSO.ChestModelSOsList.Count; i++)
            {
                Debug.Log(chestModelDatabaseSO.ChestModelSOsList[i].ChestType);
            }
        }*/

        public void Update()
        {
            foreach (ChestController controller in chestControllers)
            {
                controller?.Update();
            }
        }
    }
}