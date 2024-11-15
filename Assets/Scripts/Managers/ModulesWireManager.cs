using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// соединение коннекторов проводами, логика проводов
public class ModulesWireManager : MonoBehaviour
{
    // материал провода
    [SerializeField] Material material;
    [SerializeField] GameObject CircuitManager;
    public GameObject connectorPrefab;
    bool startUsed = false;
    bool endUsed = false;
    Transform End;
    Transform Start;
    LineRenderer lr;
    Vector3[] points;
    int PointsCount = 30;
    GameObject connector;
    GameObject startConnector;
    Outline startOutline;
    int startID;
    GameObject startSlot;

    // логика установки коннекторов
    public void SetWire(Vector3 ampPos, Vector3 pos, Quaternion ampAngle, Quaternion angle, Transform parent, 
        int SlotID, Outline outline, GameObject slot)
    {
        Transform conTransform;

        if(SlotID == 1 || SlotID == 2 || SlotID == 5 || SlotID == 6 || SlotID == 9 || SlotID == 10)
        {
            connector = Instantiate(connectorPrefab, parent);
            conTransform = connector.transform;

            conTransform.localRotation = ampAngle;
            conTransform.localPosition = ampPos;
        }
        else
        {
            connector = Instantiate(connectorPrefab, pos, angle, parent);
            conTransform = connector.transform;
        }

        connector.SetActive(false);

        if(parent.name != "WireConnector(Clone)")
        {
            var parentScale = parent.localScale;
            conTransform.localScale = new Vector3(1000 / parentScale.x, 1000 / parentScale.y, 1000 / parentScale.z);
        }
        
        var connectorSlot = conTransform.Find("Slot").GetComponent<ModuleSlot>();
        connectorSlot.WiresManager = gameObject;
        connectorSlot.SlotID = SlotID;
        
        var connectSlot = conTransform.Find("WireSlot").gameObject;

        if(!startUsed)
        {
            startID = SlotID;
            startConnector = connector;
            startOutline = outline;
            startSlot = slot;
            Start = connectSlot.transform;
            startUsed = true;
            lr = connectSlot.GetComponent<LineRenderer>();
            return;
        }
        if(!endUsed)
        {
            End = connectSlot.transform;
            endUsed = true;
            outline.StopOutline();
        }

        startOutline.StopOutline();
        CheckRelationHelper(SlotID, slot, outline);

        startConnector.SetActive(true);
        connector.SetActive(true);

        var circuitMangerComponent = CircuitManager.GetComponent<CircuitManager>();
        circuitMangerComponent.connectors.Add(connector);
        circuitMangerComponent.connectors.Add(startConnector);
        circuitMangerComponent.slots.Add(startSlot.GetComponent<ModuleSlot>());
        circuitMangerComponent.slots.Add(slot.GetComponent<ModuleSlot>());
        circuitMangerComponent.used.Add(this);
    }

    void ConnectSlots() // соединение коннекторов
    {
        if (points == null || points.Length != PointsCount)
            points = new Vector3[PointsCount];
 
        Lerp(Start, End, PointsCount);
        lr.startWidth = 0.028f;
        lr.endWidth = 0.028f;
        lr.material = material;
        lr.positionCount = PointsCount;
        lr.SetPositions(points.ToArray());
    }
 
    private void Lerp(Transform Start, Transform end, int count) // заполнение массива точками, "координаты" всего провода в пространстве
    {
        var L = (Start.position - End.position);
        var D = L.magnitude + 0.001f;
        var P0 = Start.position;
        var P1 = Start.position + Start.forward * D/6;
        var P2 = End.position + End.forward * D/6;
        var P3 = End.position;
 
        for (int i = 0; i < count; i++)
        {
            var parameter = (float)i / (count - 1);   
            var P1234 = BezierPoint.GetBezierPoint(P0, P1, P2, P3, parameter);

            points[i] = P1234;
        }
        DoSlotsUnUsed();
    }

    private void CheckRelationHelper(int endID, GameObject endSlot, Outline endOutline) // проверка правильности связи
    {
        if(CircuitManager.GetComponent<CircuitManager>().CheckRelation(startID, 
            endID))
        {
            ConnectSlots();
        }
        else
        {
            startOutline.SetRedColor();
            endOutline.SetRedColor();
            DoSlotsUnUsed();
            startSlot.GetComponent<ModuleSlot>().Pressed = false;
            endSlot.GetComponent<ModuleSlot>().Pressed = false;
            Destroy(connector);
            Destroy(startConnector);
        }
    }

    public void DoSlotsUnUsed()
    {
        startUsed = false;
        endUsed = false;
    }
}
