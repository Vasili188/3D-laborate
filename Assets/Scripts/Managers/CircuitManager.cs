using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

// логика проверки собранной цепи
public class CircuitManager : MonoBehaviour
{
    // массив возможных связей
    [SerializeField] List<string> relations;
    // массив заменяющих друг друга связи
    [SerializeField] List<EqualRelations> EqualRelations; // в формате массив массивом "одинаковых" элементов [[1-5,1-7,7-5],[аналогично]]
    [SerializeField] GameObject CalcManager;
    [SerializeField] GameObject AngleReohord;
    [SerializeField] GameObject AngleMagazin;
    [SerializeField] GameObject Switch;
    
    public bool LogicFlag = false;
    // словарь связей, [связь]: true, [связь]: false и тд
    public Dictionary<string, bool> relationsDictionary;
    public List<GameObject> connectors;
    public List<ModuleSlot> slots;
    public List<ModulesWireManager> used;


    void Start()
    {
        CreateRelationsDictionary();
    }

    
    public bool CheckRelation(int startID, int endID) // проверка наличия текущей связи
    {
        var relation = $"{startID}-{endID}";
        var reversedRelation = $"{endID}-{startID}";

        foreach(var e in relationsDictionary)
        {
            if(e.Key == relation || e.Key == reversedRelation)
            {
                if(relationsDictionary[e.Key])
                    return false;
                relationsDictionary[e.Key] = true;
                return true;
            }
        }

        return false;
    }

    void Update() // проверка заменяемых связей
    {
        var countFlag = 0;

        foreach(var e in EqualRelations)
        {
            var count = e.relations.Count == 6 ? 3 : 2;

            foreach(var x in e.relations)
            {
                if(relationsDictionary[x])
                {
                    countFlag++;
                }

                if(countFlag == count)
                {
                    foreach(var n in e.relations)
                    {
                        relationsDictionary[n] = true;
                    }
                }
            }
            countFlag = 0;
        }
    }

    public void DestroyWires() // выполняется при нажатии кнопки сброса
    {
        foreach(var e in used)
        {
            e.DoSlotsUnUsed();
        }

        foreach(var e in slots)
        {
            e.Pressed = false;
        }

        foreach(var e in connectors)
        {
            Destroy(e);
        }

        CreateRelationsDictionary();

        connectors.Clear();
        
        slots.Clear();

        CalcManager.GetComponent<PhysCalculation>().ResetCalculations();

        Switch.GetComponent<SwitchButton>().Reset();

        //PlayerPrefs.DeleteAll();

        AngleReohord.GetComponent<TextMeshPro>().text = "135";

        AngleMagazin.GetComponent<MagazinButton>().Reset();

        FindObjectOfType<ReohordButton>().txt = 135;
    }

    void CreateRelationsDictionary()
    {
        relationsDictionary = new Dictionary<string, bool>();

        foreach(var e in relations)
        {
            relationsDictionary.Add(e, false);
        }
    }
}

[System.Serializable]
public class EqualRelations
{
    public List<string> relations;
}