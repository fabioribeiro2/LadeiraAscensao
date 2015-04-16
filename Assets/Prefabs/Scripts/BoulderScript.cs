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
	public float fixedMaxXVelocity;
	public float fixedMaxYVelocity;
	public float maxXVelocity;
	public float potentialYVelocity;
	private Rigidbody2D rigidbody;
	public bool goingUp;
	public bool goingDown;
	public bool goingLeft;
	public bool goingRight;
	public float currentYVelocity;
	public float currentXVelocity;
	public GameObject Boulder;
	private BoulderCollisionScript boulderCollisionScript;
	public bool selfDestruct;
	public float savedYVelocity;
	public bool isTouchingOtherColliders = false;
	public int platformCollisionAmount;
	public int edgesLayerMask;
	public int platformBodyLayerMask;
	public int platformSidesLayerMask;
	public int boulderLayerMask;
	public bool touchingEdges = false;
	private CircleCollider2D collider;
	public bool touchingRight = false;
	public bool touchingLeft = false;
	public bool touchingTop = false;
	public bool touchingBottom = false;
	public float cornerYVelocity;
	public float maxYVelocity;
	public float calculatedVelocity;
	public bool touchingBody;
	public bool touchingLeftPlatform;
	public bool touchingRightPlatform;
	public int boulderLevel = 0;
	public float xVelocityIncrement;
	public float yVelocityIncrement;



	// Use this for initialization
	void Start ()
	{
		int edgesLayer = LayerMask.NameToLayer ("Edges");
		int plaformBodyLayer = LayerMask.NameToLayer ("PlatformBodyCollider");
		int platformSidesLayer = LayerMask.NameToLayer ("PlatformSideCollider");
		int boulderLayer = LayerMask.NameToLayer ("Boulder");
		int edgeMask = 1 << edgesLayer;
		int bodyMask = 1 << plaformBodyLayer;
		int platformSideMask = 1 << platformSidesLayer;
		int boulderMask = 1 << boulderLayer;
		rigidbody = GetComponent<Rigidbody2D> ();
		GameObject boulderCollisionEvent = GameObject.Find ("BoulderCollisionEvent");
		boulderCollisionScript = (BoulderCollisionScript)boulderCollisionEvent.GetComponent ("BoulderCollisionScript");
		collider = (CircleCollider2D)transform.GetComponent ("CircleCollider2D");
		touchingBody = false;
		touchingLeftPlatform = false;
		touchingRightPlatform = false;
		fixedMaxXVelocity = 1.5f;
		fixedMaxYVelocity = 1.3f;
		xVelocityIncrement = 0.15f;
		yVelocityIncrement = 0.13f;
		maxXVelocity = fixedMaxXVelocity + boulderLevel * xVelocityIncrement;
		maxYVelocity = fixedMaxYVelocity + boulderLevel * yVelocityIncrement;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (maxXVelocity < fixedMaxXVelocity + boulderLevel * xVelocityIncrement) {
			maxXVelocity = fixedMaxXVelocity + boulderLevel * xVelocityIncrement;
		}

//		if (potentialYVelocity > 1.55f) {
//			potentialYVelocity = 1.55f;
//		}

		//		potentialYVelocity = potentialYVelocity * 1.0025F;
		currentYVelocity = rigidbody.velocity.y;
		currentXVelocity = rigidbody.velocity.x;

		if (goingUp && rigidbody.velocity.y <= 0 && !touchingBody && !touchingLeftPlatform && !touchingRightPlatform && !touchingBottom) {
			goingUp = false;
			goingDown = true;
		}

		if (goingDown && rigidbody.velocity.y > 0 && !touchingBody && !touchingLeftPlatform && !touchingRightPlatform && !touchingBottom) {
			goingUp = true;
			goingDown = false;
		}

		if (!touchingBody && !touchingBottom && !touchingLeftPlatform && !touchingRightPlatform) {
			if (goingUp) {
				potentialYVelocity -= rigidbody.gravityScale * Time.fixedDeltaTime;
			} else {			
				potentialYVelocity += rigidbody.gravityScale * Time.fixedDeltaTime * 1.3f;
			}
		}

		if (potentialYVelocity < 0) {
			potentialYVelocity = 0;
		}

		calculatedVelocity = Mathf.Min (Mathf.Abs (potentialYVelocity), Mathf.Abs (maxYVelocity));

		rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, goingUp ? calculatedVelocity * 1.3f : -calculatedVelocity);

	}

	void OnCollisionExit2D (Collision2D coll)
	{
		float maxVelocity = Mathf.Max (Mathf.Abs (savedYVelocity), Mathf.Abs (currentYVelocity), Mathf.Abs (cornerYVelocity), Mathf.Abs (potentialYVelocity));
		float appliedVelocity = Mathf.Min (maxVelocity, maxYVelocity);

		if (coll.gameObject.tag == "MainPlatformBody") {
			touchingBody = false;
			if (goingUp) { 
				rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, appliedVelocity);
			} else {
				rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, -appliedVelocity);
			}
		}
		
		if (coll.gameObject.tag == "RightPlatform") {
			touchingRightPlatform = false;
		}

		if (coll.gameObject.tag == "LeftPlatform") {
			touchingLeftPlatform = false;
		}
		
		if (coll.gameObject.tag == "LeftSide") {
			if (goingUp) { 
				rigidbody.velocity = new Vector2 (maxXVelocity, appliedVelocity);
			} else {
				rigidbody.velocity = new Vector2 (maxXVelocity, -appliedVelocity);
			}
			
			goingRight = true;
			goingLeft = false;
			touchingLeft = false;
		}

		if (coll.gameObject.tag == "RightSide") {
			if (goingUp) { 
				rigidbody.velocity = new Vector2 (-maxXVelocity, appliedVelocity);
			} else {
				rigidbody.velocity = new Vector2 (-maxXVelocity, -appliedVelocity);
			}
			
			goingRight = false;
			goingLeft = true;
			touchingRight = false;
		}

		if (coll.gameObject.tag == "TopSide") {
			if (goingRight) { 
				rigidbody.velocity = new Vector2 (maxXVelocity, -appliedVelocity);
			} else {
				rigidbody.velocity = new Vector2 (-maxXVelocity, -appliedVelocity);
			}			
			goingUp = false;
			goingDown = true;
			touchingTop = false;
		}

		if (coll.gameObject.tag == "BottomSide") {
			if (goingRight) { 
				rigidbody.velocity = new Vector2 (maxXVelocity, appliedVelocity);
			} else {
				rigidbody.velocity = new Vector2 (-maxXVelocity, appliedVelocity);
			}			
			goingUp = true;
			goingDown = false;
			touchingBottom = false;
		}
	}
	
	void OnCollisionStay2D (Collision2D coll)
	{
		float maxVelocity = Mathf.Max (Mathf.Abs (savedYVelocity), Mathf.Abs (currentYVelocity), Mathf.Abs (cornerYVelocity), Mathf.Abs (potentialYVelocity));
		float appliedVelocity = Mathf.Min (maxVelocity, maxYVelocity);
		
		if (coll.collider.tag == "RightSide") {	
			goingRight = false;
			goingLeft = true;
			touchingRight = true;
		}
		
		if (coll.collider.tag == "LeftSide") {	
			goingRight = true;
			goingLeft = false;
			touchingLeft = true;
		}
		if (coll.collider.tag == "TopSide") {	
			goingUp = false;
			goingDown = true;
			touchingTop = true;
		}
		
		if (coll.collider.tag == "BottomSide") {	
			goingUp = true;
			goingDown = false;
			touchingBottom = true;
		}
		
		if (coll.collider.tag == "RightPlatform") {					
			touchingRightPlatform = true;		
			GameObject platform = coll.transform.parent.gameObject;	
			PlatformScript platformScript = (PlatformScript)platform.GetComponent ("PlatformScript");	
			if (!platformScript.isColliderOverlapRight) {		
				goingRight = true;			
				goingLeft = false;		
			}	
		}		
		if (coll.collider.tag == "LeftPlatform") {			
			touchingLeftPlatform = true;		
			GameObject platform = coll.transform.parent.gameObject;		
			PlatformScript platformScript = (PlatformScript)platform.GetComponent ("PlatformScript");	
			if (!platformScript.isColliderOverlapLeft) {	
				goingRight = false;		
				goingLeft = true;	
			}	
		}		
		if (coll.collider.tag == "MainPlatformBody") {		
			if (transform.position.y > coll.gameObject.transform.position.y) {		
				goingUp = true;		
				goingDown = false;		
			} else {		
				goingUp = false;		
				goingDown = true;		
			}		
			touchingBody = true;		
			if (goingUp) { 		
				rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, appliedVelocity);		
			} else {		
				rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, -appliedVelocity);		
			}	
		}	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{	
		
		if (coll.collider.tag == "MainPlatformBody") {	
			touchingBody = true;
			if (transform.position.y > coll.gameObject.transform.position.y + collider.radius / 3.5f || transform.position.y < coll.gameObject.transform.position.y - collider.radius / 3.5f) {
				savedYVelocity = rigidbody.velocity.y;	
				if (!touchingRight && !touchingLeft && !touchingBody && !touchingLeftPlatform && !touchingRightPlatform) {	
					cornerYVelocity = rigidbody.velocity.y;	
				}						
				if (transform.position.y < coll.gameObject.transform.position.y) { //boulder is coming from below		
					rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, -Mathf.Min (Mathf.Abs (potentialYVelocity), Mathf.Abs (maxYVelocity)));		
					goingUp = false;			
					goingDown = true;	
				} else { //boulder is coming from above		
					rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, Mathf.Min (Mathf.Abs (potentialYVelocity), Mathf.Abs (maxYVelocity)));		
					goingUp = true;			
					goingDown = false;		
				}		
			}	
		}	
		
		if (coll.collider.tag == "RightPlatform") {		
			float appliedVelocity = Mathf.Min (potentialYVelocity, maxYVelocity);
			if (!touchingRight && !touchingLeft && !touchingBody && !touchingLeftPlatform && !touchingRightPlatform) {		
				cornerYVelocity = rigidbody.velocity.y;		
			}						
			GameObject platform = coll.transform.parent.gameObject;	
			PlatformScript platformScript = (PlatformScript)platform.GetComponent ("PlatformScript");		
			if (!platformScript.isColliderOverlapRight && !touchingBody && !touchingRightPlatform) {									
				rigidbody.velocity = new Vector2 (maxXVelocity, goingUp ? appliedVelocity : -appliedVelocity);			
				goingRight = true;	
				goingLeft = false; 	
				potentialYVelocity = potentialYVelocity * 1.05f;
			}			
			touchingRight = true;
		}	
		
		if ((coll.collider.tag == "LeftPlatform")) {	
			float appliedVelocity = Mathf.Min (potentialYVelocity, maxYVelocity);	
			if (!touchingRight && !touchingLeft && !touchingBody && !touchingLeftPlatform && !touchingRightPlatform) {		
				cornerYVelocity = rigidbody.velocity.y;		
			}						
			GameObject platform = coll.transform.parent.gameObject;	
			PlatformScript platformScript = (PlatformScript)platform.GetComponent ("PlatformScript");		
			if (!platformScript.isColliderOverlapLeft && !touchingBody && !touchingLeftPlatform) {	
				rigidbody.velocity = new Vector2 (-maxXVelocity, goingUp ? appliedVelocity : -appliedVelocity);			
				goingRight = rigidbody.velocity.x > 0;	
				goingLeft = !goingRight; 	
				potentialYVelocity = potentialYVelocity * 1.05f;
			}			
			touchingLeft = true;
		}	

		if (coll.gameObject.tag == "Boulder") {			
			Vector2 velocity = new Vector2 (maxXVelocity, currentYVelocity);		
			Vector2 location = new Vector2 (transform.position.x, transform.position.y);	
			int level = boulderLevel;
			Destroy (this.gameObject);	
			Destroy (coll.gameObject);		
			boulderCollisionScript.makeBoulder (velocity, location, goingRight, potentialYVelocity, boulderLevel);	
		}	

		if (coll.gameObject.tag == "RightSide") {		
			savedYVelocity = rigidbody.velocity.y;	
			if (!touchingBody && !touchingLeftPlatform && !touchingRightPlatform && !touchingRight && !touchingBottom && !touchingTop) {			
				cornerYVelocity = rigidbody.velocity.y;		
			}		
			touchingRight = true;		
			rigidbody.velocity = new Vector2 (-maxXVelocity, currentYVelocity);		
			goingRight = false;		
			goingLeft = true;	
		}	
		if (coll.gameObject.tag == "LeftSide") {	
			savedYVelocity = rigidbody.velocity.y;	
			if (!touchingBody && !touchingLeftPlatform && !touchingRightPlatform && !touchingLeft && !touchingBottom && !touchingTop) {		
				cornerYVelocity = rigidbody.velocity.y;		
			}	
			touchingLeft = true;		
			rigidbody.velocity = new Vector2 (maxXVelocity, currentYVelocity);		
			goingRight = true;		
			goingLeft = false;	
		}	
		if (coll.gameObject.tag == "TopSide") {	
			savedYVelocity = rigidbody.velocity.y;	
			if (!touchingBody && !touchingLeftPlatform && !touchingRightPlatform && !touchingLeft && !touchingBottom && !touchingTop) {	
				cornerYVelocity = rigidbody.velocity.y;	
			}		
			touchingTop = true;		
			rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, -Mathf.Min (Mathf.Abs (potentialYVelocity), Mathf.Abs (maxYVelocity)));	
			goingDown = true;		
			goingUp = false;	
		}	
		if (coll.gameObject.tag == "BottomSide") {		
			savedYVelocity = rigidbody.velocity.y;			
			if (!touchingBody && !touchingLeftPlatform && !touchingRightPlatform && !touchingLeft && !touchingBottom && !touchingTop) {		
				cornerYVelocity = rigidbody.velocity.y;		
			}		
			touchingBottom = true;		
			rigidbody.velocity = new Vector2 (goingRight ? maxXVelocity : -maxXVelocity, Mathf.Min (Mathf.Abs (potentialYVelocity), Mathf.Abs (maxYVelocity)));		
			goingDown = false;		
			goingUp = true;	
		}	
	}
}