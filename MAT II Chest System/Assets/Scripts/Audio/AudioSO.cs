using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Audio
{
    [CreateAssetMenu(fileName = "AudioClip", menuName = "Audio/ New Audio SO")]
    public class AudioSO : ScriptableObject
    {
        public AudioTypes audioType;
        public AudioClip audioClip;
    }
}