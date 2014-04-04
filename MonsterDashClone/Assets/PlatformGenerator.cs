using UnityEngine;
using System.Collections;


public class PlatformGenerator : MonoBehaviour {
	
	public GameObject wall = null;
	public GameObject[] collection;
	public float distance = 2.0f;
	public float vDistance = 1.0f;
	
	public GameObject collide = null;
	public GameObject[] collection2; 
	
	
	private float xGenerate = -10.0f;
	// Use this for initialization
	void Start () {
	
		float x = 6.0f;
		for ( int i=0; i < 10; i++)
		{
			collection[i] = (GameObject)Instantiate(wall,new Vector3(x,-3.5f,0.0f),Quaternion.identity);
			collection[i].SetActive(false);
			
		}
		
		for ( int i=0; i < 10; i++)
		{
			collection2[i] = (GameObject)Instantiate(collide,new Vector3(x,0.0f,0.0f),Quaternion.identity);
			collection2[i].SetActive(false);
			
		}		
		
	}
	
	bool createPlatform(float xOffset, float yOffset, int number )
	{
		float x = 0.0f;
		bool status = false;
		int n = 1;
		float sx = -16.0f;
		float _x = xOffset,_y=-4.5f+yOffset;
		
		// check if free objects are available
		int count = 0;
		for ( int i=0; i < 10; i++)
		{
			if ( !collection[i].activeSelf )
				count++; 
		}
		
		if ( count < number )
			return false;
		
		
		for ( int i=0; i < 10; i++)
		{
			if ( collection[i].activeSelf == false && n <= number)
			{
//				collection[i] = (GameObject)Instantiate(wall,new Vector3(xOffset+x,-4.5f+yOffset,0.0f),Quaternion.identity);
				collection[i].SetActive(true);
				Transform transform = collection[i].transform;
				collection[i].transform.position = new Vector3(xOffset+x,-9.5f+yOffset,0.0f);
				collection[i].transform.localScale = new Vector3(sx, transform.localScale.y, transform.localScale.z);					
				
				x += 16.0f;
				n++;
				sx*=-1.0f;
				
				xGenerate+=16;
				_y = collection[i].transform.position.y;
				
			}
				
		}
		if ( (n-1) == number )
		{
			status = true;
			//Debug.Log("S " + xOffset + " " +  (-4.5f+yOffset));
			for ( int i=0; i < 10; i++)
			{
				if ( collection2[i].activeSelf == false)
				{
					_x+=8;
					//Debug.Log("x " + _x  + " y " + _y);
					
					collection2[i].transform.position = new Vector3((_x),_y,0.0f);
					collection2[i].SetActive(true);
					
					Vector3 p1 = new Vector3(collection2[i].transform.position.x - 8,collection2[i].transform.position.y,0.0f);
					Vector3 p2 = new Vector3(collection2[i].transform.position.x + 8,collection2[i].transform.position.y,0.0f);
					//Debug.DrawLine (p1, p2, Color.red);
					break;
				
				}
			}
		}
		
		 return status;
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject g = GameObject.FindWithTag("player");
		
		playerScript ps = (playerScript)g.GetComponent("playerScript");
		distance = (ps.moveSpeed)/5.0f;
		
		
		int count = 2;
		float height = Random.Range(-2.0f,1.0f);
		if ( createPlatform(xGenerate,height,count) )
		{
			xGenerate+=distance;
		}
	}
}
