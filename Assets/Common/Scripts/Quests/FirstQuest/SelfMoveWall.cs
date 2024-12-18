using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfMoveWall : MonoBehaviour
{
    private Vector3 start;

    private bool isMoving;
    public bool IsMoving { get { return isMoving; } }


    [SerializeField] private float targetHeightOffset = 6f; // Конечная высота подъёма
    public float TargetHeightOffset { get { return targetHeightOffset; } }

    
    public float speed = 2f; // Скорость подъёма стены

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        start = transform.position;
    }

    public void StartMove()
    {
        isMoving = true;
        audioSource.Play();
    }

    private void Move()
    {
        Debug.Log("Move");
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public bool IsMoveDone()
    {
        return start.y + TargetHeightOffset < transform.position.y;
    }

    public void Update()
    {
        if (isMoving && !IsMoveDone())
        {
            Move();
        }
        if (IsMoveDone())
        {
            isMoving = false;
        }
    }
}
