using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerService
    {
        public PlayerController playerController {  get; set; }

        public PlayerService() 
        { 
            playerController = new PlayerController();
        }
    }
}