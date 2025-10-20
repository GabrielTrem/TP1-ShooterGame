using UnityEngine;

public class AlienCollectibleDrop : MonoBehaviour
{
    [SerializeField] private CollectibleManager collectibleManager;
    private void OnDisable()
    {
        collectibleManager.DropCollectible(transform.position);
    }
}
