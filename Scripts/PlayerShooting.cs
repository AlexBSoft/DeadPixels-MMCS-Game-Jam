using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject projecttile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("FIRE");
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //var bullet = Instantiate(projecttile, transform.position, Quaternion.identity);
            //bullet.target = new Vector2(Input.x, player.position.y);
        }
    }
}
