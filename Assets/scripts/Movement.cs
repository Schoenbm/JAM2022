using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject earth;
    public float jumpHeight = 1500f;
    public float speed = 10f;
    Rigidbody2D rb;
    LayerMask groundMask;
    bool isGrounded = true;
    bool jump = false;
    public int maxExtraJump = 1;
    int extraJump = 1;
    public bool isAlive;
    public GameObject cameraTracker;
    public PlayerManager Manager;


    bool inIceStation = false;
    bool inRocket = false;

    void Start(){
        groundMask = LayerMask.GetMask("Planet", "Platform");
        isAlive = true;
        rb = this.GetComponent<Rigidbody2D>();
        extraJump = maxExtraJump;
    }

    void Update(){
        rb.AddForce(Vector2.down * 0.1f); 
        
        if (Input.GetButtonDown("Jump") && !jump && isGrounded){
            jump = true;
        }
        else if (Input.GetButtonDown("Jump") && rb.velocity.y < 15f && extraJump > 0)
        {
            Debug.Log("Double jump");
            jump = true;
        }
        if (Input.GetKeyDown("e") && inIceStation)
        {
            Manager.playerSellIce();
        }

    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Killable"){
            if(!Manager.invulnerable){
                isAlive = false;
                Manager.playerDeath();
            }
            
        }
        if(collision.gameObject.tag == "Ice")
        {
            collision.gameObject.GetComponent<Collectible>().platform.hasItem = false;
            Destroy(collision.gameObject);
            Manager.playerPickIce();
        }
        if(collision.gameObject.tag == "Scrap")
        {
            collision.gameObject.GetComponent<Collectible>().platform.hasItem = false;
            Destroy(collision.gameObject);
            Manager.playerPickScrap();
        }
        if (collision.gameObject.tag == "IceStation")
        {
            collision.gameObject.GetComponent<IceStation>().setActiveDialogue(true);
            inIceStation = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IceStation")
        {
            collision.gameObject.GetComponent<IceStation>().setActiveDialogue(false);
            inIceStation = false;
        }
    }

    void FixedUpdate(){
        if(isAlive){
            Vector3 rotation = new Vector3(0,0,Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            earth.transform.eulerAngles += rotation;
        }
        
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position - new Vector3(0,1,0), Vector2.down,0.5f, groundMask);
        if (ray.collider)
        {
            extraJump = maxExtraJump;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Debug.DrawRay(this.transform.position, Vector2.down, Color.red, 1.2f);

        if (jump && isGrounded && rb.velocity.y < 0.5f)
        {
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;

        }
        else if (jump && extraJump > 0 && rb.velocity.y < 12f)
        {
            rb.AddForce(-37 * rb.velocity.y * Vector2.up);
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
            extraJump -= 1;
        }

    }
}   