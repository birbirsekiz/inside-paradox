using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl : MonoBehaviour
{
    

    [Header("Buttons")]
    [SerializeField] ButtonParticle button;
    [SerializeField] ButtonParticle buttonClone;

    [Header("Obstacle Type")]
    [SerializeField] bool pingpongActive;
    [SerializeField] bool movesWhenEnter;
    [SerializeField] bool movesWhileStay;

    [Header("Obstacle Properties")]
    [SerializeField] float moveSpeed;
    [SerializeField] bool pingPongY;
    //[SerializeField] float moveLength;

    [Header("Target Coordinate")]
    [SerializeField] Transform targetPoint = null;

    private CloneControl cloneControl;

    private Vector2 startPos;
    private Vector2 targetPos;

    private void Start()
    {
        cloneControl = GameObject.FindGameObjectWithTag("CloneControl").GetComponent<CloneControl>();
        startPos = transform.position;

        if (targetPoint != null)
        {
            targetPos = targetPoint.position;
        }
    }

    private void Update()
    {
        if (cloneControl.IsEnd()) { EndOfTheLevel(); }
        else if (pingpongActive) { PingPongMove(); }
        else if (movesWhenEnter) { MovesWhenEnter(); }
        else if (movesWhileStay) { MovesWhileStay(); }
    }

    private void EndOfTheLevel()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private void PingPongMove()
    {
        if (pingPongY)
        {
            float moveLength = targetPoint.localPosition.y;
            float ppong = Mathf.PingPong(Time.time * moveSpeed, moveLength);
            transform.position = new Vector3(transform.position.x, startPos.y + ppong, transform.position.z);
        }
        else
        {
            float moveLength = targetPoint.localPosition.x;
            float ppong = Mathf.PingPong(Time.time * moveSpeed, moveLength);
            transform.position = new Vector3(startPos.x + ppong, transform.position.y, transform.position.z);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private void MovesWhenEnter()
    {
        if (button.ButtonEnter())
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    private void MovesWhileStay()
    {
        if (buttonClone != null)
        {
            if (button.ButtonStay() || buttonClone.ButtonStay())
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            }
            else if (button.ButtonExit() || buttonClone.ButtonExit())
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            }
        }

        else
        {
            if (button.ButtonStay())
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            }
            if (button.ButtonExit())
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            }
        }
        
    }
}
