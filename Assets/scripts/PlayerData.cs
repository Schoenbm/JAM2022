using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<int> speed;
    public List<int> costSpeed;
    int levelSpeed;

    public List<int> jump;
    public List<int> costJump;
    int levelJump;

    public List<int> extraJump;
    public List<int> costExtraJump;
    int levelExtraJump;

    public List<int> ice;
    public List<int> costIce;
    int levelIce;

    Movement playerMovement;
    PlayerManager playerManager;

    private void Start()
    {

        playerManager = this.gameObject.GetComponent<PlayerManager>();
        playerMovement = playerManager.player.GetComponent<Movement>();
        levelSpeed = 0;
        levelJump = 0;
        levelIce = 0;
        levelExtraJump = 0;
        playerManager.iceValue = ice[0];
        playerMovement.speed = speed[0];
        playerMovement.maxExtraJump = extraJump[0];
        playerMovement.jumpHeight = jump[0];
    }
    public int getCost(string category)
    {
        switch (category)
        {
            case "speed":
                return costSpeed[levelSpeed];
            case "jump":
                return costJump[levelJump];
            case "extraJump":
                return costExtraJump[levelExtraJump];
            case "ice":
                return costIce[levelIce];
            default: return -1;
        }
    }

    public void levelUp(string category)
    {
        switch (category)
        {
            case "speed":
                levelSpeed++;
                playerMovement.speed = speed[levelSpeed];
                break;
            case "jump":
                levelJump++;
                playerMovement.jumpHeight = jump[levelJump];
                break;
            case "extraJump":
                levelExtraJump++;
                playerMovement.maxExtraJump = extraJump[levelExtraJump];
                break;
            case "ice":
                levelIce++;
                playerManager.iceValue = ice[levelIce];
                break;
            default: 
                Debug.Log("Wrong String");
                break;
        }
    }

    public string getLevel(string category)
    {
        switch (category)
        {
            case "speed":
                if (levelSpeed == speed.Count -1)
                    return "max";
                return levelSpeed.ToString();
            case "jump":
                if (levelJump == jump.Count -1)
                    return "max";
                return levelJump.ToString();
            case "extraJump":
                if (levelExtraJump == extraJump.Count-1)
                    return "max";
                return levelExtraJump.ToString();
            case "ice":
                if (levelIce == ice.Count-1)
                    return "max";
                return levelIce.ToString();
            default:
                return "no level";
        }
    }


    public int getValue(string category)
    {
        switch (category)
        {
            case "speed":
                return speed[levelSpeed];
            case "jump":
                return jump[levelJump];
            case "extraJump":
                return extraJump[levelExtraJump];
            case "ice":
                return ice[levelIce];
            default:
                return -1;
        }
    }
}
