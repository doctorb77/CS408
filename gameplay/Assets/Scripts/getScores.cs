using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using UnityEngine.UI;
//using Assets;
public class getScores : MonoBehaviour
{

string API_KEY = "2oRiTkq1ZrBIqNFzU7tVqFELzCpu_J0H";
    public int offset = 0;
    public int peopleIndex = 0;
    struct person {
        public string name;
        public string score;
    }

    person[] people = new person[1000];
    // Start is called before the first frame update
    void Start()
    {
        click();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.mlab.com/api/1/databases/cs408_highscores/collections/people?apiKey={0}&s={1}", API_KEY, "{\"score\": -1}"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        parseScores(jsonResponse);
        //printPeople();
        displayTenScores();
    }

    public void printPeople() {
        for (int i = 0; i < 100; i++) {
            Debug.Log("Person: " + people[i].name + " Score: " + people[i].score);
        }
    }
    public void parseScores(string phrase) {
        string[] words = phrase.Split(',');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Contains("_id")) {
                var foos = new List<string>(words);
                foos.RemoveAt(i);
                words = foos.ToArray();
            }
            if (words[i].Contains("}")) {
                words[i] = words[i].Substring(0, words[i].Length - 2);
            }
            if (i % 2 == 0)
            {
                people[peopleIndex].name = words[i].Split(':')[1].Split('\"')[1];
            }
            else
            {
                if (i == words.Length - 1) {
                    words[i] = words[i].Substring(0, words[i].Length - 1);
                }
                people[peopleIndex].score = words[i].Split(':')[1].Split(' ')[1];
                peopleIndex++;
            }
        }
    }

    public void displayTenScores() {
        int e = 0;
        for (int i = offset; i < offset + 10; i++) {
            GameObject.Find("rank" + (e+1).ToString()).GetComponent<Text>().text = (e+1+offset).ToString();
            GameObject.Find("name" + (e + 1).ToString()).GetComponent<Text>().text = people[e + offset].name;
            GameObject.Find("score" + (e + 1).ToString()).GetComponent<Text>().text = people[e + offset].score;
            e++;
        }
    }

    public void click() {
        GameObject.Find("<").GetComponent<Button>().onClick.AddListener(leftArrowClick);
        GameObject.Find(">").GetComponent<Button>().onClick.AddListener(rightArrowClick);
    }

    public void leftArrowClick() {
        if (!((offset - 10) < 0)) {
            offset -= 10;
        }
        displayTenScores();
    }

    public void rightArrowClick()
    {
        if (!(offset+10 > peopleIndex)) { 
            offset += 10;
        }
        displayTenScores();
    }




}
