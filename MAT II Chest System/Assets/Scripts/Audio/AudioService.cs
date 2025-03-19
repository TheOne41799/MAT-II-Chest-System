using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Events;

namespace ChestSystem.Audio
{
    public class AudioService
    {
        public AudioController audioController { get; private set; }

        public AudioService(AudioView audioViewPrefab, AudioDatabaseSO database)
        {
            audioController = new AudioController(audioViewPrefab, database);

            EventService.Instance.OnBackgroundMusicPlay.AddListener(audioController.PlaybackgroundMusic);
            EventService.Instance.OnAudioEffectPlay.AddListener(audioController.PlaySoundEffects);
        }
    }
}