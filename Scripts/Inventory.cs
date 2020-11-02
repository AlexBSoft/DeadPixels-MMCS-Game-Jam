using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Inventory", menuName="InventorySystem/Inventory")]
public class Inventory : ScriptableObject
{
    public InventoryList Container = new InventoryList();

    public InventorySlot AddItem(ItemData _item, int _amount){
        //SetEmptySlot(_item, _amount);
        //bool hasItem = false;
        for(int i = 0; i < Container.Items.Length; i++){
            if(Container.Items[i].Id!= -1&&Container.Items[i].Id == _item.Id && Container.Items[i].item.stackable){
                Container.Items[i].AddAmount(_amount);
                //hasItem = true;
                return Container.Items[i];
            }
        }
        var slot = SetEmptySlot(_item, _amount);
            if(slot == null) // Если не удалось вставить предмет
                return null;
            else
                return slot;
    }
    public InventorySlot SetEmptySlot(ItemData _item, int _amount){
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].Id <= 0){
                Container.Items[i].UpdateSlot(_item.Id,_item,_amount);
                return Container.Items[i];
            }
        }
        GameManager.instance.LogText("Inventory Is FULL");
        Debug.Log("Inventory Is FULL");
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2){
        InventorySlot temp = new InventorySlot(item2.Id, item2.item, item2.amount);
        item2.UpdateSlot(item1.Id, item1.item, item1.amount);
        item1.UpdateSlot(temp.Id,temp.item,temp.amount);
    }
    public void RemoveItem(ItemData _item, int _amount =0){
        if(_item.curseType == CurseType.Cursed){
            GameManager.instance.LogText("Cused item can not be dropped");
            return;
        }
        for (int i = 0; i < Container.Items.Length; i++)
        {   
            if(Container.Items[i].item == _item){
                // If buffs
                for (int ii = 0; ii < Container.Items[i].item.buffs.Length; ii++)
                {
                    GameManager.instance.PlayerController.RemoveBuff(Container.Items[i].item.buffs[ii].attribute, Container.Items[i].item.buffs[ii].value) ;
                }
                if(_amount == 0 || Container.Items[i].amount - _amount < 1){
                    Container.Items[i].UpdateSlot(-1, null, 0);
                }else{
                    Container.Items[i].amount -= _amount;
                }
            }
        }   
    }

    public void SetAllSlotsEmpty(){
        for (int i = 0; i < Container.Items.Length; i++)
        {  
            Container.Items[i].item = null;
            Container.Items[i].amount = 0;
            Container.Items[i].Id = -1;
        }
    }
}


[System.Serializable]
public class InventoryList {
    public InventorySlot[] Items = new InventorySlot[14];
}


[System.Serializable]
public class InventorySlot {
    public int Id = -1;
    public ItemData item;
    public int amount;
    public InventorySlot(){
        Id = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, ItemData _item, int _amount){
        Id = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, ItemData _item, int _amount){
        Id = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value){
        amount += value;
    }
}