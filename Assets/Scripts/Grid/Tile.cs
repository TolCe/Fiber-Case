using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileTrigger _trigger;

    public int[] Coordinates { get; private set; }

    private Stack _attachedStack;

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
        _attachedStack = stack;
    }

    public bool IsFree()
    {
        return _attachedStack == null;
    }
}