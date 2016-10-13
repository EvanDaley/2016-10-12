using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	private NavMeshRunner navMeshRunner;
	private CombatScript combatScript;

	public float maxHealth = 100f;
	private float health = 100;
	public float regen = .2f;
	public Rigidbody rbody;

	bool dead = false;

	void Start () {
		navMeshRunner = GetComponent<NavMeshRunner> ();
		combatScript = GetComponent<CombatScript> ();
		health = maxHealth;

		rbody = GetComponent<Rigidbody> ();
	}

	public float Health
	{
		get { return health; }
	}

	/// <summary>
	/// Gets the health on a scale of 0 to 1.
	/// </summary>
	/// <value>The health normalized.</value>
	public float HealthNormalized
	{
		get { return health / maxHealth; }
	}

	void Update()
	{
		if (health < 1)
		{
			return;
		}

		if (health < maxHealth)
		{
			health += regen * Time.deltaTime;
		}

		if (health > maxHealth)
		{
			health = maxHealth;
		}
	}

	public void Damage(float damage)
	{
		health -= damage;

		if (health < 1)
			DieNow ();
	}

	public void SetMoveTarget(Vector3 position)
	{
		navMeshRunner.SetTarget (position);
	}

	public void Attack(Vector3 position)
	{
		combatScript.Attack (position);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Attack")
		{
			print ("hit");
			Damage (4f);
		}
	}

	void DieNow()
	{
		if (dead)
			return;

		dead = true;

		Invoke ("Restart", 4f);
		rbody.isKinematic = false;

		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.enabled = false;

		BroadcastMessage ("Die");
	}

	void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
