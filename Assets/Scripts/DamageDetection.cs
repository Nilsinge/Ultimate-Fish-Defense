using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageDetection : MonoBehaviour {

    public int hpMax;
    public int hpCurrent;
    public Image hpBar;
    public bool isDamaged = false;
    public GameObject explosion;
    private ResourceManager resourceManager;


    private void Start()
    {
        hpCurrent = hpMax;

        GameObject rm = GameObject.FindGameObjectWithTag("ResourceManager");
        resourceManager = rm.GetComponent<ResourceManager>(); 
    }

    private void Update()
    {
        if (hpCurrent <= 0)
        {
            Destruct();
        }

        UpdateHpBar();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Projectile" && transform.tag != otherCollider.gameObject.transform.parent.tag)
        {
            Projectile projectile = (Projectile)otherCollider.gameObject.GetComponent(typeof(Projectile));
            int damage = projectile.damage;
            Destroy(otherCollider.gameObject);

            hpCurrent = hpCurrent - damage;
            isDamaged = true;
        }
    }

    public void upgradeHealth(int health)
    {
        hpMax += health;
        hpCurrent = hpMax; 
    }

    public void restoreHealth()
    {
        hpCurrent = hpMax;
        isDamaged = false;
    }

    private void Destruct()
    {
        if (this.gameObject.tag == "Tower")
        {
            transform.parent.GetComponent<PlacementController>().TowerDestroyed();
        }

        if (this.gameObject.tag == "Enemy")
        {
            int enemyValue = transform.GetComponent<Enemy>().enemyCashValue;
            resourceManager.AddCash(enemyValue);
        }

        Destroy(this.gameObject);
        GameObject expl = (GameObject) Instantiate(explosion, transform.position, transform.rotation);
        Destroy(expl, 1);
    }

    private void UpdateHpBar()
    {
        var hpCurrentFloat = (float) hpCurrent;
        var hpMaxFloat = (float)hpMax;

        hpBar.fillAmount = hpCurrentFloat / hpMaxFloat;
    }
}
