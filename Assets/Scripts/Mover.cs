using UnityEngine;
using System.Collections;


public class Mover : MonoBehaviour {

    public float speed;
    public Rigidbody body;

    
    public void Start()
    {
        if(transform.gameObject.tag == "FakeShark")
        {
            body = GetComponent<Rigidbody>();
            body.velocity = transform.forward * speed;

        } else
        {
            body = GetComponent<Rigidbody>();
            body.velocity = transform.right * -speed;
        }       
    }
}
	
	
