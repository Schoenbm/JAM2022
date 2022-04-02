using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject earth;

    float jumpHeight = 1500f;

    public float speed = 10f;

    Rigidbody2D rb;

    bool jump = false;
    public bool isAlive;

    public PlayerManager Manager;

    void Start(){
        isAlive = true;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(Input.GetButtonDown("Jump") && !jump){
            jump = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Killable"){
            isAlive = false;
            Manager.playerDeath();
        }
        if(collision.gameObject.tag == "Ice")
        {
            Destroy(collision.gameObject);
            Manager.playerPick();
        } 
    }

    void FixedUpdate(){
        if(isAlive){
            Vector3 rotation = new Vector3(0,0,Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            earth.transform.eulerAngles += rotation;
        }
        

        if(jump){
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
        }
    }
}