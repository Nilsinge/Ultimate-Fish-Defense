using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerSelector : MonoBehaviour {

    public GameObject selectedTowerType;
    private Tower towerBehaviour;
    public Color oldColour;
    public Color newColour;

    public Button tower1_Btn;
    public Button tower2_Btn;
    public Button tower3_Btn;

    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;

    public void Start()
    {
        ColorBlock cb = tower1_Btn.colors;
        cb.normalColor = newColour;
        tower1_Btn.colors = cb;
  
        int t1Cost =  tower1.GetComponent<Tower>().towerCost;
        int t2Cost =  tower2.GetComponent<Tower>().towerCost;
        int t3Cost = tower3.GetComponent<Tower>().towerCost;

        tower1_Btn.GetComponentInChildren<Text>().text += " ($" + t1Cost + ")";
        tower2_Btn.GetComponentInChildren<Text>().text += " ($" + t2Cost + ")";
        tower3_Btn.GetComponentInChildren<Text>().text += " ($" + t3Cost + ")";
    }

    public void changeSelected(GameObject tower)
    {
        if (tower != null)
        {
            selectedTowerType = tower;
            RevertColour();

            ColourPressed(selectedTowerType.transform.name);
        }
    }

    private void RevertColour()
    {
        ColorBlock cb1 = tower1_Btn.colors;
        cb1.normalColor = oldColour;
        tower1_Btn.colors = cb1;

        ColorBlock cb2 = tower2_Btn.colors;
        cb2.normalColor = oldColour;
        tower2_Btn.colors = cb2;

        ColorBlock cb3 = tower3_Btn.colors;
        cb3.normalColor = oldColour;
        tower3_Btn.colors = cb3;
    }

    private void ColourPressed(string towerTypeName)
    {

        if (towerTypeName == tower1.name)
        {
            ColorBlock cb = tower1_Btn.colors;
            cb.normalColor = newColour;
            tower1_Btn.colors = cb;
        }
        else if (towerTypeName == tower2.name)
        {
            ColorBlock cb2 = tower2_Btn.colors;
            cb2.normalColor = newColour;
            tower2_Btn.colors = cb2;
        }
        else if (towerTypeName == tower3.name)
        {
            ColorBlock cb3 = tower3_Btn.colors;
            cb3.normalColor = newColour;
            tower3_Btn.colors = cb3;
        }
    }
}
