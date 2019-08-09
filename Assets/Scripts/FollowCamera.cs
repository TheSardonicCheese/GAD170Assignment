using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float followSpeed;
    public Transform followLocation;



    void Update()
    {
        float t = followSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, followLocation.position, t);
    }
}