using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShot : MonoBehaviour
{
    public GameObject ammo;
    public float Cooldown;

    
    void Start()
    {

    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        float rotateZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        

        if (Input.GetMouseButtonDown(0))
        {

            if (Cooldown < 1)
            {
                Instantiate(ammo, GameManager.instance.Player.transform.position, transform.rotation);
                Cooldown = 1.5f*Mathf.Sqrt(Mathf.Abs(GameManager.instance.PlayerController.characteristics[PlayerBuffs.intelligence]));
            }

        }
        
        if(Cooldown > 0){
            if(GameManager.instance.PlayerController.characteristics[PlayerBuffs.intelligence]<1)
                Cooldown -= Time.deltaTime;
            else
                Cooldown -= Time.deltaTime*GameManager.instance.PlayerController.characteristics[PlayerBuffs.intelligence];
        }

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
