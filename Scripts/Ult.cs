using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ult : MonoBehaviour
{
    public float TimeToDestroyUlt;
    void Start()
    {
        
    }
    void Update()
    {
        TimeToDestroyUlt += Time.deltaTime;

        if (TimeToDestroyUlt > 2)
            DestroyAmmo();
    }
/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);
        Debug.Log(other.tag);
        if (other.tag == "Enemy")
        {
       //     Destroy(gameObject);
            if (other.GetComponent<EnemyMain>())
            {
                other.GetComponent<EnemyMain>().lives -= GameManager.instance.PlayerController.characteristics[0]/3;
                if (other.GetComponent<EnemyMain>().lives < 1)
                {
                    GameManager.instance.LogText("Enemy killed");
                    GameManager.instance.kills++;
                    Destroy(other.gameObject);
                }
            }
        }
    }*/

    void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
