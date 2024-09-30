using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ScreenUI : MonoBehaviour
{
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Effect;
    public TextMeshProUGUI Level;
    //public playerData player;

    public ButtonHighlited SpeedButton;
    public ButtonHighlited HigherButton;
    public ButtonHighlited ExtraJumpButton;
    public ButtonHighlited RepairButton;
    public ButtonHighlited UpgradeIceButton;

    public PlayerData data;
    PlayerManager playerManager;

    private void Awake()
    {
        playerManager = data.gameObject.GetComponent<PlayerManager>();

    }
    private void Update()
    {
        //PointerEventData lastPointer = PointerInputModule.GetLastPointerEventData() // GetLastPointerEventData(PointerInputModule.kMouseLeftId);
        //if (lastPointer != null)
        //  return lastPointer.pointerCurrentRaycast.gameObject;
        //return null;

        if (SpeedButton.GetHighlighted())// || EventSystem.current.currentSelectedGameObject == SpeedButton)
        {
            Cost.SetText("Cost :\n" + data.getCost("speed"));
            Effect.SetText("Effect :\n Run faster");
            Level.SetText("Level : " + data.getLevel("speed"));
        }
        else if (HigherButton.GetHighlighted())// || EventSystem.current.currentSelectedGameObject == HigherButton)
        {
            Cost.SetText("Cost :\n" + data.getCost("jump"));
            Effect.SetText("Effect :\n Jump Higher");
            Level.SetText("Level : " + data.getLevel("jump"));
        }
        else if (ExtraJumpButton.GetHighlighted())// || EventSystem.current.currentSelectedGameObject == ExtraJumpButton)
        {
            Cost.SetText("Cost :\n" + data.getCost("extraJump"));
            Effect.SetText("Effect :\n New jump");
            Level.SetText("Level : " + data.getLevel("extraJump"));
        }
        else if (RepairButton.GetHighlighted())// || EventSystem.current.currentSelectedGameObject == RepairButton)
        {
            Cost.SetText("Cost :\n all");
            Effect.SetText("Effect :\n Repair ship");
            Level.SetText("");
        }
        else if (UpgradeIceButton.GetHighlighted())// || EventSystem.current.currentSelectedGameObject == UpgradeIceButton)
        {
            Cost.SetText("Cost :\n" + data.getCost("ice"));
            Effect.SetText("Effect :\n Gather more\n ice");
            Level.SetText("Level :" + data.getLevel("ice"));
        }
    }

    public void buySpeed()
    {
        if(playerManager.getMetalScrap() >= data.getCost("speed"))
        {
            Debug.Log("Bough speed");
            playerManager.setMetalScrap(playerManager.getMetalScrap() - data.getCost("speed"));
            data.levelUp("speed");
            FindObjectOfType<AudioManager>().Play("Pickup Scrap");
        }
        else
            Debug.Log("Can't buy : " + playerManager.getMetalScrap() + " where cost " + data.getCost("speed"));
    }

    public void buyIce()
    {
        if (playerManager.getMetalScrap() >= data.getCost("ice"))
        {
            playerManager.setMetalScrap(playerManager.getMetalScrap() - data.getCost("ice"));
            data.levelUp("ice");
            FindObjectOfType<AudioManager>().Play("Pickup Scrap");
        }
    }

    public void buyJump()
    {
        if (playerManager.getMetalScrap() >= data.getCost("jump"))
        {
            playerManager.setMetalScrap(playerManager.getMetalScrap() - data.getCost("jump"));
            data.levelUp("jump");
            FindObjectOfType<AudioManager>().Play("Pickup Scrap");
        }
    }

    public void buyExtraJump()
    {
        if (playerManager.getMetalScrap() >= data.getCost("extraJump"))
        {
            playerManager.setMetalScrap(playerManager.getMetalScrap() - data.getCost("extraJump"));
            data.levelUp("extraJump");
            FindObjectOfType<AudioManager>().Play("Pickup Scrap");
        }
    }

    public void healRocket()
    {
        playerManager.repairShip();
    }
}
