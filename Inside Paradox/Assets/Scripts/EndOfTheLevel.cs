using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class EndOfTheLevel : MonoBehaviour
{
    [SerializeField] CloneControl cloneControl = null;
    [SerializeField] Transform endOfTheLevelTarget = null;

    private void Update()
    {
        if(transform.position.x == endOfTheLevelTarget.position.x && cloneControl.IsEnd())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
