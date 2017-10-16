using UnityEngine;

public class LevelExit : MonoBehaviour
{

    private bool playerEntersExit;
    public bool PlayerEntersExit
    {
        get
        {
            return playerEntersExit;
        }
    }

    private bool isOpen;
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
    }

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (isOpen)
            {
                playerEntersExit = true;
                AudioManager.Instance.PlaySfx(2);
            }
    }

    public void OpenExit()
    {
        isOpen = true;
        anim.SetBool("isOpen", isOpen);
    }
}
