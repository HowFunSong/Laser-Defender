using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] TMP_InputField inputField;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        inputField = FindObjectOfType<TMP_InputField>();
    }
    void Start()
    {
        if (inputField != null)
        {
            
            EventTrigger trigger = inputField.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = inputField.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Deselect
            };

            entry.callback.AddListener((eventData) => { OnInputFieldDeselect((BaseEventData)eventData); });
            trigger.triggers.Add(entry);
        }
    }

    private void OnInputFieldDeselect(BaseEventData eventData)
    {
        scoreKeeper.SetPlayerName();
    }
}
