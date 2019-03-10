﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    public GameObject melee1tier1;
    public GameObject melee1tier2;
    public GameObject melee2tier1;
    public GameObject melee2tier2;
    public GameObject melee1tier3;
    public GameObject melee2tier3;
    public GameObject ranged1tier1;
    public GameObject ranged1tier2;
    public GameObject ranged1tier3;
    public GameObject ranged2tier1;
    public GameObject ranged2tier2;
    public GameObject ranged2tier3;

    public GameObject selectedUnit;
    public GameObject lightBlueValidMoveTile;
    public GameObject spawnMenu;

    public GameObject explosion;
    public GameObject hurtText;

    private Vector2 lastClicked;
    private Vector2 baseLocation1;
    private Vector2 baseLocation2;

    private bool isPlayerOneTurn;

    public int player1Funds, player2Funds;
    public Text fundsTextBox;
    public GameObject showPlayer1Turn;
    public GameObject showPlayer2Turn;
    public GameObject showPlayer1UnitSelection;
    public GameObject showPlayer2UnitSelection;

    private int score;

    private bool checkingVictory;
    public GameObject victoryPanel;
    public GameObject showPlayer1Victory;
    public GameObject showPlayer2Victory;

    bool spawnMenuActive;

    private void Start()
    {
        checkingVictory = true;
        showPlayer1Victory.SetActive(false);
        showPlayer2Victory.SetActive(false);

        spawnMenuActive = false;

        score = 20;
        

        string map = PlayerPrefs.GetString("mapname");

        //if (String.Compare(map, "\"a_nice_map1\"") == 0)
        //{
            //load map
            loadMap1();
        //}

        PlayerPrefs.SetInt("mapwidth", 10);
        PlayerPrefs.SetInt("mapheight", 10);


        Instance = this;

        selectedUnit = null;

        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                unitTileInstances[i, j] = null;

        //initialize turn state
        isPlayerOneTurn = (UnityEngine.Random.value > 0.5f);
        showPlayer1Turn.SetActive(false);
        showPlayer2Turn.SetActive(false);
        endTurn();

        //initialize menus
        spawnMenu.SetActive(false);
        victoryPanel.SetActive(false);

        showPlayer1UnitSelection.SetActive(false);
        showPlayer2UnitSelection.SetActive(false);

        //initialize default values
        player1Funds = player2Funds = 5000;

        if (isPlayerOneTurn)
        {
            showPlayer1UnitSelection.SetActive(true);
        }
        else
        {
            showPlayer2UnitSelection.SetActive(true);
        }

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
        eraseValidMoveTiles();

        if (spawnMenuActive)
        {
            displaySpawnMenu();
        }

        //count resource tiles
        //int aquiredResourceTiles = 0

        isPlayerOneTurn = !isPlayerOneTurn;

        if (isPlayerOneTurn)
        {
            showPlayer1UnitSelection.SetActive(true);
            showPlayer2UnitSelection.SetActive(false);
            player1Funds += 1000;
            StartCoroutine(displayPlayer1Turn());
        }
        else
        {
            showPlayer2UnitSelection.SetActive(true);
            showPlayer1UnitSelection.SetActive(false);
            player2Funds += 1000;
            StartCoroutine(displayPlayer2Turn());
        }

        resetUnitProperties();
    }

    private IEnumerator displayPlayer1Turn()
    {
        showPlayer2Turn.SetActive(false);
        showPlayer1Turn.SetActive(true);
        yield return new WaitForSeconds(3);
        showPlayer1Turn.SetActive(false);
    }

    private IEnumerator displayPlayer2Turn()
    {
        showPlayer1Turn.SetActive(false);
        showPlayer2Turn.SetActive(true);
        yield return new WaitForSeconds(3);
        showPlayer2Turn.SetActive(false);
    }

    public void displaySpawnMenu()
    {
        spawnMenuActive = !spawnMenuActive;
        spawnMenu.SetActive(!spawnMenu.activeInHierarchy);

        if (isPlayerOneTurn)
            fundsTextBox.text = player1Funds.ToString();
        else
            fundsTextBox.text = player2Funds.ToString();

    }

    public void spawnMeleeUnitTierOne()
    {
        spawnUnit("melee", 1, 1000);
    }

    public void spawnMeleeUnitTierTwo()
    {
        spawnUnit("melee", 2, 2000);
    }

    public void spawnMeleeUnitTierThree()
    {
        spawnUnit("melee", 3, 3000);
    }

    public void spawnRangedUnitTierOne()
    {
        spawnUnit("ranged", 1, 1000);
    }

    public void spawnRangedUnitTierTwo()
    {
        spawnUnit("ranged", 2, 2000);
    }

    public void spawnRangedUnitTierThree()
    {
        spawnUnit("ranged", 3, 3000);
    }

    public void spawnUnit(string type, int tier, int cost)
    {
        if (isPlayerOneTurn && unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y] == null && player1Funds - cost >= 0)
        {
            GameObject unitInstance = null;
            if (type == "ranged")
            {
                if (tier == 1)
                {
                    unitInstance = Instantiate(ranged1tier1) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f);
                    unitInstance.GetComponent<unit>().maxMoveDistance = 2;
                    unitInstance.GetComponent<unit>().health = 100;
                    unitInstance.GetComponent<unit>().maxHealth = 100;
                    unitInstance.GetComponent<unit>().attack = 20;
                    unitInstance.GetComponent<unit>().tier = 1;
                }
                else if (tier == 2)
                {
                    unitInstance = Instantiate(ranged1tier2) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().maxMoveDistance = 2;
                    unitInstance.GetComponent<unit>().health = 300;
                    unitInstance.GetComponent<unit>().maxHealth = 300;
                    unitInstance.GetComponent<unit>().attack = 50;
                    unitInstance.GetComponent<unit>().tier = 2;
                }
                else if (tier == 3)
                {
                    unitInstance = Instantiate(ranged1tier3) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().maxMoveDistance = 3;
                    unitInstance.GetComponent<unit>().health = 500;
                    unitInstance.GetComponent<unit>().maxHealth = 500;
                    unitInstance.GetComponent<unit>().attack = 90;
                    unitInstance.GetComponent<unit>().tier = 3;
                }
            }
            else
            {
                if (tier == 1)
                {
                    unitInstance = Instantiate(melee1tier1) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f);
                    unitInstance.GetComponent<unit>().health = 100;
                    unitInstance.GetComponent<unit>().maxHealth = 100;
                    unitInstance.GetComponent<unit>().attack = 20;
                    unitInstance.GetComponent<unit>().tier = 1;
                }
                else if (tier == 2)
                {
                    unitInstance = Instantiate(melee1tier2) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().health = 300;
                    unitInstance.GetComponent<unit>().maxHealth = 300;
                    unitInstance.GetComponent<unit>().attack = 50;
                    unitInstance.GetComponent<unit>().tier = 2;
                }
                else if (tier == 3)
                {
                    unitInstance = Instantiate(melee1tier3) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().health = 500;
                    unitInstance.GetComponent<unit>().maxHealth = 500;
                    unitInstance.GetComponent<unit>().attack = 90;
                    unitInstance.GetComponent<unit>().tier = 3;
                }
                unitInstance.GetComponent<unit>().maxMoveDistance = 1;
            }
            unitInstance.transform.position = new Vector3(baseLocation1.x, baseLocation1.y, -2);
        
            unitInstance.GetComponent<unit>().typeOfUnit = type;
            unitInstance.GetComponent<unit>().isPlayerOneUnit = true;
            unitInstance.GetComponent<unit>().unitWasMoved = false;

            if (baseLocation1.x < (terrainSize.x / 2.0f)) {
                unitInstance.GetComponent<unit>().lastFacingRight = true;
                unitInstance.GetComponent<Animator>().SetTrigger("idleright");
            } else {
                unitInstance.GetComponent<unit>().lastFacingRight = false;
                unitInstance.GetComponent<Animator>().SetTrigger("idleleft");
            }
            unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y] = unitInstance;
            player1Funds -= cost;
        } else if (!isPlayerOneTurn && unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y] == null && player2Funds - 1000 >= 0) {

            GameObject unitInstance = null;
            if (type == "ranged") {
                if (tier == 1)
                {
                    unitInstance = Instantiate(ranged2tier1) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f);
                    unitInstance.GetComponent<unit>().maxMoveDistance = 2;
                    unitInstance.GetComponent<unit>().health = 100;
                    unitInstance.GetComponent<unit>().maxHealth = 100;
                    unitInstance.GetComponent<unit>().attack = 20;
                    unitInstance.GetComponent<unit>().tier = 1;
                }
                else if (tier == 2)
                {
                    unitInstance = Instantiate(ranged2tier2) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().maxMoveDistance = 2;
                    unitInstance.GetComponent<unit>().health = 300;
                    unitInstance.GetComponent<unit>().maxHealth = 300;
                    unitInstance.GetComponent<unit>().attack = 50;
                    unitInstance.GetComponent<unit>().tier = 2;
                }
                else if (tier == 3)
                {
                    unitInstance = Instantiate(ranged2tier3) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().maxMoveDistance = 3;
                    unitInstance.GetComponent<unit>().health = 500;
                    unitInstance.GetComponent<unit>().maxHealth = 500;
                    unitInstance.GetComponent<unit>().attack = 90;
                    unitInstance.GetComponent<unit>().tier = 3;
                }
                
            }
            else {
                if (tier == 1)
                {
                    unitInstance = Instantiate(melee2tier1) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f);
                    unitInstance.GetComponent<unit>().health = 100;
                    unitInstance.GetComponent<unit>().maxHealth = 100;
                    unitInstance.GetComponent<unit>().attack = 20;
                    unitInstance.GetComponent<unit>().tier = 1;
                }
                else if (tier == 2)
                {
                    unitInstance = Instantiate(melee2tier2) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().health = 300;
                    unitInstance.GetComponent<unit>().maxHealth = 300;
                    unitInstance.GetComponent<unit>().attack = 50;
                    unitInstance.GetComponent<unit>().tier = 2;
                }
                else if (tier == 3)
                {
                    unitInstance = Instantiate(melee2tier3) as GameObject;
                    unitInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f);
                    unitInstance.GetComponent<unit>().health = 500;
                    unitInstance.GetComponent<unit>().maxHealth = 500;
                    unitInstance.GetComponent<unit>().attack = 90;
                    unitInstance.GetComponent<unit>().tier = 3;
                }
                unitInstance.GetComponent<unit>().maxMoveDistance = 1;
            }
            unitInstance.transform.position = new Vector3(baseLocation2.x, baseLocation2.y, -2);

            unitInstance.GetComponent<unit>().typeOfUnit = type;
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
            player2Funds -= cost;
        }

        displaySpawnMenu();
    }

    private void Update()
    {
        checkVictory();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("BoardLayer")))
            {
                int currMouseX = (int)Mathf.Round((hit.point.x));
                int currMouseY = (int)Mathf.Round((hit.point.y));

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

                        if (selectedUnit.GetComponent<unit>().typeOfUnit == "melee")
                        {
                            if ((currMouseX == selectedUnit.transform.position.x + 1 && currMouseY == selectedUnit.transform.position.y + 1) ||
                            (currMouseX == selectedUnit.transform.position.x + 1 && currMouseY == selectedUnit.transform.position.y - 1) ||
                            (currMouseX == selectedUnit.transform.position.x - 1 && currMouseY == selectedUnit.transform.position.y + 1) ||
                            (currMouseX == selectedUnit.transform.position.x - 1 && currMouseY == selectedUnit.transform.position.y - 1))
                            {
                                return;
                            }
                        }
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
                    if (!isDefendingWithinAttacking(selectedUnit, unitTileInstances[currMouseX, currMouseY]))
                        return;

                    StartCoroutine(CombatCoroutine(selectedUnit, unitTileInstances[currMouseX, currMouseY]));

                    selectedUnit.GetComponent<unit>().unitWasMoved = true;
                    selectedUnit = null;
                    eraseValidMoveTiles();
                }
                //if player 2 moving to attack player 1 unit
                else if (!isPlayerOneTurn && unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().isPlayerOneUnit 
                    && selectedUnit != null && selectedUnit.GetComponent<unit>().unitWasMoved == false)
                {
                    if (!isDefendingWithinAttacking(selectedUnit, unitTileInstances[currMouseX, currMouseY]))
                        return;

                    StartCoroutine(CombatCoroutine(selectedUnit, unitTileInstances[currMouseX, currMouseY]));

                    selectedUnit.GetComponent<unit>().unitWasMoved = true;
                    selectedUnit = null;
                    eraseValidMoveTiles();
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
                        drawValidMoveTiles(currMouseX, currMouseY, selectedUnit.GetComponent<unit>().typeOfUnit);
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));
                        return;
                    }
                    else if (unitTileInstances[currMouseX, currMouseY] != null && !isPlayerOneTurn && !unitTileInstances[currMouseX, currMouseY].GetComponent<unit>().isPlayerOneUnit)
                    {
                        eraseValidMoveTiles();
                        selectedUnit = unitTileInstances[currMouseX, currMouseY];
                        drawValidMoveTiles(currMouseX, currMouseY, selectedUnit.GetComponent<unit>().typeOfUnit);
                        lastClicked.x = Mathf.Round((hit.point.x));
                        lastClicked.y = Mathf.Round((hit.point.y));
                        return;
                    }
                }
            }
        }
    }

    public void returnToMainMenu()
    {
        PlayerPrefs.SetInt("gameWasPlayed", 0);
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene("Crater_Clash_MainMenu");
    }

    public void submitScore()
    {
        PlayerPrefs.SetInt("gameWasPlayed", 1);
        PlayerPrefs.SetInt("score", score);
        SceneManager.LoadScene("Crater_Clash_MainMenu");
    }

    public IEnumerator pause(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public IEnumerator deleteAllUnitsAndShowVictoryPlayer1()
    {
        var units = GameObject.FindGameObjectsWithTag("unit");
        foreach (GameObject obj in units)
        {
            GameObject explosionObject = Instantiate(explosion) as GameObject;
            explosionObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -2);
            Destroy(explosionObject, 1.0f);

            StartCoroutine(pause(0.2f));

            yield return new WaitForSeconds(0.1f);

            Destroy(obj);
        }

        showPlayer1Victory.SetActive(true);
        victoryPanel.SetActive(!victoryPanel.activeInHierarchy);
    }

    public IEnumerator deleteAllUnitsAndShowVictoryPlayer2()
    {
        var units = GameObject.FindGameObjectsWithTag("unit");
        foreach (GameObject obj in units)
        {
            GameObject explosionObject = Instantiate(explosion) as GameObject;
            explosionObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -2);
            Destroy(explosionObject, 1.0f);

            StartCoroutine(pause(0.2f));

            yield return new WaitForSeconds(0.1f);

            Destroy(obj);
        }

        showPlayer2Victory.SetActive(true);
        victoryPanel.SetActive(!victoryPanel.activeInHierarchy);
    }

    public void checkVictory()
    {
        if (checkingVictory)
        {
            if (unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y] != null)
            {
                if (unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y].GetComponent<unit>().isPlayerOneUnit &&
                    unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y].transform.position.x == baseLocation2.x &&
                    unitTileInstances[(int)baseLocation2.x, (int)baseLocation2.y].transform.position.y == baseLocation2.y)
                {
                    checkingVictory = false;

                    StartCoroutine(deleteAllUnitsAndShowVictoryPlayer1());
                }
            }

            if (unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y] != null)
            {
                if (!unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y].GetComponent<unit>().isPlayerOneUnit &&
                    unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y].transform.position.x == baseLocation1.x &&
                    unitTileInstances[(int)baseLocation1.x, (int)baseLocation1.y].transform.position.y == baseLocation1.y)
                {
                    checkingVictory = false;

                    StartCoroutine(deleteAllUnitsAndShowVictoryPlayer2());
                }
            }
        }
    }

    public bool isDefendingWithinAttacking(GameObject attackingUnit, GameObject defendingUnit)
    {
        bool defendingUnitWithinRange = false;
        if (attackingUnit.GetComponent<unit>().typeOfUnit == "melee")
        {
            if (defendingUnit.transform.position.x == attackingUnit.transform.position.x - 1 && defendingUnit.transform.position.y == attackingUnit.transform.position.y)
            {
                defendingUnitWithinRange = true;
            }
            else if (defendingUnit.transform.position.x == attackingUnit.transform.position.x + 1 && defendingUnit.transform.position.y == attackingUnit.transform.position.y)
            {
                defendingUnitWithinRange = true;
            }
            else if (defendingUnit.transform.position.x == attackingUnit.transform.position.x && defendingUnit.transform.position.y == attackingUnit.transform.position.y + 1)
            {
                defendingUnitWithinRange = true;
            }
            else if (defendingUnit.transform.position.x == attackingUnit.transform.position.x && defendingUnit.transform.position.y == attackingUnit.transform.position.y - 1)
            {
                defendingUnitWithinRange = true;
            }
        }
        else if (attackingUnit.GetComponent<unit>().typeOfUnit == "ranged")
        {
            if (Math.Abs(defendingUnit.transform.position.x - attackingUnit.transform.position.x) <= attackingUnit.GetComponent<unit>().maxMoveDistance &&
                Math.Abs(defendingUnit.transform.position.y - attackingUnit.transform.position.y) <= attackingUnit.GetComponent<unit>().maxMoveDistance)
            {
                defendingUnitWithinRange = true;
            }
        }

        return defendingUnitWithinRange;
    }

    //combat coroutine
    public IEnumerator CombatCoroutine(GameObject attackingUnit, GameObject defendingUnit)
    {
        //if defending unit is not within range of attack unit, break
        bool defendingUnitWithinRange = isDefendingWithinAttacking(attackingUnit, defendingUnit);
        if (defendingUnitWithinRange == false)
        {
            yield break;
        }


        if ((defendingUnit.transform.position.y - attackingUnit.transform.position.y) >= Math.Abs(defendingUnit.transform.position.x - attackingUnit.transform.position.x))
        {
            attackingUnit.GetComponent<Animator>().SetTrigger("combatup");
        }
        else if ((attackingUnit.transform.position.y - defendingUnit.transform.position.y) >= Math.Abs(defendingUnit.transform.position.x - attackingUnit.transform.position.x))
        {
            attackingUnit.GetComponent<Animator>().SetTrigger("combatdown");
        }
        else if ((attackingUnit.transform.position.x - defendingUnit.transform.position.x) >= Math.Abs(defendingUnit.transform.position.y - attackingUnit.transform.position.y))
        {
            attackingUnit.GetComponent<Animator>().SetTrigger("combatleft");
        }
        else if ((defendingUnit.transform.position.x - attackingUnit.transform.position.x) >= Math.Abs(defendingUnit.transform.position.y - attackingUnit.transform.position.y))
        {
            attackingUnit.GetComponent<Animator>().SetTrigger("combatright");
        }



        //if attackingUnit is within defendingUnit range, play defendingUnit combat animation
        bool attackingUnitWithinRange = isDefendingWithinAttacking(defendingUnit, attackingUnit);
        if (attackingUnitWithinRange)
        {
            if ((attackingUnit.transform.position.y - defendingUnit.transform.position.y) >= Math.Abs(attackingUnit.transform.position.x - defendingUnit.transform.position.x))
            {
                defendingUnit.GetComponent<Animator>().SetTrigger("combatup");
            }
            else if ((defendingUnit.transform.position.y - attackingUnit.transform.position.y) >= Math.Abs(attackingUnit.transform.position.x - defendingUnit.transform.position.x))
            {
                    defendingUnit.GetComponent<Animator>().SetTrigger("combatdown");
            }
            else if ((defendingUnit.transform.position.x - attackingUnit.transform.position.x) >= Math.Abs(attackingUnit.transform.position.y - defendingUnit.transform.position.y))
            {
                    defendingUnit.GetComponent<Animator>().SetTrigger("combatleft");
            }
            else if ((attackingUnit.transform.position.x - defendingUnit.transform.position.x) >= Math.Abs(attackingUnit.transform.position.y - defendingUnit.transform.position.y))
            {
                    defendingUnit.GetComponent<Animator>().SetTrigger("combatright");
            }
        }



        yield return new WaitForSeconds(2);

        if (defendingUnit.GetComponent<unit>().lastFacingRight == true)
        {
            defendingUnit.GetComponent<Animator>().SetTrigger("idleright");
        }
        else
        {
            defendingUnit.GetComponent<Animator>().SetTrigger("idleleft");
        }

        if (attackingUnit.GetComponent<unit>().lastFacingRight == true)
        {
            attackingUnit.GetComponent<Animator>().SetTrigger("idleright");
        }
        else
        {
            attackingUnit.GetComponent<Animator>().SetTrigger("idleleft");
        }

        
        Random rnd = new Random();

        if (defendingUnitWithinRange)
        {
            int attack = attackingUnit.GetComponent<unit>().attack;

            if (attackingUnit.GetComponent<unit>().tier == 1)
                attack = attack + (Random.Range(-2, 2));
            else if (attackingUnit.GetComponent<unit>().tier == 3)
                attack = attack + (Random.Range(-5, 5));
            else if (attackingUnit.GetComponent<unit>().tier == 3)
                attack = attack + (Random.Range(-10, 10));

            StartCoroutine(HurtCoroutine(attackingUnit, defendingUnit, attack));
        }

        if (attackingUnitWithinRange)
        {
            int attack = attackingUnit.GetComponent<unit>().attack;

            if (attackingUnit.GetComponent<unit>().tier == 1)
                attack = attack + (Random.Range(-2, 2));
            else if (attackingUnit.GetComponent<unit>().tier == 3)
                attack = attack + (Random.Range(-5, 5));
            else if (attackingUnit.GetComponent<unit>().tier == 3)
                attack = attack + (Random.Range(-10, 10));

            StartCoroutine(HurtCoroutine(defendingUnit, attackingUnit, attack));
        }
    }

    public IEnumerator HurtCoroutine(GameObject attackingUnit, GameObject unit, int damageTaken)
    {

        //if health is above zero for unit, play hurt animation

        for (var n = 0; n < 5; n++)
        {
            unit.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
            unit.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        unit.GetComponent<Renderer>().enabled = true;

        unit.GetComponent<unit>().health -= damageTaken;

        StartCoroutine(spawnAndMoveHurtNumber((int)unit.transform.position.x, (int)unit.transform.position.y, damageTaken));

        if (isPlayerOneTurn && attackingUnit.GetComponent<unit>().isPlayerOneUnit)
        {
            Debug.Log("test");
            attackingUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
        }
        if (!isPlayerOneTurn && !attackingUnit.GetComponent<unit>().isPlayerOneUnit)
        {
            Debug.Log("test2");
            attackingUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
        }

        if (unit.GetComponent<unit>().health <= 0)
        {
            int unitPosX = (int)unit.transform.position.x;
            int unitPosY = (int)unit.transform.position.y;

            unitTileInstances[unitPosX, unitPosY] = null;

            Destroy(unit);

            GameObject explosionObject = Instantiate(explosion) as GameObject;
            explosionObject.transform.position = new Vector3(unitPosX, unitPosY, -2);
            Destroy(explosionObject, 1.0f);
        }

        yield return null;
    }

    public IEnumerator spawnAndMoveHurtNumber(int startX, int startY, int attackDamage)
    {
        GameObject gameObjectText = Instantiate(hurtText) as GameObject;


        gameObjectText.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "-" + attackDamage.ToString();
        //gameObjectText.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().fontSize = 6;
        //gameObjectText.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = new Color(1.0f, 0.0f, 0.0f);

        bool arrived = false;

        gameObjectText.transform.position = new Vector3(startX, startY, -2);

        Vector3 desiredPositionVertical = new Vector3(gameObjectText.transform.position.x, gameObjectText.transform.position.y + 1.0f, -2.0f);

        while (!arrived)
        {
            
            gameObjectText.transform.position = Vector3.MoveTowards(gameObjectText.transform.position, desiredPositionVertical, 0.75f * Time.deltaTime);
            if (Vector3.Distance(gameObjectText.transform.position, desiredPositionVertical) == 0) arrived = true;
            yield return null;
        }
        if (arrived)
        {
            Destroy(gameObjectText);
        }
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
                if (isPlayerOneTurn && selectedUnit.GetComponent<unit>().isPlayerOneUnit == true)
                {
                    selectedUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                }

                if (!isPlayerOneTurn && selectedUnit.GetComponent<unit>().isPlayerOneUnit == false)
                {
                    selectedUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                }

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
            if (isPlayerOneTurn && selectedUnit.GetComponent<unit>().isPlayerOneUnit == true)
            {
                selectedUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
            }

            if (!isPlayerOneTurn && selectedUnit.GetComponent<unit>().isPlayerOneUnit == false)
            {
                selectedUnit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
            }

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

    private void drawValidMoveTiles(int x, int y, string unitType)
    {
        int maxMoveDistance = selectedUnit.GetComponent<unit>().maxMoveDistance;

        for (int i = x - maxMoveDistance; i <= x + maxMoveDistance; i++)
        {
            for (int j = y - maxMoveDistance; j <= y + maxMoveDistance; j++)
            {
                if (i >= 0 && i < 10 && j >= 0 && j < 10)
                {
                    if (unitType == "melee")
                    {
                        if (!(i == x + 1 && j == y + 1) && !(i == x + 1 && j == y - 1) && !(i == x - 1 && j == y + 1) && !(i == x - 1 && j == y - 1))
                        {
                            GameObject validMoveTileInstance = Instantiate(lightBlueValidMoveTile) as GameObject;
                            validMoveTileInstance.transform.position = new Vector3(i, j, -3);
                        }
                    }
                    else
                    {
                        GameObject validMoveTileInstance = Instantiate(lightBlueValidMoveTile) as GameObject;
                        validMoveTileInstance.transform.position = new Vector3(i, j, -3);
                    }
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
