using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject earth;

    float jumpHeight = 1500f;

    public float speed = 10f;

    Rigidbody2D rb;
    LayerMask groundMask;
    bool isGrounded = true;
    bool jump = false;
    public bool isAlive;

    public PlayerManager Manager;

    void Start(){
        groundMask = LayerMask.GetMask("Planet", "Platform");
        isAlive = true;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update(){
        rb.AddForce(Vector2.down * 0.1f);
        if(Input.GetButtonDown("Jump") && !jump && isGrounded){
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
            Manager.playerPickIce();
        }
        if(collision.gameObject.tag == "Scrap")
        {
            Destroy(collision.gameObject);
            Manager.playerPickScrap();
        }
    }

    void FixedUpdate(){
        if(isAlive){
            Vector3 rotation = new Vector3(0,0,Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            earth.transform.eulerAngles += rotation;
        }
        
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position, Vector2.down,1.5f, groundMask);
        if (ray.collider)
        {
            Debug.Log("grounded");
            Debug.Log(ray.collider.transform.gameObject.tag);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            Debug.Log("not grounded");
        }

        Debug.DrawRay(this.transform.position, Vector2.down, Color.red, 1.2f);

        if(jump && isGrounded){
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
        }
    }
}   