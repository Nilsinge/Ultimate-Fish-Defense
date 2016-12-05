using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject projectile;
    public float cooldown = 1.1f;
    public float range = 20;
    public GameObject TargetTower;
    private float nextAttack;
    private bool isTarget;

    public Material exitMaterial;
    public int enemyCashValue;
    private PlayerController playerController;


    private void Start()
    {
        GameObject pc = GameObject.FindGameObjectWithTag("PlayerController");
        playerController = pc.GetComponent<PlayerController>();
    }

    private void Update()
    {
        GameObject closestTower;
        float distance;

        DetectClosestTower(out closestTower, out distance);

        if (closestTower != null)
        {
            Attack(closestTower, distance);
        }
    }

    private void DetectClosestTower(out GameObject closestTower, out float distance)
    {
        closestTower = null;
        distance = 99999999;

        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        for (var i = 0; i < towers.Length; i++)
        {
            float distanceToTower;
            Vector3 turretPosition, enemyPosition;

            turretPosition = transform.position;
            enemyPosition = towers[i].transform.position;

            distanceToTower = Vector3.Distance(enemyPosition, turretPosition);

            if (distanceToTower < distance)
            {
                distance = distanceToTower;
                closestTower = towers[i];
            }
        }
    }

    private void Attack(GameObject closestTower, float distance)
    {
        if (distance <= range && Time.time > nextAttack)
        {
            nextAttack = Time.time + cooldown;

            TargetTower = closestTower;

            if (TargetTower != null)
            {
                GameObject myProjectile = (GameObject)Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
                myProjectile.transform.parent = this.transform;
            }   
        }
    }

    public void ChangeMaterial()
    {
        var rend = gameObject.GetComponent<Renderer>();
        rend.material = exitMaterial;
    }

    private void OnMouseEnter()
    {
        playerController.MouseTransformCrosshair();
        isTarget = true;
    }

    private void OnMouseExit()
    {
        playerController.MouseTransformBack();
        isTarget = false;
    }

    private void OnDestroy()
    {
        if (isTarget == true)
        {
            playerController.MouseTransformBack();
        }
    }

    private void OnMouseDown()
    {
        playerController.Bombard(transform);
    }

}

