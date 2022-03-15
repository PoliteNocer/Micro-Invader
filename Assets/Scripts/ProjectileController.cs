using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField]
    float startingVelocity = 10f;

    [SerializeField]
    bool enemy = false;

    [SerializeField]
    float dissapearTime = 1f;

    [SerializeField]
    GameObject explosionParticles;

    GameController _gameController;

    public GameObject source;

    void Start()
    {
        if (!_gameController) _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (!_rb) _rb = GetComponent<Rigidbody>();

        if (!enemy)
            _rb.AddForce(Vector3.forward * startingVelocity, ForceMode.VelocityChange);
        else
            _rb.AddForce(Vector3.back * startingVelocity, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "End") //Destroy bullet if it touched end line
            Destroy(gameObject, dissapearTime);

        if (!enemy && other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            _gameController.EnemyDestroyed();

            Explode();
        }
        else if (enemy && other.tag == "Player")
        {
            if (source) 
                _gameController.LoosePoints(source.transform.parent.childCount * 2);    //Count how many enemies are in source object column and loose that amount * 2 points
            else
                _gameController.LoosePoints(2);

            Explode();
        }
    }

    void Explode()
    {
        Destroy(Instantiate(explosionParticles, transform.position, transform.rotation), 5f); //Spawn and destroy explosion after 5 seconds.
        Destroy(gameObject);
    }
}
