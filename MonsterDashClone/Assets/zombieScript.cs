using UnityEngine;
using System.Collections;

public class zombieScript : MonoBehaviour {

	public Texture[] walk;
	
	private Texture[] temp;
	
	private int currentFrame = 0;
	private int animState = -1;
	private bool Loop = true;
	private float FPS = 10;
	private float secondsToWait;	
	
	enemyScript script = null;
	
	Vector3 vel = Vector3.zero;
	
	// Use this for initialization
	void Start () 
	{
		secondsToWait = 1/FPS;
		setAnimState(0,30);
		StartCoroutine(Animate());
		
	}
	
	
	void Update()
	{
		vel.x = -1f;
		rigidbody.velocity = vel;
	}
	
	public IEnumerator Animate()
	{
		bool stop = false;
		
		if ( animState != -1 )
		{
			if(currentFrame >= temp.Length)
			{
				if(Loop == false)
					stop = true;
				else
					currentFrame = 0;
			
				onAnimationEnd(animState);
			}
			renderer.material.mainTexture = temp[currentFrame];
		}
		
		yield return new WaitForSeconds(secondsToWait);
		currentFrame++;
		
		if(stop == false)
			StartCoroutine(Animate());
	}
	
	public void setAnimState(int state , float fps, bool force = false)
	{
		if ( (!force && animState == state)  )
			return;
		
		currentFrame = 0;
		FPS = fps;
		secondsToWait = 1/FPS;
		animState = state;
		
		switch(animState)
		{
			case 0:
				temp = walk;
				break;
			case 1:
				
				break;		
			case 2:
				
				break;
		};
		
	}
	
	void onAnimationEnd(int state)
	{
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if ( collision.gameObject.tag == "wall")
		{
		}
		else if ( collision.gameObject.tag == "bullet" )
		{
			gameObject.collider.enabled = false;
			gameObject.collider.rigidbody.useGravity = false;
			collision.gameObject.collider.enabled = false;
			collision.rigidbody.velocity = new Vector3(0.0f,0.0f,0.0f);
		}
		else if ( collision.gameObject.tag == "player" )
		{
			if ( collision.contacts[0].normal.y <= -0.9f )
			{
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
	}
	
	void OnCollisionStay(Collision collision)
	{
	}
	
	public void reset(Vector3 v)
	{
		gameObject.SetActive(true);
		gameObject.transform.position = new Vector3(v.x,v.y,v.z);
		gameObject.collider.enabled = true;
		gameObject.collider.rigidbody.useGravity = true;
		setAnimState(0,30);
		StartCoroutine(Animate());
		
	}
	
	public void kill()
	{
		gameObject.SetActive(false);
	}
}
