using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParticle : MonoBehaviour
{
    [Header("Button Type")]
    [SerializeField] bool isEnterButton;
    [SerializeField] bool isStayButton;

    [SerializeField] Color startColor = new Color(0, 0, 0, 1);
    [SerializeField] Color pressColor = new Color(0, 0, 0, 1);

    private bool entered = false;
    private bool staying = false;
    private bool exited = false;

    private ParticleSystem particle;

    private void Start()
    {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isEnterButton)
        {
            var particleMain = particle.main;
            particleMain.startColor = pressColor;
            particle.Stop();
            particle.Play();

            entered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isStayButton)
        {
            var particleMain = particle.main;
            particleMain.startColor = pressColor;
            particle.Stop();
            particle.Play();

            staying = true;
            exited = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isStayButton)
        {
            var particleMain = particle.main;
            particleMain.startColor = startColor;
            particle.Stop();
            particle.Play();

            staying = false;
            exited = true;
        }
    }

    public bool ButtonEnter() { return entered; }
    public bool ButtonStay() { return staying; }
    public bool ButtonExit() { return exited; }
    public string ButtonType() { return isEnterButton ? "enter" : "stay"; }
}
