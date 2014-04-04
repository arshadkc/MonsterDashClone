using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
	
	public float jumpSpeed = 20.0f;
	public float moveSpeed = 10.0f;
	public float delta = 1.0f;
	public bool falling = false;
	private SpriteAnimation script;
	private bool onGround = false;
	
	private bool isJumping = false;
	private float jumpTimer = 0.0f;
	
	Rigidbody r;
	
	public GameObject bullet = null;
	public GameObject[] bulletCollection = null;
	public float bulletSpeed = 0.0f;
	// Use this for initialization
	void Start () {
	
		r =  GetComponent<Rigidbody>();
		script = GetComponent<SpriteAnimation>();
		
		for ( int i=0; i < 10; i++)
		{
			bulletCollection[i] = (GameObject)Instantiate(bullet,new Vector3(0.0f,0.0f,-0.1f),Quaternion.identity);
			bulletCollection[i].GetComponent<bulletScript>().load();
			bulletCollection[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( gameObject.transform.position.y < - 10.0f )
			 Application.LoadLevel(Application.loadedLevel);
		
		if ( falling && !isJumping) return;
		
		
		
		r.velocity = new Vector3(moveSpeed,r.velocity.y,0.0f);
		moveSpeed+=delta;
		
		if ( Input.GetKeyDown(KeyCode.Space ) && onGround)
		{
			jump();
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began)
			{
				if ( touch.position.x < 480 )
				{
					if (onGround)
               			jump();
				}
				else
					fire();
			}
        }
		
		if ( isJumping )
		{
			jumpTimer+=Time.deltaTime;
			if ( ( Input.GetKey(KeyCode.Space ) || Input.touches.GetLength(0) > 0 ) && jumpTimer < 0.5f )
			{
				r.velocity = new Vector3(r.velocity.x,r.velocity.y+0.5f,r.velocity.z);
			}
			else
			{
				isJumping = false;
				jumpTimer = 0.0f;
			}
		}
		
		if ( Input.GetKeyDown(KeyCode.S))
		{
			fire();
		}
		
            
	}
	
	void jump()
	{
		isJumping = true;
		r.velocity = new Vector3(r.velocity.x,jumpSpeed,r.velocity.z);
		script.setAnimState(1,8);
	}
	
	void fire()
	{
		script.setAnimState(2,25);
		for ( int i=0; i < 10; i++)
		{
			if ( bulletCollection[i].activeSelf == false )
			{
				bulletCollection[i].GetComponent<bulletScript>().reset(new Vector3((gameObject.transform.position.x+ Mathf.Abs(transform.localScale.x/2.0f))+0.5f,gameObject.transform.position.y-0.5f,bulletCollection[i].transform.position.z));
				bulletCollection[i].GetComponent<Rigidbody>().velocity = new Vector3((moveSpeed+bulletSpeed),0.0f,0.0f);
				break;
			}
		}		
	}
	
	void OnCollisionEnter(Collision collision) {
		
		
		if ( collision.gameObject.tag == "wall" )
		{
		
			script.setAnimState(0,25);
			onGround = true;
			if ( collision.contacts[0].normal.y < 0.001f )
			{
				falling = true;
				//Debug.Log(collision.contacts[0].normal);
			}
			
		}
	}
	
	void OnCollisionExit(Collision collision) {
		
		
		if ( collision.gameObject.tag == "wall" )
		{
			onGround = false;
		}
	}
	
	void OnCollisionStay(Collision collision) {
		
		
		if ( collision.gameObject.tag == "wall" )
		{
			onGround = true;
		}
	}
}
