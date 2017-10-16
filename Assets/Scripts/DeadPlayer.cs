using UnityEngine;

public class DeadPlayer : MonoBehaviour
{

    public float HopForce = 100f;
    public float HopTorque = 10f;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(0, HopForce));
        rb2d.AddTorque(HopTorque);
        Destroy(gameObject, 3f);
    }
}
