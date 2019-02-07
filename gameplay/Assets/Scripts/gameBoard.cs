using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameBoard : MonoBehaviour
{
    public static gameBoard Instance { set; get; }

    //public terrainTiles[,] terrain = new terrainTiles[10, 10];
    public string[,] boardTileInstanceTypes = new string[10, 10];
    public GameObject[,] boardTileInstances = new GameObject[10, 10];
    public Vector3 pz = new Vector3(-5.0f, 0.0f, 0.0f);

    public GameObject terrain1Prefab;
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
                boardTileInstances[i, j] = null;

        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                boardTileInstanceTypes[i, j] = "empty";

        test = Instantiate(melee1) as GameObject;
        test.transform.position = new Vector3(0, 0, -1);
        boardTileInstances[0, 0] = test;
        boardTileInstanceTypes[0, 0] = "melee1";

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

                if (boardTileInstanceTypes[currMouseX, currMouseY] == "empty")
                {
                    if (selectedUnit == null)
                        return;
                    else
                    {
                        selectedUnit.transform.position = new Vector3(currMouseX, currMouseY, -1);
                        boardTileInstanceTypes[(int)lastClicked.x, (int)lastClicked.y] = "empty";
                        boardTileInstances[(int)lastClicked.x, (int)lastClicked.y] = null;
                        boardTileInstanceTypes[currMouseX, currMouseY] = "melee1";
                        boardTileInstances[currMouseX, currMouseY] = selectedUnit;
                        selectedUnit = null;
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));
                        return;
                    }
                }

                if (boardTileInstanceTypes[currMouseX, currMouseY] == "melee1")
                {
                    selectedUnit = boardTileInstances[currMouseX, currMouseY];
                    lastClicked.x = Mathf.Round((hit.point.x));
                    lastClicked.y = Mathf.Round((hit.point.y));
                    return;
                }
            }
        }
    }

}
