using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShoting : MonoBehaviour
{

    
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projecttile;
    private Transform player;



    void Start()
    {
        timeBtwShots = Random.Range(2f,5f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        }else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > stoppingDistance) {

            transform.position = this.transform.position;

        } else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

        }

        if ((Vector2.Distance(transform.position, player.position) < stoppingDistance) && (Vector2.Distance(transform.position, player.position) > retreatDistance))
        {
            if (timeBtwShots <= 0)
            {   
                var player = GameObject.FindGameObjectWithTag("Player").transform;
                var pp = Instantiate(projecttile, transform.position, Quaternion.identity);
                pp.GetComponent<EnemyShoting_Bullet>().target = new Vector2(player.position.x, player.position.y);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
   

    }


}
