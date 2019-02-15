using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubmitScore : MonoBehaviour
{
    public int finalScore = 287;
    public InputField username;
    public Text score;
    public Text errorMessage;
    private string uname;

    // Start is called before the first frame update
    void Start()
    {
        // TODO use the real score after the player finishes the game
        score.text = finalScore.ToString();
    }

    public void submit(){
        if(!string.IsNullOrWhiteSpace(username.text)){
            // trim white spaces from start and end of username
            uname = ((username.text).TrimStart()).TrimEnd();
                       
            /*
             * TODO: send score and username to database
             * score.text stores the score to be submitted
             * uname stores the username to be submitted
             */

            // Navigate to Scoreboard scene
            SceneManager.LoadScene("Scoreboard");
        }else{
            // username cannot be empty
            errorMessage.text = "Nickname cannot be empty!";
        }
    }
}
