using System.Collections;
using UnityEngine;

public class Follower : MonoBehaviour {

    private static float xPosition = 0.9f;
    private static bool facingRight = true;

    private float xOffset = 0.6f;
    private float yOffset = 0.5f;
    private float instanceXPosition;
    private float updatePositionTimeDelay = 0.1f;
    //private Vector3 playerPosition;
    //private Vector3 delayedPlayerPosition;

    private void Start () {
        instanceXPosition = xPosition;
        xPosition += xOffset;
    }

    private void OnDisable()
    {
        xPosition -= xOffset;
        facingRight = true;
    }

    private void Update () {
        //updatePosition(getPlayerPosition());
        //Invoke("getDelayedPlayerPosition", 0.5f);
        //Invoke("updatePosition", 0.5f);
        StartCoroutine(UpdatePositionAfterTime(updatePositionTimeDelay));
    }

    public static void FlipFacing()
    {
        facingRight = !facingRight;
    }

    private Vector3 GetPlayerPosition()
    {
        return Player.Instance.transform.position;
    }

    private void UpdatePosition(Vector3 position)
    {
        gameObject.transform.position = (facingRight) ? 
            position + new Vector3(-instanceXPosition, yOffset, 0f) 
            : 
            position + new Vector3(instanceXPosition, yOffset, 0f);
    }

    IEnumerator UpdatePositionAfterTime(float time)
    {
        Vector3 pos = GetPlayerPosition();
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        UpdatePosition(pos);
    }
}
