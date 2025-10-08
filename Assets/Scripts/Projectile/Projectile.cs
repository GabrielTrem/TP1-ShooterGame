using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
}
