using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Platform : MonoBehaviour
{

    public bool isCorrect;  
    Vector2 size;  
    void Awake(){
        isCorrect = true;
        LayerMask mask = LayerMask.GetMask("Platform");
        Vector2 size = 2*transform.localScale;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, transform.rotation.z, mask);
        if(colliders.Length > 1){
            isCorrect = false;
            Debug.Log("collided");
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        //Gizmos.DrawCube(transform.position, 2*transform.localScale);

    }
}
