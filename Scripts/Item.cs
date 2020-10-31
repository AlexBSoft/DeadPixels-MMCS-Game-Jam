using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Wearable,
    Consumable
}
public enum CurseType
{
    Normal,
    Cursed,
    Holy
}

[CreateAssetMenu(fileName= "ItemData", menuName="InventorySystem/Items/ItemData")]
public class Item : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite image;
    public ItemType type;
    public CurseType curseType = CurseType.Normal;
    public string curseData;
    public string itemDescription;
    public bool stackable = true;

    public ItemBuff[] buffs;
    
    public UseAspect useAspect = UseAspect.None;
    public int useAspectAmount=1;

    public ItemData CreateItem(){
        ItemData newItem = new ItemData(this);
        return newItem;
    }
}

[System.Serializable]
public class ItemData
{
    public string Name;
    public int Id;
    public Sprite image;
    public bool stackable;
    public ItemType type;
    public CurseType curseType;
    public string curseData;
    public ItemBuff[] buffs;
    public UseAspect useAspect;
    public int useAspectAmount;
    public ItemData(Item item){
        Name = item.Name;
        Id = item.Id;
        image = item.image;
        stackable = item.stackable;
        useAspect = item.useAspect;
        useAspectAmount = item.useAspectAmount;
        type = item.type;
        curseType = item.curseType;
        curseData = item.curseData;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i]=new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
    public void Use(int value = 1){
        if(useAspect == UseAspect.Lives){
            GameManager.instance.lives += useAspectAmount;
        }
    }
}

[System.Serializable]
public class ItemBuff{
    public PlayerBuffs attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max){
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue(){
       value = UnityEngine.Random.Range(min, max);
    }
}