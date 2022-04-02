using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    Rigidbody2D rb;
    float speed = 1f;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = -transform.position;
        Vector3 newPosition = transform.position + Time.deltaTime * speed * direction.normalized;
        transform.position = newPosition;
    }


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){            
            Debug.Log("Hit player ! ");
            collision.gameObject.GetComponent<Movement>().isAlive = false;
        }
        if(collision.gameObject.tag == "Planet"){
            DestroyMeteor();
            Destroy(this.gameObject);
        }
    }
    
    void DestroyMeteor(){

    }
}
