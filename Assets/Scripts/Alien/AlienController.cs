using UnityEngine;

public class AlienController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private int lives = 1;
    private Rigidbody rigidBody;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlienDie()
    {
        if(lives == 0)
        {
           Destroy(gameObject);
        }

        return;
    }
}
