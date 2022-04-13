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

    void Start(){
        groundMask = LayerMask.GetMask("Planet", "Platform");
        isAlive = true;
        faceRight = true;
        rb = this.GetComponent<Rigidbody2D>();
        extraJump = maxExtraJump;
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
            FindObjectOfType<AudioManager>().Play("Open Shop");
            rocket.ActivateMenu(true);
            inRocketMenu = true;
        }
        else if ((Input.GetButtonDown("Cancel") ||Input.GetKeyDown("e")) && inRocket && inRocketMenu)
        {
            FindObjectOfType<AudioManager>().Play("Close Shop");
            rocket.ActivateMenu(false);
            inRocketMenu = false;
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

        if(transform.position.y < 31.4f)
        {
            this.rb.AddForce(Vector2.up);
        }
        if(isAlive){
            Vector3 rotation = new Vector3(0,0,Input.GetAxis("Horizontal") * speed * Time.deltaTime);
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
        
        RaycastHit2D rayLeft = Physics2D.Raycast(this.transform.position - new Vector3(-0.35f,1,0), Vector2.down,0.5f, groundMask);
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position - new Vector3(0, 0.7f, 0), Vector2.down, 0.7f, groundMask);
        RaycastHit2D rayRight = Physics2D.Raycast(this.transform.position - new Vector3(0.35f, 1, 0), Vector2.down, 0.5f, groundMask);
        if (rayLeft.collider || rayRight.collider || ray.collider)
        {
            extraJump = maxExtraJump;
            isGrounded = true;
            coyoteTime = maxCoyoteTime;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
            isGrounded = (coyoteTime > 0);
        }
        Debug.DrawRay(this.transform.position - new Vector3(-0.35f, 1, 0), Vector2.down, Color.red, 0.5f);
        Debug.DrawRay(this.transform.position - new Vector3(0.35f, 1, 0), Vector2.down, Color.red, 0.5f);


        if (isGrounded && dropPlatform)
        {
            if (rayLeft.collider)
                platform = rayLeft.collider.gameObject;
            else if (rayLeft.collider)
                platform = rayLeft.collider.gameObject;
            else if (rayLeft.collider)
                platform = rayLeft.collider.gameObject;

            if ((platform.tag == "Platform" || platform.tag == "LongPlatform") && dropPlatform)
            {
                platform.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
                StartCoroutine(turnPlatform(platform));
            }
        }

        if (jump && isGrounded && rb.velocity.y < 0.5f)
        {
            rb.AddForce(-20 * rb.velocity.y * Vector2.up);
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
            jumpPressTime = 0f;
        }
        else if (jump && extraJump > 0 && rb.velocity.y < 12f)
        {
            rb.AddForce(-37 * rb.velocity.y * Vector2.up);
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
            extraJump -= 1;
        }
        if(jumpPressTime < 10)
        {
            jumpPressTime += Time.deltaTime;
        }
        
        if (jumpPressTime < 0.15f && stopJumping)
        {
            Debug.Log("short");
            jumpPressTime = 10f;
            rb.AddForce(Vector2.down * jumpHeight * 0.38f);
            stopJumping = false;
        }
        else if(stopJumping)
        {
            stopJumping = false;
            jumpPressTime = 10f;
        }

    }
}   