using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ItemHolder : MonoBehaviour, ISerializationCallbackReceiver
{
    public Item item;
    public int amount =1;

    private void Awake() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer =GetComponent<SpriteRenderer>();
        renderer.sprite = item.image;
    }
    
    public void OnAfterDeserialize(){
        
    }

    public void OnBeforeSerialize(){
        /*gameObject.GetComponent<SpriteRenderer>().sprite = item.image;
        EditorUtility.SetDirty(GetComponent<SpriteRenderer>());*/
    }
}
