using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject planet;
    public RigidBody2D rbPlayer;
    public jumpStrengh;
    public runSpeed;

    void Start()
    {
        rbPlayer = this.gameObject.get
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.getButtonDown("Jump"){
            rbPlayer.addForce(Vector2.up, jumpStrengh);
        }
        if (Input.getAxis("Horizontal"))
        {

        }
    }
}
