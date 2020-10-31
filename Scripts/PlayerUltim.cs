using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltim : MonoBehaviour
{
    public GameObject UltAttack;
    public float Cooldown;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

            if (Cooldown < 1)
            {
                Instantiate(UltAttack, GameManager.instance.Player.transform.position, transform.rotation);
                Cooldown = 10f;
            }

        }
        if(Cooldown > 0)
            Cooldown -= Time.deltaTime;
    }
}