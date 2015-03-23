using UnityEngine;
using System.Collections;

public class BoulderCollisionScript : MonoBehaviour
{

	private int boulderCount;
	private Vector2 tempVelocity;
	private Vector2 tempLocation;
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

	public void makeBoulder (Vector2 velocity, Vector2 location, bool goingRight)
	{
		boulderCount++;
		if (boulderCount == 2) {
			boulderCount = 0;
			if (Mathf.Abs (velocity.y) > Mathf.Abs (tempVelocity.y)) {
				boulderClone = Instantiate (boulder, new Vector3 (location.x, location.y, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
			} else {				
				boulderClone = Instantiate (boulder, new Vector3 (tempLocation.x, tempLocation.y, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
			}
			Rigidbody2D rigidbody = boulderClone.GetComponent<Rigidbody2D> ();
//			Debug.Log ("x1 = " + velocity.x + " x2 = " + tempVelocity.x);
			if (Mathf.Abs (velocity.y) > Mathf.Abs (tempVelocity.y)) {
				rigidbody.velocity = new Vector2 (goingRight ? velocity.x : -velocity.x, velocity.y);
			} else {							
				rigidbody.velocity = new Vector2 (tempGoingRight ? tempVelocity.x : -tempVelocity.x, tempVelocity.y);
			}
			BoulderScript boulderScript = (BoulderScript)boulderClone.GetComponent ("BoulderScript");
			boulderScript.goingRight = rigidbody.velocity.x > 0;
			boulderScript.goingLeft = rigidbody.velocity.x < 0;
			boulderScript.goingUp = rigidbody.velocity.y > 0;
			boulderScript.goingDown = rigidbody.velocity.y < 0;
		} else {
			tempLocation = location;
			tempVelocity = velocity;
			tempGoingRight = goingRight;
		}
	}
}
