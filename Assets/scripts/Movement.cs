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
    bool isGrounded = false;
    bool jump = false;
    bool faceRight;
    public int maxExtraJump = 0;
    int extraJump = 1;
    public bool isAlive;
    public GameObject cameraTracker;
    public PlayerManager Manager;
    float coyoteTime;
    public float maxCoyoteTime;
    float jumpPressTime;
    bool stopJumping;
    bool inIceStation = false;
    bool inRocket = false;
    bool inRocketMenu = false;
    bool dropPlatform = false;
    Rocket rocket;
    GameObject platform;
    float deltaPlatformDrop = 0;


    private float largeurPersonnage;
    private float hauteurPersonnage;

    private float jumpGravity;
    private float fallGravity;
    private float fastFallGravity;

    private AudioManager sm;

    void Start() {
        groundMask = LayerMask.GetMask("Planet", "Platform");
        isAlive = true;
        faceRight = true;
        rb = this.GetComponent<Rigidbody2D>();
        extraJump = maxExtraJump;
        sm = FindObjectOfType<AudioManager>();
        jumpGravity = rb.gravityScale;
        fastFallGravity = rb.gravityScale * 2f;
        fallGravity = rb.gravityScale * 1.5f;

        largeurPersonnage = this.GetComponent<CapsuleCollider2D>().size.x * this.transform.localScale.x;
        hauteurPersonnage = this.GetComponent<CapsuleCollider2D>().size.y * this.transform.localScale.y ;
    }

    void Update(){
        rb.AddForce(Vector2.down * 0.1f);

        if (Input.GetAxis("Vertical") < 0)
        {
            dropPlatform = true;
        }
        else
            dropPlatform = false;
        if (Input.GetButtonDown("Jump") && !jump && isGrounded && !inRocketMenu){
            jump = true;
        }
        else if (Input.GetButtonDown("Jump") && rb.velocity.y < 15f && extraJump > 0 && !inRocketMenu)
        {
            Debug.Log("Double jump");
            jump = true;
            stopJumping = false;
        }
        if (Input.GetButtonUp("Jump") && jumpPressTime < 10)
        {
            stopJumping = true;
        }
        if (Input.GetKeyDown("e") && inIceStation)
        {
            Manager.playerSellIce();
        }
        if (Input.GetKeyDown("e") && inRocket && !inRocketMenu)
        {
            sm.Play("Open Shop");
            rocket.ActivateMenu(true);
            inRocketMenu = true;
        }
        else if ((Input.GetButtonDown("Cancel") ||Input.GetKeyDown("e")) && inRocket && inRocketMenu)
        {
            sm.Play("Close Shop");
            rocket.ActivateMenu(false);
            inRocketMenu = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Killable"){
            if(!Manager.invulnerable){
                isAlive = false;
                int r = Random.Range(1, 3);
                sm.Play("Damage" + r);
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

        if (collision.gameObject.tag == "Rocket")
        {
            rocket = collision.GetComponent<Rocket>();
            rocket.setActiveDialogue(true);
            inRocket = true;
        }
    }
        
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            rocket.ActivateMenu(false);
            rocket.setActiveDialogue(false);
            inRocket = false;
        }

        if (collision.gameObject.tag == "IceStation")
        {
            collision.gameObject.GetComponent<IceStation>().setActiveDialogue(false);
            inIceStation = false;
        }
    }

    IEnumerator turnPlatform(GameObject platform)
    {
        yield return new WaitForSeconds(0.25f);
        platform.GetComponent<PlatformEffector2D>().rotationalOffset = 0f;
    }

    void FixedUpdate(){

        //Si jamais c'est stuck
        if(transform.position.y < 31.4f)
        {
            this.rb.AddForce(Vector2.up);
        }

 
        processRunning();
        processGround();
        processJump();
        processExtraJump();
    }

    private void processGround()
    {
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position - new Vector3(largeurPersonnage * 0.5f, hauteurPersonnage * 0.52f, 0), Vector2.right, largeurPersonnage, groundMask);

        Debug.DrawRay(this.transform.position - new Vector3(largeurPersonnage * 0.5f ,hauteurPersonnage * 0.52f, 0), Vector2.right * largeurPersonnage, Color.green);
        Debug.Log(rb.velocity.y);
        if (!isGrounded &&  ray.collider && rb.velocity.y <= 0.001 )
        {


            rb.gravityScale = fallGravity;
            sm.Play("Grounded");
            extraJump = maxExtraJump;
            isGrounded = true;
            Debug.Log("Grounded");
            coyoteTime = maxCoyoteTime;
        }
        else if(!(ray.collider) && isGrounded)
        {
            Debug.Log("NotGrounded");
            isGrounded = false;
        }


        if (!(ray.collider))
        {
            coyoteTime -= Time.deltaTime;
        }

        if (isGrounded && dropPlatform)
        {
            if (ray.collider)
                platform = ray.collider.gameObject;
            else
                return;

            if ((platform.tag == "Platform" || platform.tag == "LongPlatform") && dropPlatform)
            {
                platform.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
                StartCoroutine(turnPlatform(platform));
            }
        }
    }


    private void processExtraJump()
    {

    }

    private void processRunning()
    {
        if(isAlive){
            Vector3 rotation = new Vector3(0,0,Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            if (isGrounded && Input.GetAxis("Horizontal") != 0 && !sm.isPlaying("Running"))
                sm.Play("Running");
            else if (sm.isPlaying("Running") && (!isGrounded ||  Input.GetAxis("Horizontal") ==0))
                sm.Stop("Running");

            if(faceRight && rotation.z < 0){
                faceRight = false;
                this.transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            if(!faceRight && rotation.z > 0){
                faceRight = true;
                this.transform.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            earth.transform.eulerAngles += rotation;
        }
    }

    private void processJump()
    {
        if (jump && isGrounded && coyoteTime > 0)
        {
            rb.velocity.Set(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            rb.gravityScale = jumpGravity;
            jump = false;
            jumpSound();
        }
        else if (!isGrounded && jump && extraJump > 0 )
        {
            rb.AddForce(-0.82f * rb.velocity, ForceMode2D.Impulse) ;
            rb.AddForce(Vector2.up * jumpHeight * 0.75f, ForceMode2D.Impulse);
            rb.gravityScale = jumpGravity;
            jump = false;
            extraJump -= 1;
            sm.Play("extraJump");
        }

        if (stopJumping)
        {
            rb.gravityScale = fastFallGravity;
            stopJumping = false;
        }
    }

    private void jumpSound()
    {
        int r = Random.Range(1, 4);
        sm.Play("Jump" + r);
    }
}   