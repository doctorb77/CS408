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

    public GameObject playerEntryPrefab;

    private int size;
    string API_KEY = "2oRiTkq1ZrBIqNFzU7tVqFELzCpu_J0H";
    public int offset = 0;
    public int searchOffset = 0;
    public bool searching = false;
    public int peopleIndex = 0;
    public int searchIndex = 0;
    struct person
    {
        public int rank;
        public string name;
        public string score;
    }
    //array that holds all score entries
    person[] people = new person[1000];

    //Array that holds any matches for searching
    person[] searchArray = new person[1000];

    //person[] people = new person[1000];
    // Start is called before the first frame update
    void Start()
    {
        click();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.mlab.com/api/1/databases/cs408_highscores/collections/people?apiKey={0}&s={1}", API_KEY, "{\"score\": -1}"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();

        //Get the amount of people there are in the database and resize the array accordingly
        getSize(jsonResponse);
        Array.Resize(ref people, size);

        //Parse scores into the person array
        parseScores(jsonResponse);

        //Display current 10 scores
        displayTenScores();
        GameObject.Find("searchButton").GetComponent<Button>().onClick.AddListener(searchButtonCLick);
    }

    /// <summary>
    /// Finds number of matches for the search query in the people array, and resizes searchArray accordingly
    /// </summary>
    public void findNumberOfMatches()
    {
        int total = 0;
        string testString = GameObject.Find("searchText").GetComponent<InputField>().text;
        for (int i = 0; i < people.Length; i++)
        {
            if (people[i].name.Contains(testString))
            {
                total++;
            }
        }
        Array.Resize(ref searchArray, total);
    }

    /// <summary>
    /// Adds all matches of the search query to the search array
    /// </summary>
    public void enterMatchesInSearch()
    {
        string testString = GameObject.Find("searchText").GetComponent<InputField>().text;
        for (int i = 0; i < people.Length; i++)
        {
            if (people[i].name.Contains(testString))
            {
                searchArray[searchIndex] = people[i];
                searchIndex++;
            }
        }
    }

    /// <summary>
    /// A debugging function that prints each person in the people array and their scores
    /// </summary>
    public void printPeople()
    {
        for (int i = 0; i < people.Length; i++)
        {
            Debug.Log("Person: " + people[i].name + " Score: " + people[i].score);
        }
    }
    public void printSearch()
    {
        for (int i = 0; i < searchArray.Length; i++)
        {
            Debug.Log("Person: " + searchArray[i].name + "Rank: " + searchArray[i].rank + " Score: " + searchArray[i].score);
        }
    }


    /// <summary>
    /// Find amount of people, so that you can create a correctly sized array of person objects
    /// </summary>
    /// <param name="json"></param>
    public void getSize(string json)
    {
        size = 0;
        string[] seperate = json.Split(',');
        for (int i = 0; i < seperate.Length; i++)
        {
            if (seperate[i].Contains("score"))
            {
                size++;
            }
        }
        Debug.Log(size);
    }


    /// <summary>
    /// A function that takes the string'd version of a JSON object from the scores database, and parses it into the people array
    /// </summary>
    /// <param name="phrase"></param>
    public void parseScores(string phrase)
    {
        string[] words = phrase.Split(',');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Contains("_id"))
            {
                var foos = new List<string>(words);
                foos.RemoveAt(i);
                words = foos.ToArray();
            }
            if (words[i].Contains("}"))
            {
                words[i] = words[i].Substring(0, words[i].Length - 2);
            }
            if (i % 2 == 0)
            {
                people[peopleIndex].name = words[i].Split(':')[1].Split('\"')[1];
            }
            else
            {
                if (i == words.Length - 1)
                {
                    words[i] = words[i].Substring(0, words[i].Length - 1);
                }
                people[peopleIndex].score = words[i].Split(':')[1].Split(' ')[1];
                peopleIndex++;
            }
        }
        for (int i = 0; i < people.Length; i++)
        {
            people[i].rank = i + 1;
        }
    }
    public void searchButtonCLick()
    {
        searchIndex = 0;
        findNumberOfMatches();
        enterMatchesInSearch();
        printSearch();
        searching = true;
        clearValues();
        displayTenScoresSearch();
    }

    /// <summary>
    /// Retrieves the textbox of the highscores canvas and assigns correct values to the ranks, usernames, and scores
    /// </summary>
    public void displayTenScores()
    {
        int e = 0;
        for (int i = offset; i < offset + 10 && (e + offset) < people.Length; i++, e++)
        {
            // create a new playerEntry prefab 
            GameObject go = (GameObject)Instantiate(playerEntryPrefab);
            // make it's parent this transform (player score list)
            go.transform.SetParent(this.transform);
            // add rank, username, and score to the player entry
            go.transform.Find("Rank").GetComponent<Text>().text = (e + 1 + offset).ToString();
            go.transform.Find("Username").GetComponent<Text>().text = people[e + offset].name;
            go.transform.Find("Score").GetComponent<Text>().text = people[e + offset].score;
        }
    }

    public void displayTenScoresSearch()
    {
        int e = 0;
        for (int i = searchOffset; i < searchOffset + 10 && (e + searchOffset) < searchArray.Length; i++, e++)
        {
            // create a new playerEntry prefab 
            GameObject go = (GameObject)Instantiate(playerEntryPrefab);
            // make it's parent this transform (player score list)
            go.transform.SetParent(this.transform);
            // add rank, username, and score to the player entry
            go.transform.Find("Rank").GetComponent<Text>().text = searchArray[e + offset].rank.ToString();
            go.transform.Find("Username").GetComponent<Text>().text = searchArray[e + offset].name;
            go.transform.Find("Score").GetComponent<Text>().text = searchArray[e + offset].score;
        }
    }


    /// <summary>
    /// Initializes click events for the < and > buttons
    /// </summary>
    public void click()
    {
        GameObject.Find("<").GetComponent<Button>().onClick.AddListener(leftArrowClick);
        GameObject.Find(">").GetComponent<Button>().onClick.AddListener(rightArrowClick);
    }


    /// <summary>
    /// click event for the < option. it reduces the offset by 10 and then calls display scores again to assign the text box values 
    /// to the previous 10 scores of the people array
    /// </summary>
    public void leftArrowClick()
    {
        clearValues();
        if (!searching)
        {
            if (!((offset - 10) < 0))
            {
                offset -= 10;
            }
            displayTenScores();
        }
        else
        {
            if (!((searchOffset - 10) < 0))
            {
                searchOffset -= 10;
            }
            displayTenScoresSearch();
        }
    }


    /// <summary>
    /// click event for the > option. it reduces the offset by 10 and then calls display scores again to assign the text box values 
    /// to the next 10 scores of the people array
    /// </summary>
    public void rightArrowClick()
    {
        if (!searching)
        {
            clearValues();
            if (!(offset + 10 > peopleIndex))
            {
                offset += 10;
            }
            displayTenScores();
        }
        else
        {
            clearValues();
            if (!(searchOffset + 10 > searchIndex))
            {
                searchOffset += 10;
            }
            displayTenScoresSearch();
        }
    }


    /// <summary>
    /// Clear the highscores of the previous 10 scores (for use on the last page, where not all of the scores will be rewritten)
    /// </summary>
    public void clearValues()
    {
        // remove the children of player score list
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }


}
