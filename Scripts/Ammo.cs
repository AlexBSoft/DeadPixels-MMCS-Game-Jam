using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public float TimeToDestroyBul;

    public AudioClip clip;

    private Vector3 target;
 
    private void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        if(target.x == 0 && target.y == 0)
        {
            target.x += 1;
            target.y += 1;
        }
        Vector3 direction = target - transform.position;
        target += 5000 * direction;
       }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        TimeToDestroyBul += Time.deltaTime;

        if (TimeToDestroyBul > 5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);
        //Debug.Log(other.tag);
        if (other.tag == "Enemy")
        {
            if(other.GetComponent<EnemyMain>()){
                other.GetComponent<EnemyMain>().lives-= Mathf.Abs(GameManager.instance.PlayerController.characteristics[0]);
                if(other.GetComponent<EnemyMain>().lives < 1){
                    GameManager.instance.LogText("Enemy killed");
                    other.gameObject.GetComponent<EnemyMain>().Die();
                    //GameManager.instance.enemies_left--;
                    //Destroy();
                    //GameManager.instance.kills+=1;
                }
            }
        Destroy(gameObject);
        }
        if (other.tag == "Obstacle"){
            Destroy(gameObject);
        }
    }
    private void OnColliderEnter2D(Collider2D other){
        Debug.Log(other);
    }
}