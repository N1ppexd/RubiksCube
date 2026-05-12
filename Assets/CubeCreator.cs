using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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



    //when true, rotation is disabled
    bool isRotating;

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


    //pivot is used for rotating, center is the middle of the entire cube
    //and the parent of every piece
    //cube parent is just for rotating it around in the view, it has no other function
    GameObject cubeParent, pivot, center;

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



        //if cubeSides is even, we spawn a empty object that works as a pivot for the rotation of the middle pieces,
        //and we spawn the middle pieces at the position of the pivot, so they are correctly rotated when rotating the pivot
        bool isEven = cubeSides % 2 == 0;



        cubeParent = new GameObject("Cube Parent");
        cubeParent.transform.position = new Vector3((cubeSides - 1) * cubeSize * 0.5f, (cubeSides - 1) * cubeSize * 0.5f, (cubeSides - 1) * cubeSize * 0.5f);
        center = new GameObject("Center");
        center.transform.parent = cubeParent.transform;
        center.transform.position = cubeParent.transform.position;
        pivot = new GameObject("Pivot");
        pivot.transform.parent = center.transform;
        pivot.transform.position = center.transform.position;


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

                    cube.transform.parent = center.transform;


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
                    else if((x == 0 || x == cubeSides - 1) && (y == 0 || y == cubeSides - 1) || (x == 0 || x == cubeSides - 1) 
                        && (z == 0 || z == cubeSides - 1) || (y == 0 || y == cubeSides - 1) && (z == 0 || z == cubeSides - 1))
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

                        /*
                        if(cubeSides % 2 == 1 && y == cubeSides / 2 && z == cubeSides / 2)
                        {
                            //this is the center cube of this side
                            leftMiddle = cube;
                        }*/
                    }

                    if (x == cubeSides - 1)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position + Vector3.right * cube.transform.localScale.x * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = right;
                        colorCubes.Add(colorCube);

                        OrientateColorLabel(Vector3.right, colorCube.transform.up, colorCube);

                        /*
                        if(cubeSides % 2 == 1 && y == cubeSides / 2 && z == cubeSides / 2)
                        {
                            rightMiddle = cube;
                        }*/
                    }

                    if (y == 0)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position - Vector3.up * cube.transform.localScale.y * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = bottom;

                        colorCubes.Add(colorCube);

                        OrientateColorLabel(-Vector3.up, colorCube.transform.up, colorCube);

                        /*
                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && z == cubeSides / 2)
                        {
                            bottomMiddle = cube;
                        }*/
                    
                    }

                    if (y == cubeSides - 1)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position + Vector3.up * cube.transform.localScale.y * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = top;

                        colorCubes.Add(colorCube);

                        OrientateColorLabel(Vector3.up, colorCube.transform.up, colorCube);

                        /*
                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && z == cubeSides / 2)
                        {
                            topMiddle = cube;
                        }*/
                    }

                    if (z == 0)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position - Vector3.forward * cube.transform.localScale.z * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = front;

                        colorCubes.Add(colorCube);
                        OrientateColorLabel(-Vector3.forward, colorCube.transform.up, colorCube);

                        /*
                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && y == cubeSides / 2)
                        {
                            frontMiddle = cube;
                        }*/
                    }


                    if (z == cubeSides - 1)
                    {
                        GameObject colorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        colorCube.transform.position = cube.transform.position + Vector3.forward * cube.transform.localScale.z * 0.5f;

                        colorCube.transform.parent = cube.transform;
                        colorCube.GetComponent<Renderer>().material.color = back;

                        colorCubes.Add(colorCube);

                        OrientateColorLabel(Vector3.forward, colorCube.transform.up, colorCube);
                        /*
                        if(cubeSides % 2 == 1 && x == cubeSides / 2 && y == cubeSides / 2)
                        {
                            backMiddle = cube;
                        }*/
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
            if(Vector3.Dot(direction, Vector3.up) < -0.9f)
            {
                colorCube.transform.Rotate(Vector3.right, 180);
            }
            else
            {
                Vector3 cross = Vector3.Cross(upDirection, direction);

                colorCube.transform.Rotate(cross, 90);
            }
        }
    }



    public void Rotate(string rotation)
    {
        bool clockwise = !rotation.Contains("'");
        rotation = rotation.Replace("'","");

        Vector3 axis = rotation switch
        {
            "U" => cubeParent.transform.up,
            "D" => -cubeParent.transform.up,
            "R" => cubeParent.transform.right,
            "L" => -cubeParent.transform.right,
            "F" => -cubeParent.transform.forward,
            "B" => cubeParent.transform.forward,
            "x" => cubeParent.transform.right,
            "y" => cubeParent.transform.up,
            "z" => -cubeParent.transform.forward,
            _ => Vector3.zero
        };

        bool wholeCube = rotation switch
        {
            "x" => true,
            "y" => true,
            "z" => true,
            _ => false
        };
            
        if(axis == Vector3.zero) { return; }

        //dont rotate if is rotating is true
        if(isRotating) 
        {
            rotationQueue.Add((axis, clockwise, wholeCube));
        }
        else
        {
            StartCoroutine(Rotate(axis, clockwise, wholeCube, 0.5f));
        }
    }



    //make a queue of rotations to rotate multiple times in a row, and make the rotations wait for the previous one to finish before starting the next one
    private List<(Vector3 axis, bool clockwise, bool wholeCube)> rotationQueue = new List<(Vector3 axis, bool clockwise, bool wholeCube)>();


    IEnumerator Rotate( Vector3 axis, bool clockwise, bool wholeCube, float duration = 0.5f)
    {
        isRotating = true;

        GameObject middleObj = pivot;

        if (!wholeCube)
        {
            List<GameObject> rotateCubes = new List<GameObject>();
            foreach (GameObject cube in colorCubes)
            {
                if (Vector3.Dot(cube.transform.up, axis) > 0.9f)
                {
                    rotateCubes.Add(cube.transform.parent.gameObject);
                }
            }

            foreach (GameObject cube in rotateCubes)
            {
                if (cube != middleObj)
                {
                    cube.transform.parent = middleObj.transform;
                }
            }
        }
        else
        {
            middleObj = center;
        }



        float elapsed = 0f;
        Quaternion startRotation = middleObj.transform.localRotation;
        Quaternion endRotation = startRotation * Quaternion.AngleAxis(clockwise ? 90 : -90, middleObj.transform.worldToLocalMatrix * axis);

        while (elapsed < duration)
        {
            middleObj.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        middleObj.transform.localRotation = endRotation;

        if (!wholeCube)
        {
            foreach (GameObject cube in cubes)
            {
                cube.transform.parent = center.transform;
            }

            middleObj.transform.localRotation = Quaternion.identity;
        }

        if(rotationQueue.Count > 0)
        {
            var nextRotation = rotationQueue[0];
            rotationQueue.RemoveAt(0);
            StartCoroutine(Rotate(nextRotation.axis, nextRotation.clockwise, nextRotation.wholeCube, duration));
        }
        else
        {
            isRotating = false;
        }
    }

}
