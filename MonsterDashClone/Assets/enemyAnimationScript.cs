using UnityEngine;
using System.Collections;

public class enemyAnimationScript : MonoBehaviour {

	public float FPS;
	private float secondsToWait;
	public bool Loop;
	public Texture[] walk;
	public Texture[] fall;
	public Texture[] death;
	
	private Texture[] temp;
	
	private int currentFrame;
	private int animState = 0;
	
	enemyScript script = null;
	
	// Use this for initialization
	void Start () 
	{
	
		load();
		
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
		if ( (!force && animState == state) || !script.isAlive  )
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
				temp = fall;
				break;		
			case 2:
				temp = death;
				break;
		};
		
	}
	
	void onAnimationEnd(int state)
	{
		if ( state == 2 )
		{
			script.kill();
		}
	}
	
	public void reset()
	{
		StartCoroutine(Animate());
	}
	
	public void load()
	{
		currentFrame = 0;
		secondsToWait = 1/FPS;
		temp = null;
		animState = -1;
		Loop = true;
		script = gameObject.GetComponent<enemyScript>();
	}
}
