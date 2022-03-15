using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject explosionParticles;

    GameController _gameController;

    private void Start()
    {
        if (!_gameController) _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(Instantiate(explosionParticles, transform.position, other.transform.rotation), 5f); //Spawn and destroy explosion after 5 seconds.

            _gameController.LoosePoints(other.transform.parent.childCount);

            _gameController.EnemyDestroyed();
            Destroy(other.gameObject);
        }
    }
}
