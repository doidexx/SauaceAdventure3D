using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    [SerializeField] float speed;

    private float damage;
    private float range;
    private Vector3 spawnedAt;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Vector3.Distance(spawnedAt, transform.position) > range)
            Disable();
    }

    public void SetStats(float _damage, float _range)
    {
        damage = _damage;
        range = _range;
    }

    public void Launch(Vector3 dir)
    {   
        spawnedAt = transform.position;
        rb.velocity = dir * speed;
        trail.emitting = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        var health = other.transform.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(damage);
        Disable();
    }

    private void Disable()
    {
        trail.emitting = false;
        gameObject.SetActive(false);
    }
}
