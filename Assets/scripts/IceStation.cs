using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceStation : MonoBehaviour
{
    public Planet _planet;
    public GameManager gameManager;
    public GameObject dialogue;


    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(false);
    }

    void Update()
    {
    }
    // Update is called once per frame
    public void setActiveDialogue(Boolean b)
    {
        dialogue.SetActive(b);
    }
    
    
}
