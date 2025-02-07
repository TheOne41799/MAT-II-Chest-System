using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UIService
    {
        private UIChestSystemViewController uiChestSystemViewController;
        private UIGameplayViewController uiGameplayViewController;

        public UIService(UIChestSystemViewController uiChestSystemViewControllerPrefab, UIGameplayViewController uiGameplayViewControllerPrefab)
        {
            uiChestSystemViewController = GameObject.Instantiate(uiChestSystemViewControllerPrefab);
            uiGameplayViewController = GameObject.Instantiate(uiGameplayViewControllerPrefab);
        }
    }
}
