using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    public int lives=10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetLives(-1);
            Die();
        }
    }

    public void Die(){
        var ded = GameObject.Find("ItemRandomGenerator").GetComponent<ItemRandomGenerator>();
        ded.createRandomItem(transform.position);
        GameManager.instance.kills = GameManager.instance.kills + 1;
        Destroy(gameObject);
    }
}
