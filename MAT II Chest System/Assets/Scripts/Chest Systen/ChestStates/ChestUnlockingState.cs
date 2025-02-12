using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ChestSystem.Events;

namespace ChestSystem.Chests
{
    public class ChestUnlockingState<T> : IChestState where T : ChestController
    {
        public ChestController chestController { get; set; }
        private GenericStateMachine<T> stateMachine;
        private float unlockTimeRemaining;

        public ChestUnlockingState(GenericStateMachine<T> stateMachine, float unlockTime)
        {
            this.stateMachine = stateMachine;
            this.unlockTimeRemaining = unlockTime;
        }

        public void EnterState()
        {
            chestController.chestView.ChestUnlockingStateUI();            
        }

        public void ExitState()
        {
            
        }

        public void UpdateState()
        {
            if (unlockTimeRemaining >= 0)
            {
                unlockTimeRemaining -= Time.deltaTime;                
            }
            else
            {
                unlockTimeRemaining = 0;

                // Where to call this?
                //EventService.Instance.OnChestUnlocked.InvokeEvent(chestController);
            }

            DisplayTime(unlockTimeRemaining);
        }

        private void DisplayTime(float timeValue)
        {
            /*if (timeValue <= 0)
            {
                timeValue = 0;                
            }*/

            timeValue = Mathf.FloorToInt(timeValue);
            //timeValue = Mathf.Ceil(timeValue * 10) / 10f;

            chestController.chestView.UpdateTimer(timeValue);            
        }
    }
}
