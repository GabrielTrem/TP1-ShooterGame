using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 offest = new Vector3(0f, 5f, -7f);

    void LateUpdate()
    {
        transform.position = player.transform.position + offest;
    }
}
