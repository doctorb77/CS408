    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreList : MonoBehaviour
{

    public GameObject playerEntryPrefab;
    string[] data = {"username:a|score:9384",
                    "username:b|score:8765",
                    "username:c|score:8543",
                    "username:d|score:1023",
                    "username:e|score:985",
                    "username:f|score:700",
                    "username:g|score:678",
                    "username:h|score:677",
                    "username:i|score:543",
                    "username:j|score:23"
                    };

    //ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        //scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        string name;
        string score;
        for(int i = 0; i < 10; i++){
      //      Debug.Log(scoreManager.GetDataValue(scoreManager.data[i], "username:"));
        //    Debug.Log(scoreManager.GetDataValue(scoreManager.data[i], "score:"));
            GameObject go = (GameObject)Instantiate(playerEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("Rank").GetComponent<Text>().text = (i+1).ToString();
            name = GetDataValue(data[i], "username:");            
            go.transform.Find("Username").GetComponent<Text>().text = name;
            score = GetDataValue(data[i], "score:");            
            go.transform.Find("Score").GetComponent<Text>().text = score;
        }
    }

    public string GetDataValue(string data, string index){
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if(value.IndexOf("|") >= 0){
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
}
