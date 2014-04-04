using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

	public float FPS;
	public Texture[] frames;
	
	private int currentFrame;
	bool Loop;
	private float secondsToWait;
	// Use this for initialization
	void Start () 
	{
	
		load();
		
	}
	
	public IEnumerator Animate()
	{
		bool stop = false;
		
		if(currentFrame >= frames.Length)
		{
			if(Loop == false)
				stop = true;
			
			currentFrame = 0;
			onAnimationEnd();
		}
		renderer.material.mainTexture = frames[currentFrame];
		
		yield return new WaitForSeconds(secondsToWait);
		currentFrame++;
		
		if(stop == false)
			StartCoroutine(Animate());
	}
	
	void onAnimationEnd()
	{
		gameObject.SetActive(false);
	}
	
	public void reset(Vector3 v)
	{
		currentFrame = 0;
		gameObject.SetActive(true);
		StartCoroutine(Animate());
		gameObject.transform.position = new Vector3(v.x,v.y,v.z);
		collider.enabled = true;
	}
	
	public void load()
	{
		currentFrame = 0;
		secondsToWait = 1/FPS;
		Loop = false;
	}
}
