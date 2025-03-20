using UnityEngine;
using System.Collections.Generic;
using ChestSystem.Player;


namespace ChestSystem.Chests
{
    public class ChestPool
    {
        public List<PooledChestController> pooledChestControllers = new List<PooledChestController>();

        private ChestModelDatabaseSO chestModelDatabaseSO;

        private PlayerService playerService;

        private int initialChestsPerType = 3;

        public ChestPool(ChestModelDatabaseSO chestModelDatabaseSO, PlayerService playerService)
        {
            this.chestModelDatabaseSO = chestModelDatabaseSO;
            this.playerService = playerService;

            InitializeChestPool();
        }

        private void InitializeChestPool()
        {
            for (int i = 0; i < initialChestsPerType; i++)
            {
                pooledChestControllers.Add(new PooledChestController { ChestController = CreateChestOfType(ChestType.COMMON), IsUsed = false });
                pooledChestControllers.Add(new PooledChestController { ChestController = CreateChestOfType(ChestType.RARE), IsUsed = false });
                pooledChestControllers.Add(new PooledChestController { ChestController = CreateChestOfType(ChestType.EPIC), IsUsed = false });
                pooledChestControllers.Add(new PooledChestController { ChestController = CreateChestOfType(ChestType.LEGENDARY), IsUsed = false });
            }
        }

        public ChestController GetChest()
        {
            if (pooledChestControllers.Count > 0)
            {
                List<PooledChestController> availableChests = pooledChestControllers.FindAll(controller => !controller.IsUsed);
                
                if (availableChests.Count > 0)
                {
                    int randIndex = Random.Range(0, availableChests.Count);
                    PooledChestController pooledChestController = availableChests[randIndex];

                    pooledChestController.IsUsed = true;
                    pooledChestController.ChestController.InitializeVariables();

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

        private ChestController CreateChestOfType(ChestType chestType)
        {
            ChestModelSO chestModelSO = chestModelDatabaseSO.ChestModelSOsList.Find(model => model.ChestType == chestType);
            ChestController chestController = new ChestController(chestModelSO, playerService);
            
            chestController.InitializeVariables();

            return chestController;
        }

        private ChestController CreateChest()
        {
            ChestModelSO chestModelSO = ChooseARandomChestModel();
            ChestController chestController = new ChestController(chestModelSO, playerService);

            chestController.InitializeVariables();

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