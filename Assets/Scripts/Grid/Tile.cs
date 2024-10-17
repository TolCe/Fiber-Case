using UnityEngine;

public class Tile : MonoBehaviour
{
    public int[] Coordinates { get; private set; }

    public void Initialize(int[] coord)
    {
        Coordinates = coord;

        gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}