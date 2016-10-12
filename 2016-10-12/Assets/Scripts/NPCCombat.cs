using UnityEngine;
using System.Collections;

public class NPCCombat : MonoBehaviour {

	public Tag myTag;
	public Tag targetTag;
	public GameObject spellPrefab;
	public GameObject spellInstance;

	public float shotRate = .3f;
	private float shotCooldown = 0;

	void DetectObject(object gameObject)
	{
		GameObject go = gameObject as GameObject;
		if (go != null)
		{
			Tag otherTag = go.GetComponent<Tag> ();
			if (otherTag != null)
			{
				if (otherTag.teamNumber != myTag.teamNumber)
				{
					print ("found player");
					targetTag = otherTag;
				}
			}
		}
	}

	void Update()
	{

		if (targetTag != null)
		{
			if (Time.time > shotCooldown)
			{
				Vector3 attackStart = Vector3.MoveTowards (transform.position, targetTag.transform.position, 2f);
				attackStart += Random.onUnitSphere;
				spellInstance = GameObject.Instantiate (spellPrefab, attackStart, transform.rotation) as GameObject;
				BasicAttack attack = spellInstance.GetComponent<BasicAttack> ();

				if (attack != null)
					attack.SetTarget (targetTag);

				shotCooldown = Time.time + shotRate;
			}
		}

	}
}
