using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    private GameObject gameObjectHit;
    public Texture2D crosshair;
    public GameObject projectile;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 spot = Vector2.zero;
    public float cooldown = 5.0f;
    private bool isCoolingDown = true;
    public Image shotBar;
    public Text shotReady;


    private void Start()
    {
        shotReady.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isCoolingDown == true)
        {
            shotBar.fillAmount += 1.0f / cooldown * Time.deltaTime;
        }

        if (shotBar.fillAmount == 1.0f && isCoolingDown == true)
        {
            shotReady.gameObject.SetActive(true);
            isCoolingDown = false;
        }
    }

    public bool ClickedOnTower(GameObject didYouHitMe, Button button)
    {
        bool hitStatus = false;

        {
            Ray towerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(towerRay, out hit))
            {
                gameObjectHit = hit.transform.gameObject;

                if (gameObjectHit == didYouHitMe && gameObjectHit.transform.tag == "Tower")
                {
                    hitStatus = true;
                }
                else
                {
                    hitStatus = false;
                }
            }
            return hitStatus;
        }
    }  

    public void MouseTransformCrosshair()
    {
        Cursor.SetCursor(crosshair, spot, cursorMode);
    }

    public void MouseTransformBack()
    {
       Cursor.SetCursor(null, Vector2.zero, cursorMode);     
    }

    public void Bombard(Transform transform)
    {
        if (shotBar.fillAmount == 1.0f)
        {
            GameObject playerProjectile = (GameObject)Instantiate(projectile, new Vector3(transform.position.x - 20, transform.position.y + 40, transform.position.z), Quaternion.identity);
            playerProjectile.transform.parent = this.transform;

            playerProjectile.GetComponent<Projectile>().target = transform.gameObject;

            shotBar.fillAmount = 0.0f;

            isCoolingDown = true;
            shotReady.gameObject.SetActive(false);
        }
    }
}
