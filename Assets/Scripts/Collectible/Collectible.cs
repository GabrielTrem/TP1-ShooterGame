using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum collectibleType {HEALING, MISSILE, TRIPLE_SHOT};
    [SerializeField] collectibleType type;
    [SerializeField] float lifeTime = 15;
    private float timeLeftBeforeDespawn;
    void Awake()
    {
        timeLeftBeforeDespawn = lifeTime;
    }

    void OnEnable()
    {
        timeLeftBeforeDespawn = lifeTime;   
    }

    void Update()
    {
        timeLeftBeforeDespawn -= Time.deltaTime;
        gameObject.transform.Rotate(Vector3.right, Time.deltaTime);
        if(timeLeftBeforeDespawn <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public collectibleType GetCollectibleType()
    {
        return type;
    }
}
