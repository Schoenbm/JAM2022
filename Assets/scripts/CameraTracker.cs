using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public bool active = true;
    public Movement player;
    Rigidbody2D rb;
    private void Start()
    {
        rb = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;

        float coef = player.gameObject.transform.transform.position.y - 29;
        if(rb.velocity.y >= 0)
            transform.localPosition = new Vector3(Input.GetAxis("Horizontal") * 2, rb.velocity.y/ 5 - coef/9, 0);
        else
            transform.localPosition = new Vector3(Input.GetAxis("Horizontal") * 2, rb.velocity.y / 5f - coef/9, 0);
    }
}
