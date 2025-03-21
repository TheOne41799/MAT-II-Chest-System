using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/ New Audio Database")]
    public class AudioDatabaseSO : ScriptableObject
    {
        public List<AudioSO> audioClips;
    }
}