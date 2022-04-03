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

    public int getCost(string category)
    {
        switch (category)
        {
            case "speed":
                return costSpeed[levelSpeed];
            case "jump":
                return costJump[levelJump];
            case "extraJump":
                return extraJump[levelExtraJump];
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
                playerMovement.maxExtraJump = ice[levelIce];
                break;
            default: 
                Debug.Log("Wrong String");
                break;
        }
    }
}
