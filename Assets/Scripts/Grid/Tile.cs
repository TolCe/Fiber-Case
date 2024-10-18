using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileTrigger _trigger;

    public int[] Coordinates { get; private set; }

    public Stack AttachedStack { get; private set; }

    public void Initialize(int[] coord)
    {
        Coordinates = coord;

        _trigger.Initialize(this);

        gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void AttachStack(Stack stack)
    {
        AttachedStack = stack;
    }

    public void ResetStack()
    {
        AttachedStack = null;
    }

    public bool IsFree()
    {
        return AttachedStack == null;
    }
}