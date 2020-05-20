using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float _moveSpeed;
    [SerializeField] float _boundsPadding = 1f;
    [SerializeField] float _laserVelocity = 10f;
    [SerializeField] GameObject _laser;

    float xMin, xMax, yMin, yMax;
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
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _boundsPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _boundsPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _boundsPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _boundsPadding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var calculatedPos = (Vector2) transform.position + new Vector2(deltaX, deltaY) * Time.deltaTime * _moveSpeed;
        var newX = Mathf.Clamp(calculatedPos.x, xMin, xMax);
        var newY = Mathf.Clamp(calculatedPos.y, yMin, yMax);
        transform.position = new Vector2(newX, newY);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var laser = Instantiate(_laser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _laserVelocity);
        }
    }

}
