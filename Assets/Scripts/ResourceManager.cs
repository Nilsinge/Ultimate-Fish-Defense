using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {

    public int cash;
    public GUIText cashText;
    public GUIText noCashWarning;
    private bool isWarned = true;

	private void Start () {
        noCashWarning.gameObject.SetActive(false);
        cashText.text = "Money: " + " $" + cash ;
    }
	
	private void Update () {
        cashText.text = "Money: " + " $" + cash;

        if (noCashWarning.gameObject.activeSelf == true && isWarned == true)
        {
            isWarned = false;
            Invoke("Delay", 2f);
        }
    }

    public void AddCash(int cashToAdd)
    {
        cash += cashToAdd;
    }

    public bool SubtractCash(int cashToSubtract)
    {
        bool canAfford;
        if(cashToSubtract <= cash)
        {
            cash -= cashToSubtract;
            canAfford = true;        
        }
        else
        {
            noCashWarning.gameObject.SetActive(true);
            canAfford = false;
        }

        return canAfford;
    }

    public void Delay()
    {
        isWarned = true;
        noCashWarning.gameObject.SetActive(false);
    }
}
