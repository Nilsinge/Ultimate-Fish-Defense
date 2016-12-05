using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int speed;
    public int damage;
    public GameObject target;
    public GameObject explosion;

	private void Start () {
        string tag = transform.parent.gameObject.tag;

        if (tag != null)
        {
            if (transform.parent.gameObject.tag == "Enemy")
                target = transform.parent.gameObject.GetComponent<Enemy>().TargetTower;
            else if (transform.parent.gameObject.tag == "Tower")
                target = transform.parent.gameObject.GetComponent<Tower>().TargetEnemy;
        }
    }
	
	private void Update () {
        if(target == null)
        {
            Destroy(gameObject);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (speed * Time.deltaTime));
        }
    }

    private void OnDestroy()
    {
        if (this.explosion != null)
        {
            var explosion = Instantiate(this.explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 1);
        }
    }
}
