using UnityEngine;
using System.Collections;

public class flyEnemyScript : MonoBehaviour {

	// Use this for initialization
	int state = 0;
	float distance = 0f;
	float attackTimer = 0.0f;
	void Start () {
	
		rigidbody.velocity = new Vector3(2f,3.0f,0.0f);
		attackTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( transform.position.y < -8f )
			gameObject.SetActive(false);
	}
	
	void FixedUpdate()
	{
		if ( transform.position.y > 6.0f && state == 0)
		{
			//rigidbody.AddForce(new Vector3(10f,0f,0f),ForceMode.Impulse);
			GameObject g = GameObject.FindWithTag("player");
			state = 1;
			rigidbody.velocity = new Vector3(10.0f,0.0f,0.0f);
			distance = Mathf.Abs(transform.position.x - g.transform.position.x);
			Debug.Log(Camera.main.WorldToViewportPoint(g.transform.position));
			
		}
		
		if ( state == 2 )
		{
			GameObject g = GameObject.FindWithTag("player");
			Vector3 dir = g.transform.position - transform.position;
			dir.Normalize();
			dir *= 1000.0f;
			
			rigidbody.AddForce(dir,ForceMode.Acceleration);
			state = 3;
		}

	}
	
	void LateUpdate()
	{
		if ( state == 1 )
		{
			GameObject g = GameObject.FindWithTag("player");
			transform.position = new Vector3(g.transform.position.x+distance,transform.position.y,transform.position.z);
			
			attackTimer+=Time.deltaTime;
			if ( attackTimer >= 3f )
				state = 2;
		}	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if ( collision.gameObject.tag == "player" )
		{
			gameObject.SetActive(false);
		}
	}
}
