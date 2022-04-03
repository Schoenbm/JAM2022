using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public Movement player;
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(Input.GetAxis("Horizontal") * 2, 0, 0);
    }
}
