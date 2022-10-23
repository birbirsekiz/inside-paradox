using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CloneControl : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] GameObject characterWhite = null;
    [SerializeField] GameObject characterBlack = null;

    [Header("Virtual Camera")]
    [SerializeField] GameObject vcam = null;

    [Header("End of The Level")]
    [SerializeField] Transform endOfTheLevelFollowObj = null;

    private CinemachineVirtualCamera virtualCamera = null;
    private CinemachineFramingTransposer virtualCamTransposer = null;

    private SpriteRenderer characterWhiteCrown;
    private SpriteRenderer characterBlackCrown;

    private bool cloned = false;
    private bool closeEnough = false;
    private bool isCharacterWhite = true;
    private float mergeDistance = 2.0f;

    private bool endOfTheGame = false;

    private void Start()
    {
        virtualCamera = vcam.GetComponent<CinemachineVirtualCamera>();
        virtualCamTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        characterWhiteCrown = characterWhite.transform.Find("CharacterCrown").GetComponent<SpriteRenderer>();
        characterBlackCrown = characterBlack.transform.Find("CharacterCrown").GetComponent<SpriteRenderer>();

        virtualCamera.Follow = characterWhite.transform.Find("FollowCamera");
    }

    private void Update()
    {
        if (!endOfTheGame)
        {
            Clone();
            ActivePlayer();
        }
    }

    private void Clone()
    {
        // Character's crowns colors 
        // if clone exist
        if (cloned)
        {
            // calculate the distance between characters
            float dist = Vector2.Distance(characterBlack.transform.position, characterWhite.transform.position);

            // if the distance close enough
            if (dist < mergeDistance)
            {
                closeEnough = true;
                characterWhiteCrown.color = new Color(250 / 255f, 150 / 255f, 0 / 255f, 1);
                characterBlackCrown.color = new Color(250 / 255f, 150 / 255f, 0 / 255f, 1);
            }
            // if the distance not close enough
            else
            {
                closeEnough = false;
                characterWhiteCrown.color = Color.black;
                characterBlackCrown.color = Color.black;
            }
        }
        // if clone does not exist
        else
        {
            characterWhiteCrown.color = Color.white;
            characterBlackCrown.color = Color.white;
        }

        // Character cloning
        if (Input.GetKeyDown(KeyCode.E))
        {
            // if clone does not exist
            if (!cloned)
            {
                characterBlack.transform.position = new Vector2(characterWhite.transform.position.x - 1.0f, characterWhite.transform.position.y);
                characterBlack.SetActive(true);
                cloned = true;
                SetCharacterBlackActive();
            }

            // if clone exist
            else
            {
                if (closeEnough)
                {
                    characterBlack.SetActive(false);
                    cloned = false;
                    SetCharacterWhiteActive();
                }
            }
        }
    }

    private void ActivePlayer()
    {
        // Change Virtual Camera follow player by pressing Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isCharacterWhite && cloned)
            {
                SetCharacterBlackActive();
            }
            else
            {
                SetCharacterWhiteActive();
            }
        }

    }

    void SetCharacterWhiteActive()
    {
        virtualCamera.Follow = characterWhite.transform.Find("FollowCamera");
        isCharacterWhite = true;

        characterBlack.GetComponent<PlayerControl>().enabled = false;
        characterBlack.GetComponent<PlayerMovement>().enabled = false;

        characterWhite.GetComponent<PlayerControl>().enabled = true;
        characterWhite.GetComponent<PlayerMovement>().enabled = true;

        characterWhiteCrown.enabled = true;
        characterBlackCrown.enabled = false;
    }

    void SetCharacterBlackActive()
    {
        virtualCamera.Follow = characterBlack.transform.Find("FollowCamera");
        isCharacterWhite = false;

        characterWhite.GetComponent<PlayerControl>().enabled = false;
        characterWhite.GetComponent<PlayerMovement>().enabled = false;

        characterBlack.GetComponent<PlayerControl>().enabled = true;
        characterBlack.GetComponent<PlayerMovement>().enabled = true;

        characterBlackCrown.enabled = true;
        characterWhiteCrown.enabled = false;
    }

    public bool Cloned()
    {
        return cloned;
    }

    public bool CharacterWhiteActive()
    {
        return isCharacterWhite;
    }

    public GameObject GetFollowCam()
    {
        if (isCharacterWhite)
        {
            return characterWhite.transform.Find("FollowCamera").gameObject;
        }
        return characterBlack.transform.Find("FollowCamera").gameObject;
    }

    public void EndOfTheLevel()
    {
        virtualCamera.Follow = endOfTheLevelFollowObj;
        virtualCamTransposer.m_DeadZoneWidth = 0;
        virtualCamTransposer.m_DeadZoneHeight = 0;
        characterBlack.GetComponent<PlayerControl>().KillPlayer();
        characterWhite.GetComponent<PlayerControl>().KillPlayer();
    }

    public void ControlLock()
    {
        endOfTheGame = true;
        characterWhite.GetComponent<PlayerMovement>().enabled = false;
        characterBlack.GetComponent<PlayerMovement>().enabled = false;
    }

    public bool IsEnd()
    {
        return endOfTheGame;
    }
}
