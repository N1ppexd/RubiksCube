using Unity.Mathematics;
using UnityEngine;

public static class CubeDirections
{
    
    public static Vector3Int GetDir(CubeSide side)
    {
        return side switch
        {
            CubeSide.F => new Vector3Int(0, 0, 1),
            CubeSide.B => new Vector3Int(0, 0, -1),
            CubeSide.U => new Vector3Int(0, 1, 0),
            CubeSide.D => new Vector3Int(0, -1, 0),
            CubeSide.L => new Vector3Int(-1, 0, 0),
            CubeSide.R => new Vector3Int(1, 0, 0),
            _ => throw new System.ArgumentOutOfRangeException(nameof(side), side, null)
        };
    }

}
