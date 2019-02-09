using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameBoard : MonoBehaviour
{
    public static gameBoard Instance { set; get; }

    public string[,] terrainTileInstanceTypes = new string[10, 10];
    public string[,] unitTileInstanceTypes = new string[10, 10];
    public GameObject[,] unitTileInstances = new GameObject[10, 10];
    public Vector3 pz = new Vector3(-5.0f, 0.0f, 0.0f);

    public GameObject base1;
    public GameObject terrain1;
    public GameObject terrain2;
    public GameObject terrain3;
    public GameObject melee1;

    public GameObject selectedUnit;

    public GameObject test;

    private Vector2 lastClicked;


    private void Start()
    {
        Instance = this;

        selectedUnit = null;

        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                unitTileInstances[i, j] = null;

        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                unitTileInstanceTypes[i, j] = "empty";

        test = Instantiate(melee1) as GameObject;
        test.transform.position = new Vector3(0, 0, -2);
        unitTileInstances[0, 0] = test;
        unitTileInstanceTypes[0, 0] = "melee1";

        string[,] terrainTileInstanceTypesTest = new string[10, 10];
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                terrainTileInstanceTypesTest[i, j] = "empty";

        terrainTileInstanceTypesTest[1, 1] = "terrain1";
        terrainTileInstanceTypesTest[2, 2] = "terrain2";
        terrainTileInstanceTypesTest[3, 3] = "terrain3";
        terrainTileInstanceTypesTest[4, 4] = "base1";
        initializeTerrain(terrainTileInstanceTypesTest);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("BoardLayer")))
            {
                int currMouseX = (int)Mathf.Round((hit.point.x));
                int currMouseY = (int)Mathf.Round((hit.point.y));

                Debug.Log(lastClicked);

                if (unitTileInstanceTypes[currMouseX, currMouseY] == "empty")
                {
                    if (selectedUnit == null)
                        return;
                    else
                    {
                        selectedUnit.transform.position = new Vector3(currMouseX, currMouseY, -2);
                        unitTileInstanceTypes[(int)lastClicked.x, (int)lastClicked.y] = "empty";
                        unitTileInstances[(int)lastClicked.x, (int)lastClicked.y] = null;
                        unitTileInstanceTypes[currMouseX, currMouseY] = "melee1";
                        unitTileInstances[currMouseX, currMouseY] = selectedUnit;
                        selectedUnit = null;
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));
                        return;
                    }
                }

                if (unitTileInstanceTypes[currMouseX, currMouseY] == "melee1")
                {
                    selectedUnit = unitTileInstances[currMouseX, currMouseY];
                    lastClicked.x = Mathf.Round((hit.point.x));
                    lastClicked.y = Mathf.Round((hit.point.y));
                    return;
                }
            }
        }
    }

    private void initializeTerrain(string[,] terrainInput)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                this.terrainTileInstanceTypes[i, j] = terrainInput[i, j];

                if (terrainInput[i,j] == "terrain1")
                {
                    GameObject terrainTile = Instantiate(terrain1) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                } else if (terrainInput[i, j] == "terrain2") {
                    GameObject terrainTile = Instantiate(terrain2) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                }
                else if (terrainInput[i, j] == "terrain3") {
                    GameObject terrainTile = Instantiate(terrain3) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                }
                else if (terrainInput[i, j] == "base1") {
                    GameObject terrainTile = Instantiate(base1) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                } 

            }
        }
    }

}
