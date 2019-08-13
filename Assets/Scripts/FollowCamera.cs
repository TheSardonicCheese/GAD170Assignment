using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float followSpeed;
    public Transform followLocation;



    void Update()
    {
        //make speed slower, not so fast (number between 0 and 1)
        float t = followSpeed * Time.deltaTime;
        //move to this position by whatever our speed is (0.16 of the way)
        transform.position = Vector3.Lerp(transform.position, followLocation.position, t);
    }
}