using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameBoard : MonoBehaviour
{
    public static gameBoard Instance { set; get; }

    //public terrainTiles[,] terrain = new terrainTiles[10, 10];
    public Vector3 pz = new Vector3(-5.0f, 0.0f, 0.0f);

    public GameObject terrain1Prefab;
    public GameObject test;

    private Vector2 mouseOver;


    private void Start()
    {
        Instance = this;

        test = Instantiate(terrain1Prefab) as GameObject;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("BoardLayer")))
            {
                mouseOver.x = (hit.point.x);
                mouseOver.y = (hit.point.y);

                test.transform.position = new Vector3(mouseOver.x, mouseOver.y, 0);


                Debug.Log(mouseOver);
            }
        }
    }

}
