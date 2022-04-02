using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    private PlayerManager _playerManager;
    
    public Text iceNumberText;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerManager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        iceNumberText.text = "Ice: " + 0;
        if (_playerManager == null )
        {
            Debug.LogError(_playerManager.ice);
        }
    }

    // Update is called once per frame
    void Update()
    {
        iceNumberText.text = "Ice:" + _playerManager.ice;
    }
}
