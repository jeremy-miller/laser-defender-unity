using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;

    private Vector2 _maxBounds;
    private Vector2 _minBounds;
    private Vector2 _rawInput;
    private Shooter _shooter;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }

    private void InitBounds()
    {
        _minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        _maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // multiply by Time.deltaTime to ensure movement is framerate-independent
        Vector2 delta = _rawInput * (moveSpeed * Time.deltaTime);
        Vector2 newPos = new Vector2
        {
            x = Mathf.Clamp(transform.position.x + delta.x, _minBounds.x + paddingLeft, _maxBounds.x - paddingRight),
            y = Mathf.Clamp(transform.position.y + delta.y, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop)
        };
        transform.position = newPos;
    }

    private void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if (_shooter != null)
        {
            _shooter.isFiring = value.isPressed;
        }
    }
}
