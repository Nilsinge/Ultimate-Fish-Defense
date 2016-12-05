using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int shipsPassed = 0;
    public int shipsAllowedToPass = 3;
    private int fishToCatch = 2;

    public GUIText shipStatus;
    public GUIText gameOverText;
    public GUIText winText;
    private bool endOfGame = false;
    private List<GameObject> fishList;

    private Button repairButton;
    private Button upgradeButton;
    public float lastWaveToWinTime;


	private void Start ()
    {
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        fishList = new List<GameObject>();
        fishList.AddRange(GameObject.FindGameObjectsWithTag("Fish"));
        shipStatus.text = "Ships escaped: " + shipsPassed + " / " + shipsAllowedToPass;

        repairButton = GameObject.Find("BtnRepair").GetComponent<Button>();
        upgradeButton = GameObject.Find("BtnUpgrade").GetComponent<Button>();

        int upgradeCost = Tower.upgradeCost;
        int repairCost = Tower.repairCost;

        upgradeButton.GetComponentInChildren<Text>().text += " ($" + upgradeCost + ")";
        repairButton.GetComponentInChildren<Text>().text += " ($" + repairCost + ")";
    }

    private void Update () {      
        if (endOfGame == true)
        {
            Time.timeScale = 0.0f;
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1.0f;
                Scene level = SceneManager.GetActiveScene();
                SceneManager.LoadScene(level.name);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("LevelSelection");
            }
        }
    }

    private void CatchFish()
    {
        if (fishToCatch > fishList.Count)
        {
            return;
        }
        else
        {
            for (int i = 0; i < fishToCatch; i++)
            {
                GameObject caughtFish = fishList[0];
                fishList.Remove(caughtFish);
                Destroy(caughtFish);
            }
        }
    }

    public void ShipGotThrough()
    {
        shipsPassed = shipsPassed + 1;
        shipStatus.text = "Ships escaped: " + shipsPassed + " / " + shipsAllowedToPass;

        CatchFish();
        
        if (shipsPassed >= shipsAllowedToPass)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        endOfGame = true;
        gameOverText.gameObject.SetActive(true);
    }

    public IEnumerator WinGame()
    {
        if (endOfGame == false)
        {
            yield return new WaitForSeconds(lastWaveToWinTime);
            winText.gameObject.SetActive(true);
            yield return new WaitForSeconds(5.0f);

            SceneManager.LoadScene("LevelSelection");
        }    
    }
}
