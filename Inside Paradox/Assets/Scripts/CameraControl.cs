using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] CloneControl cloneControl;
    [SerializeField] bool zoomOut = false;
    [SerializeField] bool zoomIn = false;

    private CinemachineFramingTransposer virtualCamTransposer;

    private GameObject followCam;

    private bool activeZoomOut = false;
    private bool activeZoomIn = false;
    private float smoothSpeed = 5.0f;

    private float minOrtho = 6.0f;
    private float minDeadZoneWidth = 0.2f;
    private float minDeadZoneHeight = 0.1f;
    private float minFollowCamX = 2.0f;
    private float minFollowCamY = 1.0f;

    private float maxOrtho = 8.0f;
    private float maxDeadZoneWidth = 0.4f;
    private float maxDeadZoneHeight = 0.4f;
    private float maxFollowCamX = 4.0f;
    private float maxFollowCamY = 2.0f;

    private void Start()
    {
        virtualCamTransposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        //virtualCam.m_Lens.OrthographicSize = minOrtho;

        minOrtho = virtualCam.m_Lens.OrthographicSize;
        minDeadZoneWidth = virtualCamTransposer.m_DeadZoneWidth;
        minDeadZoneHeight = virtualCamTransposer.m_DeadZoneHeight;
        minFollowCamX = cloneControl.GetFollowCam().transform.localPosition.x;
        minFollowCamY = cloneControl.GetFollowCam().transform.localPosition.y;
    }

    private void Update()
    {
        //Debug.Log("zoomout:" + zoomOut.ToString() + " - zoomin:" + zoomIn.ToString());

        if (activeZoomOut)
        {
            ZoomOut();
        }

        if (activeZoomIn)
        {
            ZoomIn();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && zoomOut)
        {
            activeZoomIn = false;
            activeZoomOut = true;
            followCam = cloneControl.GetFollowCam();
        }

        if(collision.gameObject.CompareTag("Player") && zoomIn)
        {
            activeZoomOut = false;
            activeZoomIn = true;
            followCam = cloneControl.GetFollowCam();
        }
    }

    private void ZoomOut()
    {
        virtualCam.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCam.m_Lens.OrthographicSize, maxOrtho, smoothSpeed * Time.deltaTime);
        virtualCamTransposer.m_DeadZoneWidth = maxDeadZoneWidth;
        virtualCamTransposer.m_DeadZoneHeight = maxDeadZoneHeight;

        followCam.transform.localPosition = new Vector2(
            Mathf.MoveTowards(followCam.transform.localPosition.x, maxFollowCamX, smoothSpeed * Time.deltaTime),
            Mathf.MoveTowards(followCam.transform.localPosition.y, maxFollowCamY, smoothSpeed * Time.deltaTime)
            );

        if (virtualCam.m_Lens.OrthographicSize >= maxOrtho) activeZoomOut = false;
    }

    private void ZoomIn()
    {
        virtualCam.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCam.m_Lens.OrthographicSize, minOrtho, smoothSpeed * Time.deltaTime);
        virtualCamTransposer.m_DeadZoneWidth = minDeadZoneWidth;
        virtualCamTransposer.m_DeadZoneHeight = minDeadZoneHeight;

        followCam.transform.localPosition = new Vector2(
            Mathf.MoveTowards(followCam.transform.localPosition.x, minFollowCamX, smoothSpeed * Time.deltaTime),
            Mathf.MoveTowards(followCam.transform.localPosition.y, minFollowCamY, smoothSpeed * Time.deltaTime)
            );

        if (virtualCam.m_Lens.OrthographicSize <= minOrtho) activeZoomIn = false;
    }

}
