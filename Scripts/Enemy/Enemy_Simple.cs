using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple : MonoBehaviour
{
    public float speed;
    private Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

    }

    

}