using ChestSystem.UI;
using ChestSystem.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestPool
    {
        public List<PooledChestController> pooledChestControllers = new List<PooledChestController>();

        private ChestModelDatabaseSO chestModelDatabaseSO;


        public ChestPool(ChestModelDatabaseSO chestModelDatabaseSO)
        {
            this.chestModelDatabaseSO = chestModelDatabaseSO;
        }

        public ChestController GetChest()
        {
            if (pooledChestControllers.Count > 0)
            {
                PooledChestController pooledChestController = pooledChestControllers.Find(controller => !controller.IsUsed); ;

                if (pooledChestController != null)
                {
                    pooledChestController.IsUsed = true;
                    return pooledChestController.ChestController;
                }

                
            }

            return CreateNewRandomChest();
        }

        private ChestController CreateNewRandomChest()
        {
            PooledChestController newPooledChestController = new PooledChestController();
            newPooledChestController.ChestController = CreateChest();
            newPooledChestController.IsUsed = true;

            pooledChestControllers.Add(newPooledChestController);

            return newPooledChestController.ChestController;
        }

        private ChestController CreateChest()
        {
            ChestModelSO chestModelSO = ChooseARandomChestModel();
            ChestController chestController = new ChestController(chestModelSO);

            return chestController;
        }

        private ChestModelSO ChooseARandomChestModel()
        {
            int rand = Random.Range(0, chestModelDatabaseSO.ChestModelSOsList.Count);
            return chestModelDatabaseSO.ChestModelSOsList[rand];
        }

        public void ReturnChest(ChestController controller)
        {
            PooledChestController pooledChestController = pooledChestControllers.Find(i => i.ChestController.Equals(controller));
            pooledChestController.IsUsed = false;
        }
    }
}