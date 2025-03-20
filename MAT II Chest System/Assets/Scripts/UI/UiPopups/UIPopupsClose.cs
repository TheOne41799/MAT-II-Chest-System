using ChestSystem.Audio;
using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupsClose : MonoBehaviour
{
    // a separate button prefab for some of the UI popup prefab
    [SerializeField] private Button uiPopupsCloseButton;

    private void OnEnable()
    {
        uiPopupsCloseButton.onClick.AddListener(CloseAllUIPopups);
    }

    private void OnDisable()
    {
        uiPopupsCloseButton.onClick.RemoveAllListeners();
    }

    private void CloseAllUIPopups()
    {
        EventService.Instance.OnCloseUIPopups.InvokeEvent();

        EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.BUTTON_CLICKED, false);
    }
}
