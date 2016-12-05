using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tower : MonoBehaviour {

    public GameObject projectile;
    public GameObject TargetEnemy;

    public Material OriginalMaterial;
    public Material SelectedMaterial;
    public Material UpgradedMaterial;

    public Button repairButton;
    public Button upgradeButton;

    public Vector3 upgradeScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float upgradeFactor = 1.5f;
    public int upgrade_hp = 150;

    public bool isSelected;
    public static bool anyTower_isSelected = false;
    public bool isUpgraded = false;

    public float cooldown = 0.5f;
    public float range = 10;
    private float nextAttack;
    public float shotHeight = 200;
    public bool doubleShooter = false;

    public int towerCost = 150;
    public static int repairCost = 75;
    public static int upgradeCost = 125;

    private ResourceManager resourceManager;
    private PlayerController playerController;


    private void Start()
    {
        isSelected = false;

        repairButton = GameObject.Find("BtnRepair").GetComponent<Button>();
        upgradeButton = GameObject.Find("BtnUpgrade").GetComponent<Button>();

        repairButton.onClick.AddListener(
            delegate { Repair(); });

        upgradeButton.onClick.AddListener(
            delegate { Upgrade(); });

        GameObject rm = GameObject.FindGameObjectWithTag("ResourceManager");
        resourceManager = rm.GetComponent<ResourceManager>();

        GameObject pc = GameObject.FindGameObjectWithTag("PlayerController");
        playerController = pc.GetComponent<PlayerController>();

        var rend = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rend)
        {
            r.material = OriginalMaterial;
        }
    }

    private void Update()
    {
        GameObject closestEnemy;
        float distance;

        DetectClosestEnemy(out closestEnemy, out distance);

        if (closestEnemy != null)
        {
            Quaternion rotation = TurretRotation(closestEnemy, distance);
            Attack(closestEnemy, distance, rotation);
        }
        if (Input.GetMouseButton(0))
            StartCoroutine(HandleSelection());
    }

    public IEnumerator HandleSelection()
    {
        bool isSelectedNew =  playerController.ClickedOnTower(gameObject, upgradeButton);

        if (isSelectedNew == false && anyTower_isSelected == false)
        {
            yield return new WaitForSeconds(0.20f);
            ChangeMaterial(false);
            isSelected = false;
        }
        else if (isSelectedNew == false && anyTower_isSelected == true)
        {
            yield return new WaitForSeconds(0.20f);

            ChangeMaterial(false);
            isSelected = false;
        }

        else if (isSelectedNew == true)
        {
            yield return new WaitForSeconds(0.20f);
            ChangeMaterial(true);
            isSelected = true;
            anyTower_isSelected = true;
        }   
    }

    private void DetectClosestEnemy(out GameObject closestEnemy, out float distance)
    {
        closestEnemy = null;
        distance = 99999999;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy;
            Vector3 turretPosition, enemyPosition;

            turretPosition = transform.position;
            enemyPosition = enemies[i].transform.position;

            distanceToEnemy = Vector3.Distance(turretPosition, enemyPosition);

            if (distanceToEnemy < distance)
            {
                distance = distanceToEnemy;
                closestEnemy = enemies[i];
            }
        }
    }

    private void Upgrade()
    {
        if (isUpgraded == false && isSelected == true)
        {
            bool canAfford = resourceManager.SubtractCash(upgradeCost);

            if (canAfford == true)
            {
                transform.localScale += upgradeScale;
                range *= upgradeFactor;
                cooldown *= 0.8f;
                this.transform.GetComponent<DamageDetection>().upgradeHealth(upgrade_hp);
                isUpgraded = true;
            }
        }
    }

    private void Repair()
    {
        if (this.transform.GetComponent<DamageDetection>().isDamaged == true && isSelected == true)
        {
            bool canAfford = resourceManager.SubtractCash(repairCost);

            if (canAfford == true)
            {
                this.transform.GetComponent<DamageDetection>().restoreHealth();
            }
        }
    }

    private void ChangeMaterial(bool change)
    {
        var rend = transform.gameObject.GetComponentsInChildren<Renderer>();

        if (change == true)
        {
            foreach(Renderer r in rend)
            {
                r.material = SelectedMaterial;
            }
        }
        if (change == false && isUpgraded == false)
        {
            foreach (Renderer r in rend)
            {
                r.material = OriginalMaterial;
            }
        }
        if (change == false && isUpgraded == true)
        {
            foreach (Renderer r in rend)
            {
                r.material = UpgradedMaterial;
            }
        }
    }

    private Quaternion TurretRotation(GameObject closestEnemy, float distance)
    {
        Vector3 directionToEnemy = closestEnemy.transform.position - transform.position;
        Quaternion rotationAngle = Quaternion.LookRotation(directionToEnemy);

        Transform headPart = null;
        headPart = transform.Find("Head");

        headPart.rotation = Quaternion.Euler(0, rotationAngle.eulerAngles.y, 0);

        return rotationAngle;
    }

    private void Attack(GameObject closestEnemy, float distance, Quaternion rotationAngle)
    {
        float zAdjustment = 1.25f;

        if (distance <= range && Time.time > nextAttack)
        {
            nextAttack = Time.time + cooldown;
            
            TargetEnemy = closestEnemy;

            if (TargetEnemy != null)
            {
                if (!doubleShooter)
                {
                    GameObject myProjectile = (GameObject)Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + shotHeight, transform.position.z + zAdjustment), Quaternion.identity);
                    myProjectile.transform.parent = this.transform;
                }
                else
                {
                    GameObject myProjectile1 = (GameObject)Instantiate(projectile, new Vector3(transform.position.x - 0.6f, transform.position.y + shotHeight, transform.position.z + zAdjustment), Quaternion.identity);
                    myProjectile1.transform.parent = this.transform;

                    GameObject myProjectile2 = (GameObject)Instantiate(projectile, new Vector3(transform.position.x + 0.6f, transform.position.y + shotHeight, transform.position.z + zAdjustment), Quaternion.identity);
                    myProjectile2.transform.parent = this.transform;
                }
            }
        }
    }
}

