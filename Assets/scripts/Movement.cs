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

    public PlayerManager Manager;

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
        if (Input.GetButtonDown("Jump") && rb.velocity.y < 8f && !isGrounded && extraJump > 0)
        {
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

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "BaseHealth")
        {
            //WHY I CANT COME HERE ?????? :@
            Debug.Log("Player Sell Ice" );
            if (Input.GetButtonDown("E"))//E is not mapping in unity enter is probably a best choice
            {
                Manager.playerSellIce();
            } 
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
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Debug.DrawRay(this.transform.position, Vector2.down, Color.red, 1.2f);

        if (jump && !isGrounded && extraJump > 0 && rb.velocity.y < 8f)
        {
            rb.AddForce(-40 * rb.velocity.y * Vector2.up);
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
            extraJump -= 1;
        }

        if (jump && isGrounded && rb.velocity.y < 2f)
        {
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
            extraJump = maxExtraJump;
        }
    }
}   