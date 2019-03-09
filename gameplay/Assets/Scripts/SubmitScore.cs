using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System;
using System.IO;
using System.Text;
public class SubmitScore : MonoBehaviour
{
    public int finalScore = 0;
    public InputField username;
    public Text score;
    public Text errorMessage;
    private string uname;
    string API_KEY = "2oRiTkq1ZrBIqNFzU7tVqFELzCpu_J0H";
    // Start is called before the first frame update
    void Start()
    {
        finalScore = PlayerPrefs.GetInt("score");
        score.text = finalScore.ToString();
    }

    public void submit(){
        if (finalScore == 0)
            return;

        if(!string.IsNullOrWhiteSpace(username.text)){
            // trim white spaces from start and end of username
            uname = ((username.text).TrimStart()).TrimEnd();
                
            var request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.mlab.com/api/1/databases/cs408_highscores/collections/people?apiKey={0}", API_KEY));
            //Create object in the form of an entry using username and score (currently hardcoded)
            string json = "{ 'name':'"+uname+"', 'score':"+finalScore+"}";

            var data = Encoding.ASCII.GetBytes(json);

            //create a POST request
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf8";
            request.ContentLength = data.Length;

            //Make the request
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            //Get request response from server/database
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            // Navigate to Scoreboard scene
            //SceneManager.LoadScene("Scoreboard");
            finalScore = 0;
            score.text = finalScore.ToString();
        }
        else {
            // username cannot be empty
            errorMessage.text = "Nickname cannot be empty!";
        }
    }
}
