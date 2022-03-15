using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCompanyController : MonoBehaviour
{
    [SerializeField][Min(0f)]
    float movementSpeed = 10f;
    [SerializeField][Min(0f)]
    float movementSpeedBoost = 1f;  //Boost amount after destroying enemy

    [SerializeField]
    float borderLeft = -20f;
    [SerializeField]
    float borderRight = 20f;
    [SerializeField]
    float movingDownLength = 20f;

    int movementPhase; //0 - left, 1 - right, 2 - down;

    private float prevStep;

    [SerializeField]
    float shootersBoost = 0.1f;

    private GameController _gameController;

    private void Start()
    {
        if (!_gameController) _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _gameController.enemyDestroyedEvent += BoostMovement;
        _gameController.enemyDestroyedEvent += BoostShooters;
    }

    private void Update()
    {
        Moving();
    }

    void Moving() 
    {
        switch (movementPhase)
        {
            case 0:
                gameObject.transform.position += Vector3.left * movementSpeed * Time.deltaTime;

                if (transform.position.x <= borderLeft + 0.1f)  //Approximation to border plus changing phase
                {
                    gameObject.transform.position = new Vector3(borderLeft, 0, transform.position.z);
                    movementPhase++;
                }
                break;
            case 1:
                gameObject.transform.position += Vector3.right * movementSpeed * Time.deltaTime;

                if (transform.position.x >= borderRight - 0.1f)  //Approximation to border plus changing phase
                {
                    gameObject.transform.position = new Vector3(borderRight, 0, transform.position.z);
                    movementPhase++;

                    prevStep = transform.position.z;
                }
                break;
            case 2:
                gameObject.transform.position += Vector3.back * movementSpeed * Time.deltaTime;

                if (transform.position.z <= prevStep - movingDownLength + 0.1f)  //Approximation to border plus changing phase
                {
                    gameObject.transform.position = new Vector3(transform.position.x, 0, prevStep - movingDownLength);
                    movementPhase = 0;
                }
                break;
            default:
                break;
        }
    }

    //Boost movement of all enemies after destroying one of them
    private void BoostMovement()
    {
        movementSpeed += movementSpeedBoost;
    }

    //Boost shoot speed of shooters after destroying one of enemy
    void BoostShooters()
    {
        foreach (GunController gC in GameObject.FindObjectsOfType<GunController>())
        {
            if (gC.gameObject.tag == "Enemy")
                gC.boostAmount += shootersBoost;
        }
    }
}
