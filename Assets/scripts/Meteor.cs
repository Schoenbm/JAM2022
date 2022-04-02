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
            float angle = Mathf.Atan2(this.transform.position.normalized.y , this.transform.position.normalized.x);
            GameObject lava = Instantiate(lavaPrefab, this.transform.position - new Vector3(0,1,0), Quaternion.identity, this.gameObject.transform.parent.parent);;
            Debug.Log(angle);
            lava.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90f);
            Destroy(this.gameObject);
        }
    }
    
    void DestroyMeteor(){

    }
}
