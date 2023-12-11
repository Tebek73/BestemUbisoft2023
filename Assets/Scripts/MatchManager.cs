using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    public TMP_Text winText;
    public TMP_Text currPlayerText;
    public SevenSeconds timeScript;
    // Start is called before the first frame update
    Slingshot playerScript;
    public Camera mainCamera;
    private int round;
    private string player1 = "Player1";
    private string player2 = "Player2";

    public GameObject[] player1Catapults;
    public GameObject[] player2Catapults;

    private GameObject currShooter;

    private string currWeapon;
    void Start()
    {
       round = 0;
       
        Time.timeScale = 1f;
       timeScript = GetComponent<SevenSeconds>();
       //ca sa se faca player1Catapults automat
       // Find all GameObjects tagged as "Player"
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // Iterate through each found object
        foreach (GameObject playerObj in playerObjects)
        {
            // Convert the object's position from world space to viewport space
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(playerObj.transform.position);

            // Check if the object is on the left side of the viewport (camera's view)
            if (viewportPos.x < 0.5f)
            {
                // If it's on the left side, add it to the player1Catapults array
                AddToPlayer1Catapults(playerObj);
            }
            else
            {
                // If it's on the right side, add it to the player2Catapults array
                AddToPlayer2Catapults(playerObj);
            }
        }
        NextRound();
    }

    // Update is called once per frame
    bool gameEnded = false;
    void Update()
    {
        VerifyWinner();


        
        if(timeScript.elapsedTime >=7){
            timeScript.StopTimer();
            
            NextRound();
        } 
        if(currWeapon.Contains(" ")) currWeapon = currWeapon.Substring(0, currWeapon.IndexOf(' '));
        if(round%2!=0){
            currPlayerText.text = player1 + " " + currWeapon;
            
            if(currShooter == null && gameEnded == false){
               
                SelectPlayer(1);
                playerScript.forceGuidelineFalse();
            }
            
        }
        else {
            currPlayerText.text = player2+ " " + currWeapon;
            
            if(currShooter == null && gameEnded == false){
                
                SelectPlayer(2);
                playerScript.forceGuidelineFalse();
            }
            
        }
        
    }

    void VerifyWinner(){
        int dead1 = 0;
        int dead2 = 0;
        int nr1 = 0;
        int nr2 = 0;
        foreach(GameObject g in player1Catapults){
            nr1++;
            if(g == null){//a castigat 2
                dead1++;
            }
        }
        if(dead1==nr1){
                Time.timeScale = 0f;
                winText.text = "A castigat Player2";
                gameEnded = true;
        }
        foreach(GameObject g in player2Catapults){
            nr2++;
            if(g == null){//a castigat 2
                dead2++;
            }
        }
        if(dead2==nr2){
                Time.timeScale = 0f;
                winText.text = "A castigat Player1";
                gameEnded = true;
        }

        
    }

    public void SendToMenu(){
            
        SceneManager.LoadScene(0);
    
    }

    //trebuie sa activam o catapulta care sa functioneze
    public void SelectPlayer(int p){
        if(p == 1){
            int catapultIndex = Random.Range(0,player1Catapults.Length);
            if(player1Catapults[catapultIndex]==null){
                while(player1Catapults[catapultIndex]==null){
                    catapultIndex = Random.Range(0,player1Catapults.Length);
                }
            }
            currShooter = player1Catapults[catapultIndex];
            ActivateCatapult(player1Catapults[catapultIndex]);
            currWeapon = player1Catapults[catapultIndex].name;
        }
        else{
            int catapultIndex = Random.Range(0,player2Catapults.Length);
            if(player2Catapults[catapultIndex]==null){
                while(player2Catapults[catapultIndex]==null){
                   catapultIndex = Random.Range(0,player2Catapults.Length);
                }
            }
            currShooter = player2Catapults[catapultIndex];
            ActivateCatapult(player2Catapults[catapultIndex]);
            currWeapon = player2Catapults[catapultIndex].name;
        }
    }

    void ActivateCatapult(GameObject p){

        if(playerScript != null) playerScript.isActive = false;
        playerScript = p.GetComponent<Slingshot>();


            if (playerScript != null)
            {
                
                playerScript.isActive = true;
            }
            
            
    }

    

    public void NextRound(){
        round++;
        
        timeScript.ResetTimer();

        Invoke("startTimerWithDelay",3f);
        
        if(round%2!=0){
            SelectPlayer(1);
        }
        else{
            SelectPlayer(2);
        }


        
        
    }

    void startTimerWithDelay(){
        timeScript.StartTimer();
    }

     void AddToPlayer1Catapults(GameObject obj)
    {
        // Add the object to player1Catapults array
        // You can expand this array or use a List<GameObject> if the size is dynamic
        player1Catapults = AppendToArray(player1Catapults, obj);
    }
    void AddToPlayer2Catapults(GameObject obj)
    {
        player2Catapults = AppendToArray(player2Catapults, obj);
    }
    GameObject[] AppendToArray(GameObject[] array, GameObject obj)
    {
        GameObject[] newArray = new GameObject[array.Length + 1];

        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }

        newArray[array.Length] = obj;

        return newArray;
    }
}
