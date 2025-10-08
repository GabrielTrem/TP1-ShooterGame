using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum collectibleType {HEALTH, AMMO, SHOOTING_BOOST};
    [SerializeField] collectibleType type;
    [SerializeField] float lifetime = 15;
    private float timeLeftBeforeDespawn;
    void Awake()
    {
        timeLeftBeforeDespawn = lifetime;
    }

    void OnEnable()
    {
        timeLeftBeforeDespawn = lifetime;   
    }

    void Update()
    {
        timeLeftBeforeDespawn -= Time.deltaTime;
        gameObject.transform.Rotate(Vector3.up * 180f * Time.deltaTime);
        if (timeLeftBeforeDespawn <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public collectibleType GetCollectibleType()
    {
        return type;
    }
}
