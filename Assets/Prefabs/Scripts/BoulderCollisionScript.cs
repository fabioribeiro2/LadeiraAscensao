﻿using UnityEngine;
using System.Collections;

public class BoulderCollisionScript : MonoBehaviour
{
	
	private int boulderCount;
	private float tempPotentialYVelocity;
	private Vector2 tempVelocity;
	private Vector2 tempLocation;
	private int tempBoulderLevel;
	private bool tempGoingRight;
	private GameObject boulder;
	private GameObject boulderClone;

	// Use this for initialization
	void Start ()
	{
		boulderCount = 0;
		boulder = GameObject.Find ("Boulder");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void makeBoulder (Vector2 velocity, Vector2 location, bool goingRight, float potentialYVelocity, int boulderLevel)
	{
		boulderCount++;
		Rigidbody2D rigidbody;
		BoulderScript boulderScript;
		Debug.Log ("velo = " + velocity.y);
		Debug.Log ("pot velo = " + potentialYVelocity);
		if (boulderCount == 2) {
			boulderCount = 0;
			if (boulderLevel > tempBoulderLevel) {
				boulderClone = Instantiate (boulder, new Vector3 (location.x, location.y, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
				rigidbody = boulderClone.GetComponent<Rigidbody2D> ();
				rigidbody.velocity = new Vector2 (goingRight ? velocity.x : -velocity.x, velocity.y);
				boulderScript = (BoulderScript)boulderClone.GetComponent ("BoulderScript");
				boulderScript.potentialYVelocity = potentialYVelocity;
			} else {				
				boulderClone = Instantiate (boulder, new Vector3 (tempLocation.x, tempLocation.y, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
				rigidbody = boulderClone.GetComponent<Rigidbody2D> ();
				rigidbody.velocity = new Vector2 (tempGoingRight ? tempVelocity.x : -tempVelocity.x, tempVelocity.y);
				boulderScript = (BoulderScript)boulderClone.GetComponent ("BoulderScript");
				boulderScript.potentialYVelocity = tempPotentialYVelocity;
			}
			boulderScript.goingRight = rigidbody.velocity.x > 0;
			boulderScript.goingLeft = rigidbody.velocity.x < 0;
			boulderScript.goingUp = rigidbody.velocity.y > 0;
			boulderScript.goingDown = rigidbody.velocity.y < 0;
			if (boulderLevel == tempBoulderLevel) {
				boulderScript.boulderLevel = boulderLevel + 1;
			} else {
				boulderScript.boulderLevel = Mathf.Max(boulderLevel, tempBoulderLevel);
			}
		} else {
			tempLocation = location;
			tempVelocity = velocity;
			tempGoingRight = goingRight;
			tempPotentialYVelocity = potentialYVelocity;
			tempBoulderLevel = boulderLevel;
		}
	}
}