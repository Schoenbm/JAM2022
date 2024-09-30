using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketShadow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gm;
    float maxRocketHealth;
    private Image shadow;
    void Start()
    {
        maxRocketHealth = gm.maxHealthRocket;
        shadow = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        shadow.fillAmount = 1 - gm.healthRocket / maxRocketHealth;
    }
}
