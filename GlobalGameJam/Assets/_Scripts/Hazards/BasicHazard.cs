using UnityEngine;

public class BasicHazard : MonoBehaviour, IPopper
{
    public void Pop()
    {
        Debug.Log("POP BUBBLE");
    }
}
