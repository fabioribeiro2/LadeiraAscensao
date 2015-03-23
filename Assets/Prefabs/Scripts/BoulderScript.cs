using UnityEngine;
using System.Collections;

public class BoulderScript : MonoBehaviour
{
	private bool collisionTop = false;
	private bool collisionLeft = false;
	private bool collisionRight = false;
	private bool collisionBottom = false;
	private Vector2 saveVelocity;
	private float saveMagnitude;
	private Vector2 saveNormalized;
	public int collisionNumber;
	public float potentialXVelocity;
	private float potentialYVelocity;
	private Rigidbody2D rigidbody;
	public bool goingUp;
	public bool goingDown;
	public bool goingLeft;
	public bool goingRight;
	public float currentYVelocity;
	public GameObject Boulder;
	private BoulderCollisionScript boulderCollisionScript;
	public bool selfDestruct;
	public float selfDestructTimer;

	// Use this for initialization
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		collisionNumber = 0;		
		potentialXVelocity = Mathf.Abs (rigidbody.velocity.x);
		GameObject boulderCollisionEvent = GameObject.Find ("BoulderCollisionEvent");
		boulderCollisionScript = (BoulderCollisionScript)boulderCollisionEvent.GetComponent ("BoulderCollisionScript");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		potentialYVelocity = Mathf.Abs (rigidbody.velocity.y);
//		if (potentialYVelocity > 8) {
//			potentialYVelocity = potentialYVelocity * 0.9997F;
//		} else if (potentialYVelocity > 3) {
//			potentialYVelocity = potentialYVelocity * 0.999F;
//		} else {			
//			potentialYVelocity = potentialYVelocity * 0.99F;
//		}
		currentYVelocity = rigidbody.velocity.y;
		if (goingUp && rigidbody.velocity.y < 0 && collisionNumber == 0) {
			goingUp = false;
			goingDown = true;
		}
		if (goingDown && rigidbody.velocity.y > 0 && collisionNumber == 0) {
			goingUp = true;
			goingDown = false;
		}
		
		if (Mathf.Abs (rigidbody.velocity.x) > potentialXVelocity) {
			potentialXVelocity = Mathf.Abs (rigidbody.velocity.x);
		}	

		if (Mathf.Abs (rigidbody.velocity.x) < potentialXVelocity) {
			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, rigidbody.velocity.y);
		}	
//		if (selfDestruct) {
//			selfDestructTimer-= Time.deltaTime;
//			if (selfDestructTimer <= 0) {
//
//			}
//		}
		
//		if (Mathf.Abs (rigidbody.velocity.y) < potentialYVelocity) {
//			rigidbody.velocity = new Vector2 (rigidbody.velocity.x, goingUp? potentialXVelocity : -potentialXVelocity);
//		}	
	}

	void OnCollisionExit2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "MainPlatformBody") {
			collisionNumber = 0;
			GameObject obj = GameObject.FindGameObjectWithTag ("LeftSide");
			Collider2D coll2 = (Collider2D)obj.GetComponent ("EdgeCollider2D");
//			Debug.Log ("ta encostado? -> " + coll2.IsTouching (coll.collider));
			if (coll2.IsTouching (coll.collider)) {
				if (goingUp) { 
					rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, potentialYVelocity);
				} else {
					rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -potentialYVelocity);
				}
			}
		}
		
		if (coll.gameObject.tag == "RightPlatform" || coll.gameObject.tag == "LeftPlatform") {
			collisionNumber = 0;
		}
//		Debug.Log ("exit speed = " + rigidbody.velocity.y);
		
//		if (coll.gameObject.tag == "RightSide") {
//			Debug.Log ("left hit, current directions is: " + (goingUp ? "Up and " : "Down and "));
//			Debug.Log (potentialYVelocity);
//			Debug.Log (rigidbody.velocity.y);
//			Debug.Log ("current Y = " + currentYVelocity);
//			rigidbody.velocity = new Vector2 (-potentialXVelocity, currentYVelocity);
//			goingRight = false;
//			goingLeft = true;
//		}
		if (coll.gameObject.tag == "LeftSide") {
//			Debug.Log (potentialYVelocity);
			rigidbody.velocity = new Vector2 (potentialXVelocity, currentYVelocity);
			goingRight = true;
			goingLeft = false;
		}
//		if (coll.gameObject.tag == "TopSide") {
//			Debug.Log (potentialYVelocity);
//			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -Mathf.Abs(currentYVelocity));
//			goingDown = true;
//			goingUp = false;
//		}
//		if (coll.gameObject.tag == "BottomSide") {
//			Debug.Log (potentialYVelocity);
//			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, Mathf.Abs(currentYVelocity));
//			goingDown = false;
//			goingUp = true;
//		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		//		Debug.Log ("jones");
		Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D> ();
		if (coll.collider.tag == "MainPlatformBody") {	
			Debug.Log ("aquele abraço");
			collisionNumber++;
			//			Debug.Log (potentialYVelocity);
			//			Debug.Log (rigidbody.velocity.y);
			Debug.Log ("aquele abraço numero colliders = " + collisionNumber);
			if (collisionNumber == 1) {
				//				Debug.Log (potentialYVelocity);
				if (goingUp) {
					Debug.Log ("aquele abraço 2");
					rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -potentialYVelocity);
					goingUp = false;
					goingDown = true;
				} else {
					rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, potentialYVelocity);
					goingUp = true;
					goingDown = false;
				}
			}
		}
		//		Debug.Log ("jones");
		if (coll.collider.tag == "LeftPlatform" || coll.collider.tag == "RightPlatform") {	
			collisionNumber++;
			Debug.Log ("alo alo teresinha");
			Debug.Log ("right =" + goingRight);
			Debug.Log ("left =" + goingLeft);
			Debug.Log ("alo alo teresinha numero colliders = " + collisionNumber);
			//			Debug.Log (rigidbody.velocity.y);
			if (collisionNumber == 1) {
				if (coll.gameObject.transform.position.y > transform.position.y) {
					rigidbody.velocity = new Vector2 (coll.collider.tag == "LeftPlatform" ? -potentialXVelocity : potentialXVelocity, -potentialYVelocity);
					goingUp = false;
					goingDown = true;
				} else {
					rigidbody.velocity = new Vector2 (coll.collider.tag == "LeftPlatform" ? -potentialXVelocity : potentialXVelocity, potentialYVelocity);
					goingUp = true;
					goingDown = false;
				}
				Debug.Log ("mudando direçao");
				goingRight = rigidbody.velocity.x > 0;
				goingLeft = !goingRight; 
				Debug.Log ("direita : " + goingRight);
				Debug.Log ("esquerda : " + goingLeft);
				Debug.Log ("xVelo : " + rigidbody.velocity.x);
				Debug.Log ("yVelo : " + rigidbody.velocity.y);
			}
		}
		if (coll.gameObject.tag == "Boulder") {		
//			Debug.Log("collision tym! vel x = " + rigidbody.velocity.x + " vel y = " + rigidbody.velocity.y);
			Vector2 velocity = new Vector2 (potentialXVelocity, currentYVelocity);
			Vector2 location = new Vector2 (transform.position.x, transform.position.y);
			Destroy (this.gameObject);
			Destroy (coll.gameObject);
			boulderCollisionScript.makeBoulder (velocity, location, goingRight);
		}
		if (coll.gameObject.tag == "RightSide") {
//			Debug.Log ("left hit, current directions is: " + (goingUp ? "Up and " : "Down and "));
//			Debug.Log (potentialYVelocity);
//			Debug.Log (rigidbody.velocity.y);
//			Debug.Log ("current Y = " + currentYVelocity);
			rigidbody.velocity = new Vector2 (-potentialXVelocity, currentYVelocity);
			goingRight = false;
			goingLeft = true;
		}
		if (coll.gameObject.tag == "LeftSide") {
//			Debug.Log (potentialYVelocity);
			rigidbody.velocity = new Vector2 (potentialXVelocity, currentYVelocity);
			goingRight = true;
			goingLeft = false;
		}
		if (coll.gameObject.tag == "TopSide") {
//			Debug.Log (potentialYVelocity);
			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -currentYVelocity);
			goingDown = true;
			goingUp = false;
		}
		if (coll.gameObject.tag == "BottomSide") {
//			Debug.Log (potentialYVelocity);
			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -currentYVelocity);
			goingDown = false;
			goingUp = true;
		}
	}
}