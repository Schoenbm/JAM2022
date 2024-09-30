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

    private AudioManager sm;

    void Start(){
        groundMask = LayerMask.GetMask("Planet", "Platform");
        isAlive = true;
        faceRight = true;
        rb = this.GetComponent<Rigidbody2D>();
        extraJump = maxExtraJump;
        sm = FindObjectOfType<AudioManager>();
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
        RaycastHit2D rayLeft = Physics2D.Raycast(this.transform.position - new Vector3(-0.35f, 1, 0), Vector2.down, 0.5f, groundMask);
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position - new Vector3(0, 0.7f, 0), Vector2.down, 0.7f, groundMask);
        RaycastHit2D rayRight = Physics2D.Raycast(this.transform.position - new Vector3(0.35f, 1, 0), Vector2.down, 0.5f, groundMask);
        if (!isGrounded && (rayLeft.collider || rayRight.collider || ray.collider))
        {
            sm.Play("Grounded");
            extraJump = maxExtraJump;
            isGrounded = true;
            coyoteTime = maxCoyoteTime;
        }
        else
            isGrounded = false;

        if (!(rayLeft.collider || rayRight.collider || ray.collider))
        {
            coyoteTime -= Time.deltaTime;
        }

        if (isGrounded && dropPlatform)
        {
            if (rayLeft.collider)
                platform = rayLeft.collider.gameObject;
            else if (rayRight.collider)
                platform = rayRight.collider.gameObject;
            else if (ray.collider)
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
        if (jump && isGrounded)
        {
            rb.velocity.Set(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
            jumpSound();
        }
        else if (!isGrounded && jump && extraJump > 0 )
        {
            rb.AddForce(Vector2.up * jumpHeight * 0.8f);
            jump = false;
            extraJump -= 1;
            sm.Play("extraJump");
        }

        if (stopJumping)
        {
            stopJumping = false;
            rb.AddForce(-Vector2.up * 4 * Physics2D.gravity.y);
        }
    }

    private void jumpSound()
    {
        int r = Random.Range(1, 4);
        sm.Play("Jump" + r);
    }
}   