using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunController : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    bool randomInterval = false;

    [SerializeField][Min(0f)]
    float shootingIntervalMin = 4f;
    [SerializeField]
    float shootingIntervalMax = 7f;

    [SerializeField]
    float shootingInterval = 2f;    //Set to 0 for fun

    public float boostAmount = 1f;

    float time = 0f;

    private void Start()
    {
        DrawInterval();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= shootingInterval / boostAmount)
        {
            Fire();
            time = 0f;
        }
    }

    private void Fire()
    {
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation).GetComponent<ProjectileController>().source = gameObject;

        DrawInterval();
    }

    //Draw random interval if true
    void DrawInterval()
    {
        if (randomInterval)
            shootingInterval = Random.Range(shootingIntervalMin, shootingIntervalMax);
    }
}
