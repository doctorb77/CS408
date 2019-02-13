using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class gameBoard : MonoBehaviour
{
    public static gameBoard Instance { set; get; }

    public string[,] terrainTileInstanceTypes = new string[10, 10];
    public string[,] unitTileInstanceTypes = new string[10, 10];
    public GameObject[,] unitTileInstances = new GameObject[10, 10];
    public Vector3 pz = new Vector3(-5.0f, 0.0f, 0.0f);

    public GameObject base1;
    public GameObject base2;
    public GameObject terrain1;
    public GameObject terrain2;
    public GameObject terrain3;
    public GameObject melee1;
    public GameObject melee2;

    public GameObject selectedUnit;

    public GameObject spawnMenu;

    private Vector2 lastClicked;
    private Vector2 baseLocation1;
    private Vector2 baseLocation2;

    private bool isPlayerOneTurn;

    public int player1Funds, player2Funds;
    public Text fundsTextBox;
    public GameObject showPlayer1Turn;
    public GameObject showPlayer2Turn;

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

        string[,] terrainTileInstanceTypesTest = new string[10, 10];
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                terrainTileInstanceTypesTest[i, j] = "empty";

        terrainTileInstanceTypesTest[1, 1] = "terrain1";
        terrainTileInstanceTypesTest[2, 2] = "terrain2";
        terrainTileInstanceTypesTest[3, 3] = "terrain3";
        terrainTileInstanceTypesTest[4, 4] = "base1";
        terrainTileInstanceTypesTest[6, 4] = "base2";
        initializeTerrain(terrainTileInstanceTypesTest);

        //initialize turn state
        isPlayerOneTurn = (UnityEngine.Random.value > 0.5f);
        showPlayer1Turn.SetActive(false);
        showPlayer2Turn.SetActive(false);
        endTurn();

        //initialize menus
        spawnMenu.SetActive(false);

        //initialize default values
        player1Funds = player2Funds = 5000;
    }

    public void endTurn()
    {
        isPlayerOneTurn = !isPlayerOneTurn;

        if (isPlayerOneTurn)
            StartCoroutine(displayPlayer1Turn());
        else
            StartCoroutine(displayPlayer2Turn());
    }

    private IEnumerator displayPlayer1Turn()
    {
        showPlayer1Turn.SetActive(true);
        yield return new WaitForSeconds(3);
        showPlayer1Turn.SetActive(false);
    }

    private IEnumerator displayPlayer2Turn()
    {
        showPlayer2Turn.SetActive(true);
        yield return new WaitForSeconds(3);
        showPlayer2Turn.SetActive(false);
    }

    public void displaySpawnMenu()
    {
        spawnMenu.SetActive(!spawnMenu.activeInHierarchy);

        if (isPlayerOneTurn)
            fundsTextBox.text = player1Funds.ToString();
        else
            fundsTextBox.text = player2Funds.ToString();

    }

    public void spawnMeleeUnit()
    {
        if (isPlayerOneTurn && unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y] == null && player1Funds - 1000 >= 0)
        {
            GameObject unitInstance = Instantiate(melee1) as GameObject;
            unitInstance.transform.position = new Vector3(baseLocation1.x, baseLocation1.y, -2);
            unitInstance.GetComponent<unit>().health = 100;
            unitTileInstanceTypes[(int)baseLocation1.x, (int)baseLocation1.y] = "melee1";
            unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y] = unitInstance;
            player1Funds -= 1000;
        } else if (!isPlayerOneTurn && unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y] == null && player2Funds - 1000 >= 0) {
            GameObject unitInstance = Instantiate(melee2) as GameObject;
            unitInstance.transform.position = new Vector3(baseLocation2.x, baseLocation2.y, -2);
            unitInstance.GetComponent<unit>().health = 100;
            unitTileInstanceTypes[(int)baseLocation2.x, (int)baseLocation2.y] = "melee2";
            unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y] = unitInstance;
            player2Funds -= 1000;
        }

        displaySpawnMenu();
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
                        unitTileInstanceTypes[currMouseX, currMouseY] = unitTileInstanceTypes[(int)lastClicked.x, (int)lastClicked.y];
                        unitTileInstanceTypes[(int)lastClicked.x, (int)lastClicked.y] = "empty";
                        unitTileInstances[(int)lastClicked.x, (int)lastClicked.y] = null;
                        
                        unitTileInstances[currMouseX, currMouseY] = selectedUnit;
                        selectedUnit = null;
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));
                        return;
                    }
                }

                if (unitTileInstanceTypes[currMouseX, currMouseY] == "melee1" && isPlayerOneTurn)
                {
                    selectedUnit = unitTileInstances[currMouseX, currMouseY];
                    lastClicked.x = Mathf.Round((hit.point.x));
                    lastClicked.y = Mathf.Round((hit.point.y));
                    return;
                } else if (unitTileInstanceTypes[currMouseX, currMouseY] == "melee2" && !isPlayerOneTurn) {
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
                    baseLocation1.Set(i, j);
                }
                else if (terrainInput[i, j] == "base2") {
                    GameObject terrainTile = Instantiate(base2) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                    baseLocation2.Set(i, j);
                }

            }
        }
    }

}
