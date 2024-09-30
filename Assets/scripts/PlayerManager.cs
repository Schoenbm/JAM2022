using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public CinemachineVirtualCamera cineCamera;
    public int iceCount;
    int metalScrapCount;
    public GameObject playerPrefab;
    public GameObject planet;
    Vector3 position;
    public GameObject player;
    public Planet myPlanet;
    public GameManager gameManager;
    PlayerData playerData;
    public bool invulnerable;

    public Canvas HUD;
    public TextMeshProUGUI iceCounter;
    public int iceValue;
    public int startMetalScrap;
    public TextMeshProUGUI scrapCounter;

    public AudioManager sm;
    void Awake()
    {
        playerData = this.gameObject.GetComponent<PlayerData>();
        invulnerable = false;
        iceValue = 100;
        player = Instantiate(playerPrefab, new Vector3(0,31,0), Quaternion.identity) as GameObject;
        player.GetComponent<Movement>().earth = planet;
        player.GetComponent<Movement>().Manager = this;
        cineCamera.m_Follow =player.GetComponent<Movement>().cameraTracker.transform;
        Planet myPlanet = planet.GetComponent<Planet>();        
        iceCount = 0;
        metalScrapCount = startMetalScrap;
        iceCounter = HUD.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        scrapCounter = HUD.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        sm = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        iceCounter.SetText(metalScrapCount.ToString());
        scrapCounter.SetText(iceCount.ToString());
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(invulnerable && player){
            player.GetComponent<SpriteRenderer>().color = new Color(0.4f,0.4f,1,0.6f);
        }
        else if(player){
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void playerDeath(){
        iceCount = 0;
        metalScrapCount = 0;
        StartCoroutine(RespawnPlayer());
        
    }
   
    public void repairShip()
    {
        if(metalScrapCount > 0)
            gameManager.healRocket(metalScrapCount);
        metalScrapCount = 0;
    }

    public void playerPickIce(){
        sm.Play("Pickup Ice");
        iceCount += 1*iceValue;
    }

    public void playerPickScrap(){
        sm.Play("Pickup Scrap");
        metalScrapCount += 1;
    }

    public void playerSellIce() {
        sm.Play("Planet Cooling");
        myPlanet.plusHealth(iceCount * 8);
        iceCount = 0;
    }
    
    public int getMetalScrap()
    {
        return metalScrapCount;
    }

    public void setMetalScrap(int newCount)
    {
        metalScrapCount = newCount;
    }

    IEnumerator RespawnPlayer(){
        invulnerable = true;
        Debug.Log("Respawn player");
        position = ClosestAvailablePosition(transform.position);
        Destroy(player);
        yield return new WaitForSeconds(0.5f);
        player = Instantiate(playerPrefab, position, Quaternion.identity) as GameObject;
        player.GetComponent<Movement>().earth = planet;
        player.GetComponent<Movement>().Manager = this;
        player.GetComponent<Movement>().speed = playerData.getValue("speed");
        player.GetComponent<Movement>().jumpHeight = playerData.getValue("jump");
        player.GetComponent<Movement>().maxExtraJump = playerData.getValue("extraJump");
        cineCamera.m_Follow =player.GetComponent<Movement>().cameraTracker.transform;
        yield return new WaitForSeconds(3f);
        invulnerable = false;
        yield return null;
    }

    Vector3 ClosestAvailablePosition(Vector3 position){
        Vector3 finalPos;
        finalPos = position + new Vector3(Random.Range(-1,1),32,0);
        return finalPos; 
    }

    void EndGame(){
        sm.Play("Planet Explode");
        gameManager.EndGame();
    }
}
