using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    Rigidbody2D rb;
    float speed = 100f;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = transform.position - Vector3.zero;
        Vector3 newPosition = transform.position + Time.deltaTime * speed * direction.normalized;
        transform.position = newPosition;

        //rb.velocity = new Vector2(0,-speed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){            
            Debug.Log("Hit player ! ");
        }
    }
}
