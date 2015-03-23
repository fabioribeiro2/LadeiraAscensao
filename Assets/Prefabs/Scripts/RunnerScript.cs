﻿using UnityEngine;
using System.Collections;

public class RunnerScript : MonoBehaviour
{
	
	public GameObject PlatformPiece;
	public GameObject Boulder;
	private BoulderScript boulderScript;
	private BoulderScript boulderScript2;
	private BoulderScript boulderScript3;

	protected float timeToNextBoulder;

	// Use this for initialization
	void Start ()
	{
		PlatformPiece.transform.position = new Vector2 (20000, 20000);
		Boulder.transform.position = new Vector2 (10000, 10000);
		
		timeToNextBoulder = Random.Range (1,1);
		for (int j=0; j<10; j++) {
			for (int i=0; i<15; i++) {
				if (i != 8) {
					GameObject platformPiece = Instantiate (PlatformPiece, new Vector3 (-48.11f + 11.13f * i, 32.5f - 7.2f * j, 0), new Quaternion()) as GameObject;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeToNextBoulder -= Time.deltaTime;
		if (timeToNextBoulder <= 0) {			
//			GameObject boulder = Instantiate (Boulder, new Vector3 (-45 , 30 , 0), new Quaternion()) as GameObject;
//			Rigidbody2D rigidbody = boulder.GetComponent<Rigidbody2D>();
//			//			Rigidbody2D rigidbody = boulder.rigidbody2D;
//			rigidbody.AddForce(new Vector2(1000,0));
//			boulderScript = (BoulderScript) boulder.GetComponent("BoulderScript");
//			boulderScript.goingDown = true;
//			boulderScript.goingRight = true;
			timeToNextBoulder = Random.Range (9999,9999);
			
			
			GameObject boulder2 = Instantiate (Boulder, new Vector3 (-45 , 21 , 0), new Quaternion()) as GameObject;
			Rigidbody2D rigidbody2 = boulder2.GetComponent<Rigidbody2D>();
			//			Rigidbody2D rigidbody = boulder.rigidbody2D;
			rigidbody2.AddForce(new Vector2(1000,0));
			boulderScript2 = (BoulderScript) boulder2.GetComponent("BoulderScript");
			boulderScript2.goingDown = true;
			boulderScript2.goingRight = true;
			
//			GameObject boulder3 = Instantiate (Boulder, new Vector3 (-45 , 15 , 0), new Quaternion()) as GameObject;
//			Rigidbody2D rigidbody3 = boulder3.GetComponent<Rigidbody2D>();
//			//			Rigidbody2D rigidbody = boulder.rigidbody2D;
//			rigidbody3.AddForce(new Vector2(1000,0));
//			boulderScript3 = (BoulderScript) boulder3.GetComponent("BoulderScript");
//			boulderScript3.goingDown = true;
//			boulderScript3.goingRight = true;
		}
	}
}