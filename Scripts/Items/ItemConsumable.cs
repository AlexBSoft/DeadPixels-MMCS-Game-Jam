using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UseAspect
{
    None,
    Lives,
}

[CreateAssetMenu(fileName= "ItemConsumable", menuName="InventorySystem/Items/ItemConsumable")]
public class ItemConsumable : Item
{
    /*
    public UseAspect useAspect = UseAspect.None;
    public int useAspectAmount=1;

    public void Use(){
        if(useAspect == UseAspect.Lives){
            GameManager.instance.lives += useAspectAmount;
        }
    }
    */
}
