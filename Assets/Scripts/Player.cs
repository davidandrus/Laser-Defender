﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float boundsPadding = 1f;
    [SerializeField] float laserVelocity = 10f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float shootDelay = .1f;
    

    private float xMin, xMax, yMin, yMax;
    private Coroutine shootCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + boundsPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - boundsPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + boundsPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - boundsPadding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var calculatedPos = (Vector2) transform.position + new Vector2(deltaX, deltaY) * Time.deltaTime * moveSpeed;
        var newX = Mathf.Clamp(calculatedPos.x, xMin, xMax);
        var newY = Mathf.Clamp(calculatedPos.y, yMin, yMax);
        transform.position = new Vector2(newX, newY);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shootCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(shootCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true) {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserVelocity);
            yield return new WaitForSeconds(shootDelay);
        }
    }

}
