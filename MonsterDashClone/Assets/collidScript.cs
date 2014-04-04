using UnityEngine;
using System.Collections;

public class collidScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		
		float minX = cam.transform.position.x - width/2.0f;
		
		BoxCollider r = GetComponent<BoxCollider>();
		float _x = transform.position.x + Mathf.Abs(r.size.x)/2.0f;
		if ( _x < minX )
		{
			
			gameObject.SetActive(false);
		}
	}
}
