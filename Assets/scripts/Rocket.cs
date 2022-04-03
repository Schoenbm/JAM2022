using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    public GameObject dialogue;

    // Start is called before the first frame   update
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
        if(b)
            Time.timeScale = 0.025f;
        else
            Time.timeScale = 1f;
        dialogue.SetActive(b);
    }
    
    
}
