using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Misile : MonoBehaviour {
	public Transform target;
	public float speed = 5f;
	private Rigidbody rb;
	public float rotateSpeed = 200f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUptade(){
		Vector3 direction = target.position - rb.position;
		direction.Normalize ();
		Vector3 rotateAmount = Vector3.Cross(transform.forward, direction );
		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.forward * speed;
	}
}
