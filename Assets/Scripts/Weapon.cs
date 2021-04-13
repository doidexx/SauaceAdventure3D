using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int ammoClip;
    [SerializeField] float range;
    [SerializeField] float damange;
    [SerializeField] float rate;
    [SerializeField] bool auto;
    [SerializeField] Transform shootingPoint;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform projectileHolder;
    
    private float timeSinceLastShot = Mathf.Infinity;
    private int lastUsedProjectile;
    private GameObject[] projectiles;

    private void Awake()
    {
        projectiles = new GameObject[ammoClip];
        for (int i = 0; i < ammoClip; i++)
        {
            projectiles[i] = Instantiate(projectile.gameObject, projectileHolder);
            projectiles[i].GetComponent<Projectile>().SetStats(damange, range);
            projectiles[i].SetActive(false);
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void Shoot()
    {
        if(!CanShoot()) return;
        var p = projectiles[GetNextProjectile()];
        p.transform.position = shootingPoint.position;
        p.SetActive(true);
        p.GetComponent<Projectile>().Launch(transform.forward.normalized);
        timeSinceLastShot = 0;
    }

    private int GetNextProjectile()
    {
        if (lastUsedProjectile + 1 >= ammoClip)
            return lastUsedProjectile = 0;
        else
            return lastUsedProjectile += 1;
    }

    private bool CanShoot()
    {
        return timeSinceLastShot > rate;
    }
}
