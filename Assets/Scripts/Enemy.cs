using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100f;

    [Header("Laser")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip laserSound;
    [SerializeField] float laserVelocity = 7f;
    
    [Header("Explosion")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip explosionSound;


    private void Start()
    {
        SetShotCounter();
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            SetShotCounter();
        }
    }

    private void SetShotCounter()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Fire()
    {
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserVelocity * -1);
        PlayLaserSound();
    }

    private void PlayLaserSound()
    {
        AudioSource.PlayClipAtPoint(laserSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {

        var explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject);
        Destroy(explosion, explosionDuration);
    }
}
