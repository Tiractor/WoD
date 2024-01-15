using UnityEngine;

public class Last : TaskExecutor<Last>
{
    public void SetLast()
    {
        transform.SetAsLastSibling();
    }
    private void Awake()
    {
        Denote();
    }
}