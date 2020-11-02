using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomGenerator : MonoBehaviour
{
    public GameObject itemholder;

    public List<ItemRandList> list;

    public List<string> curses = new List<string>(){"confusion","stupid","blindness"};

    public List<int> pri;

    public void createRandomItem(Vector2 pos){
        pri = new List<int>(){};
        int num = UnityEngine.Random.Range(1,100);
        Debug.Log("<color=green> num: "+num+"</color>");
        int ii =0;
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].rarity >= num)
                pri.Add(i);

            ii++;
        }
        ItemRandList ls;
        // Если не выпал никакой предмет
        try{
            if(pri.Count == 0){
                ls = list[0];
            }
            else{
                int listindex = UnityEngine.Random.Range(0,pri.Count-1);
                ls = list[UnityEngine.Random.Range(0,pri.Count-1)];
                Debug.Log("<color=green>"+listindex.ToString()+" / s"+pri.Count.ToString()+"</color>");
                Debug.Log(ls.item.Id);
            }
            var obj = Instantiate(itemholder, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<ItemHolder>().item=ls.item;
            obj.GetComponent<ItemHolder>().amount = 1;
            obj.transform.position = pos;
            int curse = UnityEngine.Random.Range(1,100);
            if(ls.cursechance>=curse){
                obj.GetComponent<ItemHolder>().item.curseType=CurseType.Cursed;
                    int curseindex = UnityEngine.Random.Range(0,curses.Count-1);
                    
                    obj.GetComponent<ItemHolder>().item.curseData=curses[curseindex];
            }
            }catch(Exception e){
                Debug.Log(e);
        }
    }
}
[System.Serializable]
public class ItemRandList{
    public Item item;
    public int rarity;
    public int cursechance;
}