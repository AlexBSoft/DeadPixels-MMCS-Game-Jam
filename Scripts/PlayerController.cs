using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerBuffs{
    strength,
    agility,
    intelligence
}
public class PlayerController : MonoBehaviour
{

    public Inventory inventory;
    public int lives;

    //public List<> 

    public Dictionary<PlayerBuffs, int> characteristics = new Dictionary<PlayerBuffs, int>(){
        {PlayerBuffs.strength, 155},
        {PlayerBuffs.agility, 30},
        {PlayerBuffs.intelligence, 155}
    };
    public Dictionary<string, int> PlayerCurses = new Dictionary<string, int>(){
        {"blindness", -1},
        {"colorblindness", -1},
        {"confusion", -1},
        {"stupid", -1},
        {"greed", -1}
    };
/*
    public List<float> characteristics = new List<float>{
    4f, // ST
    4f, // AG
    4f, // IN
    };
*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyBuff(PlayerBuffs buff, int value){
        characteristics[buff] += value;
    }
    public void RemoveBuff(PlayerBuffs buff, int value){
        characteristics[buff] -= value;
    }

    // Триггерим вход в триггер
    void OnTriggerEnter2D(Collider2D col)
    {
        var itemHolder = col.GetComponent<ItemHolder>();
        if(itemHolder){
            var pickup = inventory.AddItem(new ItemData(itemHolder.item), 1);
            if(pickup != null){
                GameManager.instance.LogText("You picked up "+itemHolder.item.Name);
                if(pickup.item.curseType == CurseType.Cursed){
                    GameManager.instance.LogText("This item is <color=#FF0000>Cused</color>");
                    SetCurse(pickup.item.curseData);
                }
                for (int i = 0; i < pickup.item.buffs.Length; i++)
                {
                    ApplyBuff(pickup.item.buffs[i].attribute, pickup.item.buffs[i].value) ;
                }
                

                Destroy(col.gameObject);
            }else{
                
            }
        }
    }

    public void SetCurse(string type, int a = 1){
        if(type == "confusion"){
            PlayerCurses[type] = a;
            GetComponent<PlayerMovement>().moveSpeedModifier *= -a;
        }
        if(type == "stupid"){
            PlayerCurses[type] = a*10;
            characteristics[PlayerBuffs.intelligence] -=a*10;
        }
        if(type == "greed"){
            PlayerCurses[type] = a;
            characteristics[PlayerBuffs.intelligence] -=a;
        }
        if(type == "blindness"){
            PlayerCurses[type] = a;
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize= 5;
            //characteristics[PlayerBuffs.intelligence] -=a;
        }
    }

    private void OnApplicationQuit(){
        ClearAll();
    }
    public void ClearAll(){
        //inventory.Container.Items.Clear();
        inventory.Container.Items = new InventorySlot[14];
        Dictionary<PlayerBuffs, int> characteristics = new Dictionary<PlayerBuffs, int>(){
            {PlayerBuffs.strength, 6},
            {PlayerBuffs.agility, 6},
            {PlayerBuffs.intelligence, 6}
        };
        Dictionary<string, int> PlayerCurses = new Dictionary<string, int>(){
            {"blindness", -1},
            {"colorblindness", -1},
            {"confusion", -1},
            {"stupid", -1},
            {"greed", -1}
        };
        }

    
}
