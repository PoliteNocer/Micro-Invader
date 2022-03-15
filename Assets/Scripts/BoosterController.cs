using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterController : MonoBehaviour
{
    Image _img;

    float time = 0f;

    [SerializeField][Min(0f)]
    float cooldown = 40f;
    
    [SerializeField][Min(1f)]
    float boostTime = 5f;

    [SerializeField][Min(1.1f)]
    float boostAmount = 2f;

    [SerializeField]
    GunController targetGun;

    [SerializeField]
    Color readyColor;
    [SerializeField]
    Color cooldownColor;

    bool boosted = false;

    void Start()
    {
        if (!_img) _img = gameObject.GetComponent<Image>();

        if (!targetGun) targetGun = GameObject.FindGameObjectWithTag("Player").GetComponent<GunController>();   //If not set -> choose default player weapon

        _img.color = cooldownColor;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (!boosted)
        {
            _img.fillAmount = time / cooldown;
        }
        else
        {
            _img.fillAmount = (boostTime - time) / boostTime;
        }

        if (time >= cooldown)
            _img.color = readyColor;
    }

    public void FireBoost()
    {
        if (time >= cooldown)
        {
            StartCoroutine(FireBoostCoroutine());
        }
    }

    IEnumerator FireBoostCoroutine()
    {
        boosted = true;
        time = 0f;

        float prevBoost = targetGun.boostAmount;
        targetGun.boostAmount = boostAmount;
        yield return new WaitForSeconds(boostTime);
        targetGun.boostAmount = prevBoost;

        _img.color = cooldownColor;
        boosted = false;
        time = 0f;
    }
}
