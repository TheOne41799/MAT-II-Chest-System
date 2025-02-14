using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerController
    {
        public PlayerModel playerModel {  get; set; }

        public PlayerController() 
        {
            playerModel = new PlayerModel();
        }
    }
}
