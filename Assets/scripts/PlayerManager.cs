using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    int lifes = 3;
    public int iceValue = 10;

    public CinemachineVirtualCamera cineCamera;
    public int iceCount;
    int metalScrapCount;
    public GameObject playerPrefab;
    public GameObject planet;
    Vector3 position;
    GameObject player;
    public Planet myPlanet;
    public GameManager gameManager;

    public bool invulnerable;

    public Canvas HUD;
    public TextMeshProUGUI iceCounter;
    public TextMeshProUGUI scrapCounter;

    void Start()
    {
        invulnerable = false;
        player = Instantiate(playerPrefab, new Vector3(0,31,0), Quaternion.identity) as GameObject;
        player.GetComponent<Movement>().earth = planet;
        player.GetComponent<Movement>().Manager = this;
        cineCamera.m_Follow =player.GetComponent<Movement>().cameraTracker.transform;
        Planet myPlanet = planet.GetComponent<Planet>();        
        iceCount = 0;
        metalScrapCount = 0;
        iceCounter = HUD.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        scrapCounter = HUD.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(invulnerable && player){
            player.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(player){
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }

        iceCounter.SetText(metalScrapCount.ToString());
        scrapCounter.SetText(iceCount.ToString());
    }

    public void playerDeath(){
        iceCount = 0;
        metalScrapCount = 0;
        StartCoroutine(RespawnPlayer());
        
    }
   
    public void playerPickIce(){
        FindObjectOfType<AudioManager>().Play("Pickup Item");
        iceCount += 1*iceValue;
    }

    public void playerPickScrap(){
        FindObjectOfType<AudioManager>().Play("Pickup Item");
        metalScrapCount += 1;
    }

    public void playerSellIce() {
        myPlanet.plusHealth(iceCount);
        iceCount = 0;
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
        gameManager.EndGame();
    }
}
