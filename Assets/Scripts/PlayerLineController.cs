using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLineController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver(false);
        }
    }
}
