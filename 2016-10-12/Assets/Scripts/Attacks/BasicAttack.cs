using UnityEngine;
using System.Collections;

public class BasicAttack : MonoBehaviour {

	public Rigidbody rbody;
	public float moveForce = 100;
	Tag targetTag;

	public void SetTarget(Vector3 position)
	{
		Vector3 direction = (position - transform.position).normalized;
		rbody.AddForce (direction * moveForce);
	}

	public void SetTarget(Tag targetTag)
	{
		this.targetTag = targetTag;
	}

	void Update()
	{
		if (targetTag != null)
		{
			Vector3 direction = (targetTag.transform.position - transform.position).normalized;
			rbody.AddForce (direction * moveForce * Time.deltaTime);
		}
	}
}
