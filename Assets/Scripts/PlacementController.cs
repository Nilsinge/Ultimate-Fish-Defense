using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlacementController : MonoBehaviour {

    public bool occupied;
    private GameObject selectedTowerType;
    private ResourceManager resourceManager;
    private TowerSelector towerSelector;


    public void Start()
    {
        GameObject rm = GameObject.FindGameObjectWithTag("ResourceManager");
        resourceManager = rm.GetComponent<ResourceManager>();

        GameObject ts = GameObject.FindGameObjectWithTag("TowerSelector");
        towerSelector = ts.GetComponent<TowerSelector>();

        occupied = false;
    }

    public void Update() {}

    public void OnMouseUp()
    {
        GameObject SelectedTower = towerSelector.selectedTowerType;

        if (occupied == false)
        {
            TowerBuilder(SelectedTower);
        }
    }

    public void TowerBuilder(GameObject tower)
    {
        int towerCost = tower.gameObject.GetComponent<Tower>().towerCost;
        bool canAfford = resourceManager.SubtractCash(towerCost);

        if (canAfford == true)
        {
            GameObject newTower = (GameObject)Instantiate(tower, transform.position, transform.rotation);
            newTower.transform.parent = this.transform;
            occupied = true;
        }
        else
        {
            return;
        }  
    }

    public void TowerDestroyed()
    {
        occupied = false;
    }
}
