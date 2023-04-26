using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform UICanvas;
    public GameObject SkillIcon;
    public static GameObject skillicondrag;

    void Start()
    {
        UICanvas = GameObject.Find("UI").GetComponent<Transform>();
    }

    
    void Update()
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Drag.Dragging = true;
        Drag.Dragitem = Instantiate(SkillIcon);
        Drag.Dragitem.transform.SetParent(UICanvas);
        Drag.Dragitem.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Drag.Dragitem.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Drag.Dragitem.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(Drag.Dragitem.transform.parent == UICanvas)
        {
            Destroy(Drag.Dragitem);
        }
        if (Drag.Dragitem != null)
        {
            Drag.Dragitem = null;
        }
        Drag.Dragging = false;
    }
}
