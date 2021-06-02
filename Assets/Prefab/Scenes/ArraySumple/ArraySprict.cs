using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArraySprict : MonoBehaviour
{
    GameObject[,] cubeArray;

    [SerializeField] int hArray;
    [SerializeField] int vArray;

    int selectNumX = 0;
    int selectNumY = 0;

    void Start()
    {
        cubeArray = new GameObject[vArray,hArray];

        for (int i = 0; i < hArray; i++)
        {
            for (int j = 0; j < vArray; j++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(-4 + 2 * i, -4 + 2 * j, 0);

                cubeArray[j, i] = cube;
            }
        }
    }

    void Update()
    {
        ColerSelect();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyObjct();
        }
        Move();
    }

    void ColerSelect()
    {
        for (int i = 0; i < vArray; i++)
        {
            for (int j = 0; j < hArray; j++)
            {
                Renderer renderer = cubeArray[j, i].GetComponent<Renderer>();
                renderer.material.color = (i == selectNumX && j == selectNumY ? Color.red : Color.white);
            }
        }
    }

    void DestroyObjct()
    {
        Destroy(cubeArray[selectNumY,selectNumX]);

        for (int i = selectNumY; i < cubeArray.GetLength(i); i++)
        {
            for (int j = selectNumX; j < cubeArray.GetLength(j); j++)
            {
                if (i + 1 < cubeArray.GetLength(i) && j + 1 < cubeArray.GetLength(j))
                {
                    cubeArray[j, i] = cubeArray[j + 1, i + 1];
                }
            }
        }

        //Array.Resize();
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectNumX++;
            if (selectNumX >= hArray)
            {
                selectNumX--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectNumX--;
            if (selectNumX < 0)
            {
                selectNumX++;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectNumY++;
            if (selectNumY >= vArray)
            {
                selectNumY--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectNumY--;
            if (selectNumY < 0)
            {
                selectNumY++;
            }
        }
    }
}
