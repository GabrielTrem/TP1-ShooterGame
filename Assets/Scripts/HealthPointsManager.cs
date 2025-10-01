using UnityEngine;

public class HealthPointsManager : MonoBehaviour
{
    [SerializeField] private int healthPoints = 5;
    private int currentHealthPoints;
    void Awake()
    {
        currentHealthPoints = healthPoints;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Alien")
        {
            LoseHealthPoint();
        }
    }

    public void LoseHealthPoint()
    {
        currentHealthPoints--;
    }
}
