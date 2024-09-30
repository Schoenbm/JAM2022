using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed = 4f;
    public float rotSpeed = 20f;
    public GameObject lavaPrefab;
    ParticleSystem Trail;
    ParticleSystem Explosion;

    public GameManager gameManager;

    void Start(){
        speed = Random.Range(4f, 7f);
        rb = this.GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        Trail = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        Explosion = this.transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = -transform.position;
        direction = direction.normalized;
        Vector3 newPosition = transform.position + Time.deltaTime * speed * direction;
        transform.position = newPosition;

        ParticleSystem ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        var velocity = ps.velocityOverLifetime;
        velocity.x = -10 * direction.x;
        velocity.y = -10 * direction.y;

        Vector3 rotation = new Vector3(0, 0, rotSpeed * Time.deltaTime);
        transform.eulerAngles += rotation;
    }


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Planet"){
            FindObjectOfType<AudioManager>().Play("Meteor Crash");
            float angle = Mathf.Atan2(this.transform.position.normalized.y , this.transform.position.normalized.x);
            GameObject lava = Instantiate(lavaPrefab, this.transform.position, Quaternion.identity, this.gameObject.transform.parent.parent);;
            lava.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90f);
            lava.transform.position -= this.transform.position.normalized;
            StartCoroutine(DestroyMeteor());
        }
    }
    
    IEnumerator DestroyMeteor(){
        Explosion.Play();
        this.GetComponent<SpriteRenderer>().enabled = false;
        gameManager.CameraShake();
        Trail.Stop();
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
