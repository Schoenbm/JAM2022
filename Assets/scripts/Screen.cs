using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Screen : MonoBehaviour
{
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Effect;
    public TextMeshProUGUI Level;
    //public playerData player;

    public GameObject SpeedButton;
    public GameObject HigherButton;
    public GameObject ExtraJumpButton;
    public GameObject RepairButton;
    public GameObject UpgradeIceButton;

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == SpeedButton) {
            Cost.SetText("Cost :\n 1");
            Effect.SetText("Effect :\n Run faster");
            Level.SetText("Level : 1");
        }
        if (EventSystem.current.currentSelectedGameObject == HigherButton)
        {
            Cost.SetText("Cost :\n 1");
            Effect.SetText("Effect :\n Jump Higher");
            Level.SetText("Level : 1");
        }
        if (EventSystem.current.currentSelectedGameObject == ExtraJumpButton)
        {
            Cost.SetText("Cost :\n 1");
            Effect.SetText("Effect :\n New jump");
            Level.SetText("Level : 1");
        }
        if (EventSystem.current.currentSelectedGameObject == RepairButton)
        {
            Cost.SetText("Cost :\n all");
            Effect.SetText("Effect :\n Repair ship");
            Level.SetText("");
        }
        if (EventSystem.current.currentSelectedGameObject == UpgradeIceButton)
        {
            Cost.SetText("Cost :\n 1");
            Effect.SetText("Effect :\n Gather more\n ice");
            Level.SetText("Level : 1");
        }
    }
}
