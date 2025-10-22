using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool isMissile;
    
    void OnCollisionEnter(Collision collision)
    {
        Console.WriteLine("COLLIDED");
        if (!isMissile)
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
