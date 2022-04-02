using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    Rigidbody2D rb;
    float speed = 4f;
    public GameObject lavaPrefab;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
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
    }


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Planet"){
            DestroyMeteor();
            Instantiate(lavaPrefab, this.gameObject.transform.position, collision.gameObject.transform.rotation, collision.gameObject.transform) ;
            Destroy(this.gameObject);
        }
    }
    
    void DestroyMeteor(){

    }
}
