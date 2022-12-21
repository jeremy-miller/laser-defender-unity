using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private Vector2 moveSpeed;

    private Material _material;
    private Vector2 _offset;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _offset = moveSpeed * Time.deltaTime;
        _material.mainTextureOffset += _offset;
    }
}
