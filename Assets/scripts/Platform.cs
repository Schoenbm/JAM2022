using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Platform : MonoBehaviour
{

    public bool isCorrect;
    public bool hasItem;
    Vector2 size;  
    void Awake(){
        isCorrect = true;
        hasItem = false;
        LayerMask mask = LayerMask.GetMask("Platform");
        Vector2 size = new Vector2(6,8);
        Matrix4x4 mat = this.transform.localToWorldMatrix;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(mat.GetPosition(), size, mat.rotation.z, mask);
        if(colliders.Length > 1){
            isCorrect = false;
            Debug.Log("collided");
        }
    }

    void OnDrawGizmos(){
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(2,4,1));

    }
}