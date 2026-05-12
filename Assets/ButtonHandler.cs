using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private CubeCreator cubeCreator;


    public void Rotate(string rotationType)
    {


        Regex regex = new Regex("([a-zA-Z]'2)|([a-zA-Z](?!')2)|([a-zA-Z]')|([a-zA-Z](?!'))");

        if (rotationType.Contains("M"))
        {
            rotationType = rotationType.Replace("M", "L'Rx'").Replace("M'", "LR'x").Replace("M2", "L2R2x2");
        }
        if (rotationType.Contains("E")){

            rotationType = rotationType.Replace("E", "D'Uy'").Replace("E'", "DU'y").Replace("E2", "D2U2y2");
        }
        if(rotationType.Contains("S")){

            rotationType = rotationType.Replace("S", "F'Bz").Replace("S'", "FB'z'").Replace("S2", "F2B2z2");
        }

        string[] input = regex.Split(rotationType);


        foreach(string rotation in input)
        {
            

            if (rotation.Contains("2"))
            {
                string baseRotation = rotation.Replace("2", "");

                for (int i = 0; i < 2; i++)
                {
                    cubeCreator.Rotate(baseRotation);
                }

                continue;
            }

            cubeCreator.Rotate(rotation);
           
        }
        
    }
}
