using UnityEngine;
using System.Collections.Generic;

public class CubeCreator : MonoBehaviour
{

    [SerializeField] private int cubeSides = 3;

    [SerializeField] private float cubeSize = 1f;

    [SerializeField] private Vector3 colorCubeScale;

    [SerializeField] private Material cubeMaterial;

    [SerializeField]
    private Color top, bottom, left, right, front, back;

    List<GameObject> cubes = new List<GameObject>();
    List<GameObject> colorCubes = new List<GameObject>();


    /*
    public struct edge {         
        public int position;
        public int orientation;

        public edge(int position, int orientation)
        {
            this.position = position;
            this.orientation = orientation;
        }
    }*/



    Dictionary<Vector3, GameObject> edges = new Dictionary<Vector3, GameObject>();
    Dictionary<Vector3, GameObject> corners = new Dictionary<Vector3, GameObject>();


    //ulong edges = (ulong)0b0000_0000_0000_0000_1011_1010_1001_1000_0111_0110_0101_0100_0011_0010_0001_0000;
    //const ulong solvedEdges = 0b0000_0000_0000_0000_1011_1010_1001_1000_0111_0110_0101_0100_0011_0010_0001_0000;


    GameObject bottomMiddle, topMiddle, leftMiddle, rightMiddle, frontMiddle, backMiddle;

    private void OnValidate()
    {
        if(colorCubes.Count > 0)
        {
            foreach(GameObject colorCube in colorCubes)
            {
                colorCube.transform.localScale = colorCubeScale;
            }
        }
    }

    private void Start()
    {

        for (int x = 0; x < cubeSides; x++)
        {
            for (int y = 0; y < cubeSides; y++)
            {
                for (int z = 0; z < cubeSides; z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * cubeSize, y * cubeSize, z * cubeSize);
                    cube.GetComponent<Renderer>().material = cubeMaterial;

                    cubes.Add(cube);


                    bool isnotVisible = x > 0 && x < cubeSides - 1 && y > 0 && y < cubeSides - 1 && z > 0 && z < cubeSides - 1;

                    if (isnotVisible)
                    {
                        continue;
                    }

                    if((x == 0 || x == cubeSides - 1) && (y == 0 || y == cubeSides - 1) && (z == 0 || z == cubeSides - 1))
                    {
                        //this is a corner piece
                        corners.Add(new Vector3(x,y,z), cube);
                    }
                    else if((x == 0 || x == cubeSides - 1) && (y == 0 || y == cubeSides - 1) || (x == 0 || x == cubeSides - 1) && (z == 0 || z == cubeSides - 1) || (y == 0 || y == cubeSides - 1) && (z == 0 || z == cubeSides - 1))
                    {
                        //this is an edge piece
                        edges.Add(new Vector3(x,y,z), cube);
                    }


                    if(x == 0)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position - Vector3.right * cube.transform.localScale.x * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = left;

                        colorCubes.Add(colorCube);

                        OrientateColorLabel(-Vector3.right, colorCube.transform.up, colorCube);

                        if(cubeSides % 2 == 1 && y == cubeSides / 2 && z == cubeSides / 2)
                        {
                            //this is the center cube of this side
                            leftMiddle = cube;
                        }

                    }

                    if (x == cubeSides - 1)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position + Vector3.right * cube.transform.localScale.x * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = right;
                        colorCubes.Add(colorCube);

                        OrientateColorLabel(Vector3.right, colorCube.transform.up, colorCube);

                        if(cubeSides % 2 == 1 && y == cubeSides / 2 && z == cubeSides / 2)
                        {
                            rightMiddle = cube;
                        }
                    }

                    if (y == 0)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position - Vector3.up * cube.transform.localScale.y * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = bottom;

                        colorCubes.Add(colorCube);

                        OrientateColorLabel(-Vector3.up, colorCube.transform.up, colorCube);

                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && z == cubeSides / 2)
                        {
                            bottomMiddle = cube;
                        }
                    
                    }

                    if (y == cubeSides - 1)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position + Vector3.up * cube.transform.localScale.y * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = top;

                        colorCubes.Add(colorCube);

                        OrientateColorLabel(Vector3.up, colorCube.transform.up, colorCube);

                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && z == cubeSides / 2)
                        {
                            topMiddle = cube;
                        }
                    }

                    if (z == 0)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position - Vector3.forward * cube.transform.localScale.z * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = front;

                        colorCubes.Add(colorCube);
                        OrientateColorLabel(-Vector3.forward, colorCube.transform.up, colorCube);

                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && y == cubeSides / 2)
                        {
                            frontMiddle = cube;
                        }
                    }


                    if (z == cubeSides - 1)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position + Vector3.forward * cube.transform.localScale.z * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = back;

                        colorCubes.Add(colorCube);

                        OrientateColorLabel(Vector3.forward, colorCube.transform.up, colorCube);

                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && y == cubeSides / 2)
                        {
                            backMiddle = cube;
                        }
                    }

                }
            }
        }


        foreach(GameObject colorCube in colorCubes)
        {
            colorCube.transform.localScale = colorCubeScale;
        }
    }


    private void OrientateColorLabel(Vector3 direction, Vector3 upDirection, GameObject colorCube)
    {
        if(Vector3.Dot(direction,Vector3.up) < 0.9f)
        {
            Vector3 cross = Vector3.Cross(direction, upDirection);

            colorCube.transform.Rotate(cross,90);
        }
    }


    public void Rotate(string rotationType)
    {
        switch (rotationType)
        {
            case "R":


                TurnR();
               //edges = (edges & (0b1111 << 4)) << (4*4) | (edges & (0b1111 << (5*4))) >> (4*4) | (edges & (0b1111 << 5 * 4) >> 4*4 ) | (edges & (0b1111 << (8* 4)) >> 4*4);


                break;
            case "U":
                TurnU();
                break;
            default: break;
        }
    }


    private void TurnR()
    {
        List<GameObject> list = new List<GameObject>();

        Dictionary<Vector3, GameObject> newEdges = new Dictionary<Vector3, GameObject>();
        Dictionary<Vector3, GameObject> newCorners = new Dictionary<Vector3, GameObject>();

        newEdges = new Dictionary<Vector3, GameObject>(edges);
        newCorners = new Dictionary<Vector3, GameObject>(corners);

        foreach (var cube in newEdges)
        {
            if (cube.Key.x == cubeSides - 1)
            {
                cube.Value.transform.parent = rightMiddle.transform;
                Vector3 newKey = cube.Key;

                if (cube.Key.y == cubeSides - 1)
                {
                     newKey = new Vector3(cube.Key.x, cube.Key.y - 1, cube.Key.z + 1);
                }
                else
                {
                    if (cube.Key.z == cubeSides - 1)
                    {
                        newKey = new Vector3(cube.Key.x, cube.Key.y - 1, cube.Key.z - 1);
                    }
                    else
                    {
                        if(cube.Key.y == 0)
                        {
                            newKey = new Vector3(cube.Key.x, cube.Key.y + 1, cube.Key.z - 1);
                        }
                        else
                        {
                            newKey = new Vector3(cube.Key.x, cube.Key.y + 1, cube.Key.z + 1);
                        }
                    }
                }

                edges[newKey] = cube.Value;

                list.Add(cube.Value);
            }
        }

        foreach (var cube in newCorners)
        {
            if (cube.Key.x == cubeSides - 1)
            {
                cube.Value.transform.parent = rightMiddle.transform;
                list.Add(cube.Value);
                Vector3 newKey = cube.Key;

                if (cube.Key.y == cubeSides - 1)
                {
                    if (cube.Key.z == cubeSides - 1)
                    {
                        newKey = new Vector3(cube.Key.x, cube.Key.y - 2, cube.Key.z);
                    }
                    else
                    {
                        newKey = new Vector3(cube.Key.x, cube.Key.y, cube.Key.z + 2);
                    }
                }
                else
                {
                    if (cube.Key.z == cubeSides - 1)
                    {
                        if(cube.Key.y == 0)
                        {
                            newKey = new Vector3(cube.Key.x, cube.Key.y, cube.Key.z - 2);
                        }
                        else
                        {

                           newKey = new Vector3(cube.Key.x, cube.Key.y - 2, cube.Key.z);
                        }
                    }
                    else
                    {
                        newKey = new Vector3(cube.Key.x, cube.Key.y + 2, cube.Key.z);
                    }
                }

                corners[newKey] = cube.Value;

            }
        }

        rightMiddle.transform.Rotate(Vector3.right, 90);

        foreach (var cube in list)
        {
            cube.transform.parent = null;
        }

    }

    private void TurnU()
    {
        List<GameObject> list = new List<GameObject>();

        Dictionary<Vector3, GameObject> newEdges = new Dictionary<Vector3, GameObject>();
        Dictionary<Vector3, GameObject> newCorners = new Dictionary<Vector3, GameObject>();

        newEdges = new Dictionary<Vector3, GameObject>(edges);
        newCorners = new Dictionary<Vector3, GameObject>(corners);

        foreach (var cube in newEdges)
        {
            if (cube.Key.y == cubeSides - 1)
            {

                Vector3 newKey = cube.Key;
                cube.Value.transform.parent = topMiddle.transform;

                if (cube.Key.x == cubeSides - 1)
                {
                    newKey = new Vector3(cube.Key.x - 1, cube.Key.y, cube.Key.z - 1);
                }
                else
                {
                    if (cube.Key.z == cubeSides - 1)
                    {
                        newKey = new Vector3(cube.Key.x + 1, cube.Key.y, cube.Key.z - 1);
                    }
                    else
                    {
                        if(cube.Key.x == 0)
                        {
                            newKey = new Vector3(cube.Key.x + 1, cube.Key.y, cube.Key.z + 1);
                        }
                        else
                        {
                            newKey = new Vector3(cube.Key.x - 1, cube.Key.y, cube.Key.z + 1);
                        }
                    }
                }

                edges[newKey] = cube.Value;

                list.Add(cube.Value);

            }
        }

        foreach (var cube in newCorners)
        {
            if (cube.Key.y == cubeSides - 1)
            {
                cube.Value.transform.parent = topMiddle.transform;
                list.Add(cube.Value);

                Vector3 newKey = cube.Key;

                if (cube.Key.x == cubeSides - 1)
                {
                    if (cube.Key.z == cubeSides - 1)
                    {
                        newKey = new Vector3(cube.Key.x, cube.Key.y, cube.Key.z - 2);
                    }
                    else
                    {
                        newKey = new Vector3(cube.Key.x - 2, cube.Key.y, cube.Key.z);
                    }
                }
                else
                {
                    if (cube.Key.z == cubeSides - 1)
                    {
                        if (cube.Key.x == 0)
                        {
                            newKey = new Vector3(cube.Key.x + 2, cube.Key.y, cube.Key.z);
                        }
                        else
                        {

                            newKey = new Vector3(cube.Key.x, cube.Key.y, cube.Key.z - 2);
                        }
                    }
                    else
                    {
                        newKey = new Vector3(cube.Key.x, cube.Key.y, cube.Key.z + 2);
                    }
                }

                corners[newKey] = cube.Value;

            }
        }

        topMiddle.transform.Rotate(Vector3.up, 90);

        foreach (var cube in list)
        {
            cube.transform.parent = null;
        }

    }
}
