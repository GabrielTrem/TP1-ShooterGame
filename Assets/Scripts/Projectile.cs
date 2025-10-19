using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
}
