using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ghost : MonoBehaviour
{
    public float speed;
    private Transform player;

    double TimeToTeleport = 0;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //Передвижение
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        //Телепорт
        Vector3 direction = player.position - transform.position;
        TimeToTeleport += 1 * Time.deltaTime;

        if (TimeToTeleport > 10)
        {
            transform.position += direction * 2;
            TimeToTeleport = 0;
        }
    }

}