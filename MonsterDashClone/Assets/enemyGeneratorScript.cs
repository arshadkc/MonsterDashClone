using UnityEngine;
using System.Collections;

public class enemyGeneratorScript : MonoBehaviour {
	
	public GameObject enemy = null;
	
	public GameObject[] enemyCollection;
	public float genTime = 5.0f;
	public float distance = 10.0f;
	
	private float genTimer = 0.0f;
	private GameObject player;
	private float randomTimer = 0.0f;
	float totalTime = 0.0f;
	
	// Use this for initialization
	void Start () {
		
		for ( int i=0; i < 10; i++)
		{
			enemyCollection[i] = (GameObject)Instantiate(enemy,new Vector3(0.0f,0.0f,-0.1f),Quaternion.identity);
			enemyCollection[i].GetComponent<enemyScript>().load();
			enemyCollection[i].GetComponent<enemyScript>().kill();
		}
		
		player = GameObject.FindWithTag("player");
		randomTimer = genTimer;
	}
	
	// Update is called once per frame
	void Update () {
		
		totalTime+=Time.deltaTime;
		genTimer+=Time.deltaTime;
		if ( genTimer >= randomTimer )
		{
			generateEnemy();
			randomTimer = genTime + Random.Range(-0.5f,0.5f);
			genTimer = 0.0f;
			
		}
	}
	
	void generateEnemy()
	{
		int count = 0;
		int  max = 1;
		
		if ( totalTime > 20.0f && Random.value > 0.5f )
			max = 2;
		
		float xPos = distance + Random.Range(-5.0f,5.0f);
		for ( int i=0; i < 10; i++)
		{
			if ( enemyCollection[i].activeSelf == false )
			{
				
				enemyCollection[i].GetComponent<enemyScript>().reset(new Vector3((player.transform.position.x+xPos),8.0f,enemyCollection[i].transform.position.z));
				count++;
				xPos += Random.Range(3,8);
				if ( count == max )
					break;
			}
		}
	}
}
