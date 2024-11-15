using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// слот для коннектора
public class ModuleSlot : MonoBehaviour, ILMButton
{
    public int SlotID;
    public GameObject WiresManager;

    bool pressed = false;
    public bool Pressed
    {
        get { return pressed; }
        set { pressed = value; }
    }

    public void LMBInteraction()
    {
        if(!pressed)
        {
            if(SlotID == 4 || SlotID == 8)
            {
                pressed = true;
                SetWire(0f);
                return;
            }
            if(SlotID == 3 || SlotID == 7)
            {
                pressed = true;
                SetWire(180f);
                return;
            }
            if(SlotID == 11)
            {
                pressed = true;
                SetWire(270f);
                return;
            }
            if(SlotID % 2 == 0)
            {
                pressed = true;
                SetWire(180f);
            }
            else
            {
                pressed = true;
                SetWire(0f);
            }
        }
    }

    void SetWire(float angle)
    {
        var outline = GetComponentInChildren<Outline>();
        outline.StartOutline();
            WiresManager.GetComponent<ModulesWireManager>().SetWire(
                new Vector3(transform.localPosition.x, 
                    transform.localPosition.y, transform.localPosition.z),
                new Vector3(transform.position.x, 
                    transform.position.y - gameObject.GetComponent<Collider>().bounds.size.y / 2,
                    transform.position.z),
                Quaternion.Euler((transform.localEulerAngles.x + 90f), transform.localEulerAngles.y, 0f),
                Quaternion.Euler(0f, angle, 0f),
                transform.parent,
                SlotID,
                outline,
                gameObject);
    }
}