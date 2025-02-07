using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private ChestModelDatabaseSO chestModelDatabaseSO;
        private List<ChestController> chestControllers;
        private ChestView chestView;

        public ChestService(ChestModelDatabaseSO chestModelDatabaseSO, ChestView chestViewPrefab)
        {            
            this.chestModelDatabaseSO = chestModelDatabaseSO;
            this.chestView = chestViewPrefab;

            //PrintTest();
            CreateChest();
        }

        public void CreateChest()
        {
            ChestModelSO chestModelSO = ChooseARandomChestModel();
            ChestController chestController = new ChestController(chestModelSO, chestView);

            chestControllers.Add(chestController);
        }

        private ChestModelSO ChooseARandomChestModel()
        {
            int rand = Random.Range(0, chestModelDatabaseSO.ChestModelSOsList.Count);
            return chestModelDatabaseSO.ChestModelSOsList[rand];
        }

        private void PrintTest()
        {
            for (int i = 0; i < chestModelDatabaseSO.ChestModelSOsList.Count; i++)
            {
                Debug.Log(chestModelDatabaseSO.ChestModelSOsList[i].ChestType);
            }
        }
    }
}