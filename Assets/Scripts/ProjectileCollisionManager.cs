using System;
using UnityEngine;

public class ProjectileCollisionManager : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
