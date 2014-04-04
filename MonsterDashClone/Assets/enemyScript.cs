using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {
	
	Rigidbody r = null;
	enemyAnimationScript script;
	
	bool onGround = false;
	public bool isAlive = false;
	// Use this for initialization
	void Start () {
		r =  GetComponent<Rigidbody>();
		script = GetComponent<enemyAnimationScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( !isAlive) return;
		
		if ( gameObject.transform.position.y < - 10.0f )
			kill();
		
		if ( onGround )
		{
			script.setAnimState(0,10);
			r.velocity = new Vector3(-3.5f,r.velocity.y,0.0f);
		}
		else
			script.setAnimState(1,20);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if ( collision.gameObject.tag == "wall")
		{
			if( collision.contacts[0].normal.y == 1.0f) 
			{
				onGround = true;
			}
		}
		else if ( collision.gameObject.tag == "bullet" )
		{
			script.setAnimState(2,40);
			isAlive = false;
			gameObject.collider.enabled = false;
			gameObject.collider.rigidbody.useGravity = false;
			collision.gameObject.collider.enabled = false;
			collision.rigidbody.velocity = new Vector3(0.0f,0.0f,0.0f);
		}
		else if ( collision.gameObject.tag == "player" )
		{
			Debug.Log(collision.contacts[0].normal);
			if ( collision.contacts[0].normal.y <= -0.9f )
			{
				Debug.Log("kill");
				script.setAnimState(2,40);
				isAlive = false;
				gameObject.collider.enabled = false;
				gameObject.collider.rigidbody.useGravity = false;
			}
			else
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	
	void OnCollisionExit(Collision collision)
	{
		if ( collision.gameObject.tag == "wall" )
		{
			onGround = false;
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
		if ( collision.gameObject.tag == "wall" )
		{
			onGround = true;
		}
	}
	
	public void reset(Vector3 v)
	{
		gameObject.SetActive(true);
		gameObject.transform.position = new Vector3(v.x,v.y,v.z);
		isAlive = true;
		script.reset();
		gameObject.collider.enabled = true;
		gameObject.collider.rigidbody.useGravity = true;
		
	}
	
	public void kill()
	{
		isAlive = false;
		gameObject.SetActive(false);
	}
	
	public void load()
	{
		r =  GetComponent<Rigidbody>();
		script = GetComponent<enemyAnimationScript>();
		script.load();
	}
}
