using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsSafeguard : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision){
        Debug.Log("Out of bounds");
        collision.transform.position = Vector3.up;
        if(collision.rigidbody)
            collision.rigidbody.velocity = Vector3.zero;
        ScoreLogger.AddMistake(collision.transform.name+" fell out of bounds.");
    }
}
