using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public SpriteRenderer spr;
    public float lifespan;
    float currentLife;
    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        currentLife = lifespan;
    }

    private void Update()
    {
        currentLife -= Time.deltaTime;
        if(currentLife < 0.3f)
        {
            Destroy(this.gameObject);
        }
        spr.color = new Color(1,1,1,currentLife / lifespan);
    }

}
