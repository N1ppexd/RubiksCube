using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private CubeCreator cubeCreator;


    public void Rotate(string rotationType)
    {
        cubeCreator.Rotate(rotationType);
    }
}
