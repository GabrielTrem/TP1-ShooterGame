using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool isMissile;
    
    void OnCollisionEnter(Collision collision)
    {
        if (isMissile)
        {
            gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        if (!isMissile) 
        { 
            gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
