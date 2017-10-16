using UnityEngine;

public class Collectible : MonoBehaviour
{

    private static int countOfCollectibles;
    public static int CountOfCollectibles
    {
        get
        {
            return countOfCollectibles;
        }
    }

    private void OnEnable()
    {
        countOfCollectibles++;
    }

    private void OnDisable()
    {
        countOfCollectibles--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
