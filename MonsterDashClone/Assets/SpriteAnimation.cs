using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
	
	public Texture[] walk;
	public Texture[] jump;
	public Texture[] shoot;
	
	private Texture[] temp;
	
	private int currentFrame = 0;
	private int animState = -1;
	private bool Loop = true;
	private float FPS = 10;
	private float secondsToWait;	
	
	enemyScript script = null;
	
	// Use this for initialization
	void Start () 
	{
		secondsToWait = 1/FPS;
		StartCoroutine(Animate());
		
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
				temp = jump;
				break;		
			case 2:
				temp = shoot;
				break;
		};
		
	}
	
	void onAnimationEnd(int state)
	{
		if ( state == 2 )
		{
			setAnimState(0,25);
		}
	}
}