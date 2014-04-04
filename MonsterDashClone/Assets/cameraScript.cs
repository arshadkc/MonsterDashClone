using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void LateUpdate()
	{
		GameObject g = GameObject.FindWithTag("player");
		transform.position = new Vector3(transform.position.x,0f,transform.position.z);		
	}
}
