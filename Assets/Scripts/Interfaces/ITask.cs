using UnityEngine;

//интерфейс тасков
public interface ITask
{
    public bool isResolved { get; set; }
    public GameObject NextButton { get; set; }
    
    public abstract bool TaskCondition(ProgressManager obj);
    public abstract void ResetTask();
}
