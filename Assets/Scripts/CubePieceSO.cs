using UnityEngine;

[CreateAssetMenu(fileName = "CubePieceSO", menuName = "Scriptable Objects/CubePieceSO")]
public class CubePieceSO : ScriptableObject
{

}



public struct Face
{
    public CubeSide side;
    public Vector3Int position;
    public Vector3Int Direction => CubeDirections.GetDir(side);

    public Face(CubeSide side, Vector3Int position)
    {
        this.side = side;
        this.position = position;
    }

    public override string ToString()
    {
        return $"{side} at {position}";
    }
}
