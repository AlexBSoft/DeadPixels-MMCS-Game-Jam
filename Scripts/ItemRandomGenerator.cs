using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomGenerator : MonoBehaviour
{
    public GameObject itemholder;

    public List<ItemRandList> list;

    public List<int> pri;

    public void createRandomItem(Vector2 pos){
        int num = Random.Range(1,100);
        int ii =0;
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].rarity >= num){
                pri.Add(i);
                //pri.Insert(ii,i);
                ii++;
            }
        }
        
        var ls = list[Random.Range(0,pri.Count-1)];
        Debug.Log(ls.item.Id);
        var obj = Instantiate(itemholder, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<ItemHolder>().item=ls.item;
        obj.GetComponent<ItemHolder>().amount = 1;
        obj.transform.position = pos;
        int curse = Random.Range(1,100);
        if(ls.cursechance>=curse){
            obj.GetComponent<ItemHolder>().item.curseType=CurseType.Cursed;
            List<string> curses = new List<string>(){"confusion","stupid","blindness"};
            obj.GetComponent<ItemHolder>().item.curseData=curses[Random.Range(0,2)];
        }
    }
}
[System.Serializable]
public class ItemRandList{
    public Item item;
    public int rarity;
    public int cursechance;
}