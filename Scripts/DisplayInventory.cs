using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public Inventory inventory;

    public MouseItem mouseItem = new MouseItem();

    public int X_START;
    public int Y_START;

    public int X_SPACE_BETWEEN;
    public int NUMBER_OF_COLLUMN;
    public int Y_SPACE_BETWEEN;

    public GameObject slotObject;
    public GameObject tooltipObject;
    public Sprite CursedTooltip;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    void Start()
    {
        CreateSlots();
        StartCoroutine(SetEInventory());
    }
    void Update()
    {
        //UpdateDisplay();
        UpdateSlots();
    }
    IEnumerator SetEInventory()
    {
        yield return new WaitForSeconds(1);
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for(int i = 0; i<inventory.Container.Items.Length;i++){
            InventorySlot slot = inventory.Container.Items[i];
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition (i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text=slot.amount.ToString("n0");
            obj.GetComponent<Image>().sprite = slot.item.image;
            itemsDisplayed.Add(obj,slot);
            AddEvent(obj, EventTriggerType.PointerEnter, delegate{OnEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit, delegate{OnExit(obj);});
            AddEvent(obj, EventTriggerType.BeginDrag, delegate{OnDragStart(obj);});
            AddEvent(obj, EventTriggerType.EndDrag, delegate{OnDragEnd(obj);});
            AddEvent(obj, EventTriggerType.Drag, delegate{OnDrag(obj);});
            AddEvent(obj, EventTriggerType.PointerClick, delegate{OnClick(obj);});
        }
    }
    public void UpdateDisplay(){
      /*  for(int i = 0; i<inventory.Container.Items.Count;i++){
            InventorySlot slot = inventory.Container.Items[i];
            if(itemsDisplayed.ContainsKey(slot)){
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text=slot.amount.ToString("n0");
            }else{
                var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition (i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text=slot.amount.ToString("n0");
                obj.GetComponent<Image>().sprite = slot.item.image;

                AddEvent(obj, EventTriggerType.PointerEnter, delegate{OnEnter(obj);});
                AddEvent(obj, EventTriggerType.PointerExit, delegate{OnExit(obj);});
                //AddEvent(obj, EventTriggerType.BeginDrag, delegate{OnDragStart(obj);});
                //AddEvent(obj, EventTriggerType.EndDrag, delegate{OnDragEnd(obj);});
                //AddEvent(obj, EventTriggerType.Drag, delegate{OnDrag(obj);});

                itemsDisplayed.Add(slot,obj);
            }
        }*/
    }
    public void UpdateSlots(){
        foreach (KeyValuePair<GameObject,InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.Id >= 0){
                _slot.Key.transform.GetComponent<Image>().sprite = _slot.Value.item.image;
                _slot.Key.transform.GetComponent<Image>().color = new Color(1,1,1,1);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text=_slot.Value.amount == 1 ? "": _slot.Value.amount.ToString("n0");
                
            }else{
                _slot.Key.transform.GetComponent<Image>().sprite = null;
                _slot.Key.transform.GetComponent<Image>().color = new Color(1,1,1,0);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text="";
            }
        }
    }

    public void CreateSlots(){
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();


        for(int i = 0; i<inventory.Container.Items.Length;i++){
            InventorySlot slot = inventory.Container.Items[i];
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition (i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text=slot.amount.ToString("n0");
            obj.GetComponent<Image>().sprite = slot.item.image;
            itemsDisplayed.Add(obj,slot);
            AddEvent(obj, EventTriggerType.PointerEnter, delegate{OnEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit, delegate{OnExit(obj);});
            AddEvent(obj, EventTriggerType.BeginDrag, delegate{OnDragStart(obj);});
            AddEvent(obj, EventTriggerType.EndDrag, delegate{OnDragEnd(obj);});
            AddEvent(obj, EventTriggerType.Drag, delegate{OnDrag(obj);});
            AddEvent(obj, EventTriggerType.PointerClick, delegate{OnClick(obj);});
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj){
        //Debug.Log(itemsDisplayed[obj].Id);
        mouseItem.hoverObject = obj;
        if(itemsDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = itemsDisplayed[obj];
        if(itemsDisplayed[obj].Id > 0){
            var tooltip = Instantiate(tooltipObject, Vector3.zero, Quaternion.identity, transform);
            if(itemsDisplayed[obj].item.curseType == CurseType.Cursed){
                tooltip.GetComponent<Image>().sprite = CursedTooltip;
            }
            tooltip.transform.position = new Vector3(obj.transform.position.x -24,obj.transform.position.y -24,0f);
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text=itemsDisplayed[obj].item.Name;
            string desc_text = "";
            desc_text = itemsDisplayed[obj].item.curseType.ToString()+"\n";
            if(itemsDisplayed[obj].item.curseData != null)
                desc_text += itemsDisplayed[obj].item.curseData+"\n";
            for (int i = 0; i < itemsDisplayed[obj].item.buffs.Length; i++)
            {
                desc_text+= itemsDisplayed[obj].item.buffs[i].value + " "+ itemsDisplayed[obj].item.buffs[i].attribute+"\n";
            }
            tooltip.transform.GetComponentsInChildren<TextMeshProUGUI>()[1].text = desc_text;
            mouseItem.tooltip = tooltip;
        }
    }
    public void OnExit(GameObject obj){
        Destroy(mouseItem.tooltip);
        mouseItem.hoverObject = null;
        mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj){
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50,50);
        mouseObject.transform.SetParent(transform.parent);
        if(itemsDisplayed[obj].Id >= 0){
            var img = mouseObject.AddComponent<Image>();
            img.sprite = itemsDisplayed[obj].item.image;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];
    }
    public void OnDrag(GameObject obj){
        if(mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    public void OnDragEnd(GameObject obj){
        if(mouseItem.hoverObject){
            inventory.MoveItem(itemsDisplayed[obj],itemsDisplayed[mouseItem.hoverObject]);
        }else{
            inventory.RemoveItem(itemsDisplayed[obj].item);
            // TODO: DROP
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    
    public void OnClick(GameObject obj){
        if(itemsDisplayed[obj].item.type == ItemType.Consumable){
            itemsDisplayed[obj].item.Use(1);
            inventory.RemoveItem(itemsDisplayed[obj].item,1);
        }
    }


    public Vector3 GetPosition (int i){
        return new Vector3(X_START+ (X_SPACE_BETWEEN * (i%NUMBER_OF_COLLUMN)), Y_START+(-Y_SPACE_BETWEEN * (i/NUMBER_OF_COLLUMN)), 0f);
    }
}
public class MouseItem{
    public GameObject obj;
    public GameObject tooltip;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObject;
}
