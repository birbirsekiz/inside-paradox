using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] GameObject checkPointSaver = null;
    [SerializeField] CloneControl cloneControl = null;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void KillPlayer()
    {
        animator.SetTrigger("death");
    }

    public void Respawn()
    {
        transform.position = checkPointSaver.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathPlatform"))
        {
            KillPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ControlLocker"))
        {
            cloneControl.ControlLock();
        }
        if (collision.gameObject.CompareTag("EndOfTheLevel"))
        {
            cloneControl.EndOfTheLevel();
        }
    }

}
