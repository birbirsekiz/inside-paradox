using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintControl : MonoBehaviour
{
    [SerializeField] GameObject checkPointSaver;
    [SerializeField] GameObject exitHintArea;
    [SerializeField] CloneControl cloneController;
    [SerializeField] TextMeshProUGUI hintText;

    private bool enterHintArea = false;
    private bool allHintRead = false;
    private byte count = 0;

    private List<string> hints;

    private void Start()
    {
        hints = new List<string>
        {
            "Press 'E' to clone",
            "Press 'Q' to change player",
            "Again press 'E' to merge",
            "Let's play!"
        };
    }

    private void Update()
    {
        if (checkPointSaver.transform.position.x != 0 && !allHintRead && !enterHintArea)
        {
            enterHintArea = true;
            count++;
        }

        if (count == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hintText.text = hints[count++];
            }
        }

        if (count == 2)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                hintText.text = hints[count++];
            }
        }

        if (count == 3)
        {
            if (!cloneController.Cloned())
            {
                hintText.text = hints[count++];
            }
        }
        
        if (count == 4)
        {
            allHintRead = false;
            exitHintArea.SetActive(false);
        }
    }
}
