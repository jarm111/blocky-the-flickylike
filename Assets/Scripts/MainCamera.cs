using UnityEngine;

public class MainCamera : MonoBehaviour {

    public float MinX = -0.5f;
    public float MaxX = 0.5f;
    public float MinY = 4.5f;
    public float MaxY = 7.5f;

    public GameObject player;

    private void LateUpdate()
    {
        transform.position = ClampPosition(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3
            (
            Mathf.Clamp(position.x, MinX, MaxX), 
            Mathf.Clamp(position.y, MinY, MaxY), 
            position.z
            );
    }
}
