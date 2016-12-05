using UnityEngine;
using System.Collections;

public class ShipExit : MonoBehaviour {

    private GameController controller;


    private void Start()
    {
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        controller = gc.GetComponent<GameController>();
    }

    public void OnTriggerExit(Collider otherCollider)
    {
        if(otherCollider.gameObject.tag == "Enemy")
            Destroy(otherCollider.gameObject);
    }

    public void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Enemy")
        {
            otherCollider.transform.gameObject.GetComponent<DamageDetection>().hpCurrent = 99999;
            otherCollider.transform.gameObject.GetComponent<Enemy>().ChangeMaterial();

            controller.ShipGotThrough();
        }
    }
}
