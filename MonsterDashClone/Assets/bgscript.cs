using UnityEngine;
using System.Collections;

public class bgscript : MonoBehaviour {
	
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
	private Vector2 uvOffset = Vector2.zero;
	// Use this for initialization
	void Start () {
	

			Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject g = GameObject.FindWithTag("player");
		transform.position = new Vector3(transform.position.x,3.0f,100.0f);
	}
	
	void LateUpdate() 
    {
				
		uvOffset -= ( uvAnimationRate * 0.001f );
        //if( renderer.enabled )
        {
            renderer.material.SetTextureOffset("_MainTex",uvOffset);
        }
		
	}
}
