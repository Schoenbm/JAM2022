using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationRocketMenu : MonoBehaviour
{
    private GameManager _gameManager;

    public Image shadow;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        shadow.fillAmount= (100-(float)_gameManager.healthRocket) / (float)_gameManager.maxHealthRocket ;
    }
}
