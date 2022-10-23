using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControl : MonoBehaviour
{
    [SerializeField] GameObject checkPointSaver;
    [SerializeField] Color checkPointCheckedColor = new Color(0, 0, 0, 1);

    private ParticleSystem particle;
    private bool pointChecked = false;

    private void Start()
    {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pointChecked)
        {
            pointChecked = true;
            var particleMain = particle.main;
            particleMain.startColor = checkPointCheckedColor;
            particle.Stop();
            particle.Play();
            checkPointSaver.transform.position = transform.position;
            //collision.gameObject.GetComponent<PlayerControl>().setCheckPoint(transform.position);
        }
    }
    
}
