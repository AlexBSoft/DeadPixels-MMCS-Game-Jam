using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoting_Bullet : MonoBehaviour
{
    public float speed;


    private Transform player;
    public Vector2 target;
    public float TimeDestroy = 1f;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        //target = new Vector2(player.position.x, player.position.y);

        if(target.x == 0 && target.y == 0)
        {
            target.x += 1;
            target.y += 1;
        }
        Vector2 direction = target - new Vector2(transform.position.x, transform.position.y);
        target += 5 * direction;
    }

    void Update()
    {

        
        //Vector2 direction = player.position - transform.position;
        //target += 2 * direction;      
       // чтобы устроить буллет хелл
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
/*
        
    
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            
        }
*/
        DestroyProjectile();
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetLives(-1);
            Destroy(gameObject);
        }
        if (other.tag == "Obstacle"){
            Destroy(gameObject);
        }
        
    }

    void DestroyProjectile()
    {
        if (TimeDestroy > 0)
        {
            TimeDestroy -= Time.deltaTime;
        }else
        Destroy(gameObject);
    }

}
