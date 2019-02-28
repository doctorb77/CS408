﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class gameBoard : MonoBehaviour
{
    public static gameBoard Instance { set; get; }

    public string[,] terrainTileInstanceTypes = new string[10, 10];
    public GameObject[,] unitTileInstances = new GameObject[10, 10];

    public Vector2 terrainSize = new Vector2(10.0f, 10.0f);

    public GameObject base1;
    public GameObject base2;
    public GameObject terrain1;
    public GameObject terrain2;
    public GameObject terrain3;
    public GameObject terrain4;
    public GameObject melee1;
    public GameObject melee2;
    public GameObject ranged1;
    public GameObject ranged2;

    public GameObject selectedUnit;
    public GameObject lightBlueValidMoveTile;
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

        //load map
        loadMap1();

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

    public void loadMap1()
    {
        string[,] t = new string[10, 10];

        //t1 = regular tile
        //t2 = impassible tile
        //t3 = defensive tile
        //t4 = resource tile

        t[0, 9] = "t1"; t[1, 9] = "t1"; t[2, 9] = "t1"; t[3, 9] = "t1"; t[4, 9] = "t1"; t[5, 9] = "t3"; t[6, 9] = "t1"; t[7, 9] = "t1"; t[8, 9] = "t1"; t[9, 9] = "t1";
        t[0, 8] = "t1"; t[1, 8] = "t2"; t[2, 8] = "t1"; t[3, 8] = "t1"; t[4, 8] = "t1"; t[5, 8] = "t3"; t[6, 8] = "t1"; t[7, 8] = "t1"; t[8, 8] = "b2"; t[9, 8] = "t1";
        t[0, 7] = "t3"; t[1, 7] = "t3"; t[2, 7] = "t3"; t[3, 7] = "t1"; t[4, 7] = "t2"; t[5, 7] = "t2"; t[6, 7] = "t1"; t[7, 7] = "t1"; t[8, 7] = "t1"; t[9, 7] = "t1";
        t[0, 6] = "t1"; t[1, 6] = "t1"; t[2, 6] = "t1"; t[3, 6] = "t2"; t[4, 6] = "t2"; t[5, 6] = "t2"; t[6, 6] = "t2"; t[7, 6] = "t1"; t[8, 6] = "t4"; t[9, 6] = "t1";
        t[0, 5] = "t1"; t[1, 5] = "t4"; t[2, 5] = "t1"; t[3, 5] = "t2"; t[4, 5] = "t2"; t[5, 5] = "t2"; t[6, 5] = "t2"; t[7, 5] = "t3"; t[8, 5] = "t3"; t[9, 5] = "t3";
        t[0, 4] = "t3"; t[1, 4] = "t3"; t[2, 4] = "t3"; t[3, 4] = "t2"; t[4, 4] = "t2"; t[5, 4] = "t2"; t[6, 4] = "t2"; t[7, 4] = "t1"; t[8, 4] = "t4"; t[9, 4] = "t1";
        t[0, 3] = "t1"; t[1, 3] = "t4"; t[2, 3] = "t1"; t[3, 3] = "t2"; t[4, 3] = "t2"; t[5, 3] = "t2"; t[6, 3] = "t2"; t[7, 3] = "t1"; t[8, 3] = "t1"; t[9, 3] = "t1";
        t[0, 2] = "t1"; t[1, 2] = "t1"; t[2, 2] = "t1"; t[3, 2] = "t1"; t[4, 2] = "t2"; t[5, 2] = "t2"; t[6, 2] = "t1"; t[7, 2] = "t3"; t[8, 2] = "t3"; t[9, 2] = "t3";
        t[0, 1] = "t1"; t[1, 1] = "b1"; t[2, 1] = "t1"; t[3, 1] = "t1"; t[4, 1] = "t3"; t[5, 1] = "t1"; t[6, 1] = "t1"; t[7, 1] = "t1"; t[8, 1] = "t2"; t[9, 1] = "t1";
        t[0, 0] = "t1"; t[1, 0] = "t1"; t[2, 0] = "t1"; t[3, 0] = "t1"; t[4, 0] = "t3"; t[5, 0] = "t1"; t[6, 0] = "t1"; t[7, 0] = "t1"; t[8, 0] = "t1"; t[9, 0] = "t1";
    
        initializeTerrain(t);
    }

    public void endTurn()
    {
        
        isPlayerOneTurn = !isPlayerOneTurn;

        if (isPlayerOneTurn)
            StartCoroutine(displayPlayer1Turn());
        else
            StartCoroutine(displayPlayer2Turn());

        resetUnitProperties();
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

    public void spawnMeleeUnitTierOne()
    {
        spawnUnit("melee", 1);
    }

    public void spawnRangedUnitTierOne()
    {
        spawnUnit("ranged", 1);
    }

    public void spawnUnit(string type, int tier)
    {
        if (isPlayerOneTurn && unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y] == null && player1Funds - 1000 >= 0)
        {
            GameObject unitInstance = null;
            if (type == "ranged")
            {
                unitInstance = Instantiate(ranged1) as GameObject;
                unitInstance.GetComponent<unit>().maxMoveDistance = 2;
            }
            else
            {
                unitInstance = Instantiate(melee1) as GameObject;
                unitInstance.GetComponent<unit>().maxMoveDistance = 1;
            }
            unitInstance.transform.position = new Vector3(baseLocation1.x, baseLocation1.y, -2);
            unitInstance.GetComponent<unit>().health = 100;
            unitInstance.GetComponent<unit>().maxHealth = 100;
            unitInstance.GetComponent<unit>().defense = 1.0f;
            unitInstance.GetComponent<unit>().attack = 10;
            unitInstance.GetComponent<unit>().typeOfUnit = type;
            //unitInstance.GetComponent<unit>().maxMoveDistance = 1;
            unitInstance.GetComponent<unit>().isPlayerOneUnit = true;
            unitInstance.GetComponent<unit>().unitWasMoved = false;


            //unitInstance.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 1.0f);

            if (baseLocation1.x < (terrainSize.x / 2.0f)) {
                unitInstance.GetComponent<unit>().lastFacingRight = true;
                unitInstance.GetComponent<Animator>().SetTrigger("idleright");
            } else {
                unitInstance.GetComponent<unit>().lastFacingRight = false;
                unitInstance.GetComponent<Animator>().SetTrigger("idleleft");
            }
            unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y] = unitInstance;
            player1Funds -= 1000;
        } else if (!isPlayerOneTurn && unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y] == null && player2Funds - 1000 >= 0) {

            GameObject unitInstance = null;
            if (type == "ranged") { 
                unitInstance = Instantiate(ranged2) as GameObject;
                unitInstance.GetComponent<unit>().maxMoveDistance = 2;
            }
            else { 
                unitInstance = Instantiate(melee2) as GameObject;
                unitInstance.GetComponent<unit>().maxMoveDistance = 1;
            }
            unitInstance.transform.position = new Vector3(baseLocation2.x, baseLocation2.y, -2);
            unitInstance.GetComponent<unit>().health = 100;
            unitInstance.GetComponent<unit>().maxHealth = 100;
            unitInstance.GetComponent<unit>().defense = 1.0f;
            unitInstance.GetComponent<unit>().attack = 10;
            unitInstance.GetComponent<unit>().typeOfUnit = type;
            //unitInstance.GetComponent<unit>().maxMoveDistance = 1;
            unitInstance.GetComponent<unit>().isPlayerOneUnit = false;
            unitInstance.GetComponent<unit>().unitWasMoved = false;

            if (baseLocation2.x < (terrainSize.x / 2.0f)) {
                unitInstance.GetComponent<unit>().lastFacingRight = true;
                unitInstance.GetComponent<Animator>().SetTrigger("idleright");
            } else {
                unitInstance.GetComponent<unit>().lastFacingRight = false;
                unitInstance.GetComponent<Animator>().SetTrigger("idleleft");
            }
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

                //if spot is empty move selected unit there
                if (unitTileInstances[currMouseX, currMouseY] == null)
                {
                    if (selectedUnit == null)
                        return;
                    else if (Math.Abs(currMouseX - lastClicked.x) > unitTileInstances[(int)lastClicked.x, (int)lastClicked.y].GetComponent<unit>().maxMoveDistance ||
                        Math.Abs(currMouseY - lastClicked.y) > unitTileInstances[(int)lastClicked.x, (int)lastClicked.y].GetComponent<unit>().maxMoveDistance)
                    {
                        return;
                    } else
                    {
                        //selectedUnit.transform.position = new Vector3(currMouseX, currMouseY, -2);
                        Vector3 desiredPosition = new Vector3(currMouseX, currMouseY, -2);

                        //transform.position = Vector3.MoveTowards(selectedUnit.transform.position, desiredPosition, 1.0f * Time.deltaTime);
                        StartCoroutine(MovementCoroutine1(selectedUnit, desiredPosition));

                        unitTileInstances[(int)lastClicked.x, (int)lastClicked.y] = null;
                        
                        unitTileInstances[currMouseX, currMouseY] = selectedUnit;
                        //unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().unitWasMoved = true;
                        //unitTileInstances[currMouseX, currMouseY].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);

                        selectedUnit = null;
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));

                        eraseValidMoveTiles();

                        
                        return;
                    }
                }
                //if player 1 moving to attack player 2 unit
                else if (isPlayerOneTurn && !unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().isPlayerOneUnit 
                    && selectedUnit != null && selectedUnit.GetComponent<unit>().unitWasMoved == false) 
                {
                    StartCoroutine(CombatCoroutine(selectedUnit, unitTileInstances[currMouseX, currMouseY]));
                    selectedUnit.GetComponent<unit>().unitWasMoved = true;
                    selectedUnit = null;
                }
                //if player 2 moving to attack player 1 unit
                else if (!isPlayerOneTurn && unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().isPlayerOneUnit 
                    && selectedUnit != null && selectedUnit.GetComponent<unit>().unitWasMoved == false)
                {
                    selectedUnit.GetComponent<Animator>().SetTrigger("combatleft");
                    selectedUnit.GetComponent<unit>().unitWasMoved = true;
                    selectedUnit = null;
                }

                //select a unit
                if (unitTileInstances[currMouseX, currMouseY] != null)
                {

                    if (unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().unitWasMoved == true)
                        return;

                    if (unitTileInstances[currMouseX, currMouseY] != null && isPlayerOneTurn && unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().isPlayerOneUnit)
                    {
                        eraseValidMoveTiles();
                        selectedUnit = unitTileInstances[currMouseX, currMouseY];
                        drawValidMoveTiles(currMouseX, currMouseY);
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));
                        return;
                    }
                    else if (unitTileInstances[currMouseX, currMouseY] != null && !isPlayerOneTurn && !unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().isPlayerOneUnit)
                    {
                        eraseValidMoveTiles();
                        selectedUnit = unitTileInstances[currMouseX, currMouseY];
                        drawValidMoveTiles(currMouseX, currMouseY);
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));
                        return;
                    }
                }
            }
        }
    }

    //combat coroutine
    public IEnumerator CombatCoroutine(GameObject attackingUnit, GameObject defendingUnit)
    {

        attackingUnit.GetComponent<Animator>().SetTrigger("combatright");

        yield return new WaitForSeconds(3);

        StartCoroutine(HurtCoroutine(attackingUnit, defendingUnit));
    }

    //
    public IEnumerator HurtCoroutine(GameObject attackingUnit, GameObject defendingUnit)
    {

        attackingUnit.GetComponent<Animator>().SetTrigger("movedown");

        //if health is above zero for attacking or defending unit, play hurt animation



        //if health is below zero for attacking or defending unit, play death animation / delete unit



        yield return null;
    }

    //move unit up/down first
    public IEnumerator MovementCoroutine1(GameObject selectedUnit, Vector3 desiredPosition)
    {
        bool arrived = false;

        Vector3 desiredPositionVertical = desiredPosition;
        desiredPositionVertical.x = selectedUnit.transform.position.x;

        if (desiredPosition.y - selectedUnit.transform.position.y > 0.0f)
        {
            selectedUnit.GetComponent<Animator>().SetTrigger("moveup");
        } else if (desiredPosition.y - selectedUnit.transform.position.y < 0.0f)
        {
            selectedUnit.GetComponent<Animator>().SetTrigger("movedown");
        } else
        {
            arrived = true;
        }

        while (!arrived)
        {
            selectedUnit.transform.position = Vector3.MoveTowards(selectedUnit.transform.position, desiredPositionVertical, 1.0f * Time.deltaTime);
            if (Vector3.Distance(selectedUnit.transform.position, desiredPositionVertical) == 0) arrived = true;
            yield return null;
        }
        if (arrived)
        {
            if (desiredPosition.x - selectedUnit.transform.position.x == 0.0f)
            {
                selectedUnit.GetComponent<unit>().unitWasMoved = true;
                selectedUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);

                if (selectedUnit.GetComponent<unit>().lastFacingRight == true)
                {
                    selectedUnit.GetComponent<Animator>().SetTrigger("idleright");
                }
                else
                {
                    selectedUnit.GetComponent<Animator>().SetTrigger("idleleft");
                }
            } else {
                StartCoroutine(MovementCoroutine2(selectedUnit, desiredPosition));
            }
        }
    }

    //move unit left/right last
    public IEnumerator MovementCoroutine2(GameObject selectedUnit, Vector3 desiredPosition)
    {
        bool arrived = false;

        Vector3 desiredPositionHorizontal = desiredPosition;
        desiredPositionHorizontal.y = selectedUnit.transform.position.y;

        if (desiredPosition.x - selectedUnit.transform.position.x > 0.0f)
        {
            selectedUnit.GetComponent<Animator>().SetTrigger("moveright");
            selectedUnit.GetComponent<unit>().lastFacingRight = true;
        }
        else if (desiredPosition.x - selectedUnit.transform.position.x < 0.0f)
        {
            selectedUnit.GetComponent<Animator>().SetTrigger("moveleft");
            selectedUnit.GetComponent<unit>().lastFacingRight = false;
        }
        else
        {
            arrived = true;
        }

        while (!arrived)
        {
            selectedUnit.transform.position = Vector3.MoveTowards(selectedUnit.transform.position, desiredPositionHorizontal, 1.0f * Time.deltaTime);
            if (Vector3.Distance(selectedUnit.transform.position, desiredPositionHorizontal) == 0) arrived = true;
            yield return null;
        }
        if (arrived)
        {
            selectedUnit.GetComponent<unit>().unitWasMoved = true;
            selectedUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);

            if (selectedUnit.GetComponent<unit>().lastFacingRight == true)
            {
                selectedUnit.GetComponent<Animator>().SetTrigger("idleright");
            } else
            {
                selectedUnit.GetComponent<Animator>().SetTrigger("idleleft");
            }
        }
    }

    private void resetUnitProperties()
    {
        var units = GameObject.FindGameObjectsWithTag("unit");
        foreach (GameObject obj in units)
        {
            obj.GetComponent<unit>().unitWasMoved = false;
            obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void eraseValidMoveTiles()
    {
        var validMoveTilesPresent = GameObject.FindGameObjectsWithTag("ValidMove");
        foreach (GameObject obj in validMoveTilesPresent) 
        {
            Destroy(obj);
        }
    }

    private void drawValidMoveTiles(int x, int y)
    {
        int maxMoveDistance = selectedUnit.GetComponent<unit>().maxMoveDistance;

        for (int i = x - maxMoveDistance; i <= x + maxMoveDistance; i++)
        {
            for (int j = y - maxMoveDistance; j <= y + maxMoveDistance; j++)
            {
                if (i >= 0 && i < 10 && j >= 0 && j < 10)
                {
                    GameObject validMoveTileInstance = Instantiate(lightBlueValidMoveTile) as GameObject;
                    validMoveTileInstance.transform.position = new Vector3(i, j, -3);
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

                if (terrainInput[i,j] == "t1")
                {
                    GameObject terrainTile = Instantiate(terrain1) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                } else if (terrainInput[i, j] == "t2") {
                    GameObject terrainTile = Instantiate(terrain2) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                }
                else if (terrainInput[i, j] == "t3") {
                    GameObject terrainTile = Instantiate(terrain3) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                }
                else if (terrainInput[i, j] == "t4")
                {
                    GameObject terrainTile = Instantiate(terrain4) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                }
                else if (terrainInput[i, j] == "b1") {
                    GameObject terrainTile = Instantiate(base1) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                    baseLocation1.Set(i, j);
                }
                else if (terrainInput[i, j] == "b2") {
                    GameObject terrainTile = Instantiate(base2) as GameObject;
                    terrainTile.transform.position = new Vector3(i, j, -1);
                    baseLocation2.Set(i, j);
                }

            }
        }
    }

}
