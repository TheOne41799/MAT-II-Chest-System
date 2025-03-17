using ChestSystem.Chests;
using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIGameplayViewController : MonoBehaviour
    {
        [Header("Player Coins and Gems")]
        [SerializeField] private TextMeshProUGUI playerCoinsText;
        [SerializeField] private TextMeshProUGUI playerGemsText;


        private void OnEnable()
        {
            EventService.Instance.OnPlayerStatsUpdated.AddListener(UpdatePlayerDetails);
        }

        private void UpdatePlayerDetails(int playerCoins, int playerGems)
        {
            playerCoinsText.text = playerCoins.ToString();
            playerGemsText.text = playerGems.ToString();
        }
    }
}