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
	public float potentialXVelocity;
	public float potentialYVelocity;
	private Rigidbody2D rigidbody;
	public bool goingUp;
	public bool goingDown;
	public bool goingLeft;
	public bool goingRight;
	public float currentYVelocity;
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
	public bool touchingPlatforms = false;
	public float cornerYVelocity;
	public int platformAmount;

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
		platformAmount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (potentialXVelocity < 3) {
			potentialXVelocity = 3;
		}
		potentialYVelocity = Mathf.Abs (rigidbody.velocity.y);
		if (potentialYVelocity > 8) {
			potentialYVelocity = potentialYVelocity * 0.4F;
		} else if (potentialYVelocity > 3) {
			potentialYVelocity = potentialYVelocity * 1.105F;
		} else {			
			potentialYVelocity = potentialYVelocity * 1.121F;
		}
		currentYVelocity = rigidbody.velocity.y;
		if (goingUp && rigidbody.velocity.y <= 0 && !touchingPlatforms) {
			goingUp = false;
			goingDown = true;
		}
		if (goingDown && rigidbody.velocity.y > 0 && !touchingPlatforms) {
			goingUp = true;
			goingDown = false;
		}
		
		if (Mathf.Abs (rigidbody.velocity.x) > potentialXVelocity) {
			potentialXVelocity = Mathf.Abs (rigidbody.velocity.x);
		}	

		if (Mathf.Abs (rigidbody.velocity.x) < potentialXVelocity) {
			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, rigidbody.velocity.y);
		}	

	}

	void OnCollisionExit2D (Collision2D coll)
	{
		float maxVelocity = Mathf.Max (Mathf.Abs (savedYVelocity), Mathf.Abs (currentYVelocity), Mathf.Abs (cornerYVelocity));

		if (coll.gameObject.tag == "MainPlatformBody") {
			platformAmount--;
			if (platformAmount == 0) {
				touchingPlatforms = false;
			}
			if (goingUp) { 
				rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, maxVelocity);
			} else {
				rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -maxVelocity);
			}
		}
		
		if (coll.gameObject.tag == "RightPlatform" || coll.gameObject.tag == "LeftPlatform") {	
			platformAmount--;
			if (platformAmount == 0) {
				touchingPlatforms = false;
			}
		}
		
		if (coll.gameObject.tag == "LeftSide") {
			if (goingUp) { 
				rigidbody.velocity = new Vector2 (potentialXVelocity, maxVelocity);
			} else {
				rigidbody.velocity = new Vector2 (potentialXVelocity, -maxVelocity);
			}
			
			goingRight = true;
			goingLeft = false;
			touchingLeft = false;
		}

		if (coll.gameObject.tag == "RightSide") {
			if (goingUp) { 
				rigidbody.velocity = new Vector2 (-potentialXVelocity, maxVelocity);
			} else {
				rigidbody.velocity = new Vector2 (-potentialXVelocity, -maxVelocity);
			}
			
			goingRight = false;
			goingLeft = true;
			touchingRight = false;
		}

		if (coll.gameObject.tag == "TopSide") {
			if (goingRight) { 
				rigidbody.velocity = new Vector2 (potentialXVelocity, -maxVelocity);
			} else {
				rigidbody.velocity = new Vector2 (-potentialXVelocity, -maxVelocity);
			}			
			goingUp = false;
			goingDown = true;
			touchingTop = false;
		}

		if (coll.gameObject.tag == "BottomSide") {
			if (goingRight) { 
				rigidbody.velocity = new Vector2 (potentialXVelocity, maxVelocity);
			} else {
				rigidbody.velocity = new Vector2 (-potentialXVelocity, maxVelocity);
			}			
			goingUp = true;
			goingDown = false;
			touchingBottom = false;
		}
	}
	
	void OnCollisionStay2D (Collision2D coll)
	{
		float maxVelocity = Mathf.Max (Mathf.Abs (savedYVelocity), Mathf.Abs (currentYVelocity), Mathf.Abs (cornerYVelocity));
		
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
			touchingPlatforms = true;
			GameObject platform = coll.transform.parent.gameObject;
			PlatformScript platformScript = (PlatformScript)platform.GetComponent ("PlatformScript");
			if (!platformScript.isColliderOverlapRight) {
				goingRight = true;
				goingLeft = false;
			}
		}		

		if (coll.collider.tag == "LeftPlatform") {		
			touchingPlatforms = true;		
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
			touchingPlatforms = true;			
			if (goingUp) { 
				rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, maxVelocity);
			} else {
				rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -maxVelocity);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.collider.tag == "MainPlatformBody") {	
			platformAmount++;
			touchingPlatforms = true;
			if (transform.position.y > coll.gameObject.transform.position.y + collider.radius / 3.5f || transform.position.y < coll.gameObject.transform.position.y - collider.radius / 3.5f) {

//				Debug.Log ("changing saved velocity to = " + rigidbody.velocity.y);
				savedYVelocity = rigidbody.velocity.y;
				if (!touchingRight && !touchingLeft && !touchingPlatforms) {
					cornerYVelocity = rigidbody.velocity.y;
				}				
				if (transform.position.y < coll.gameObject.transform.position.y) { //boulder is coming from below
					rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -potentialYVelocity);
					goingUp = false;
					goingDown = true;
				} else { 														  //boulder is coming from above
					rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, potentialYVelocity);
					goingUp = true;
					goingDown = false;
				}
			}
		}

		if ((coll.collider.tag == "LeftPlatform" || coll.collider.tag == "RightPlatform")) {
			platformAmount++;
			if (!touchingRight && !touchingLeft && !touchingPlatforms) {
				cornerYVelocity = rigidbody.velocity.y;
			}					
			touchingPlatforms = true;
			GameObject platform = coll.transform.parent.gameObject;
			PlatformScript platformScript = (PlatformScript)platform.GetComponent ("PlatformScript");
			if ((coll.collider.tag == "LeftPlatform" && !platformScript.isColliderOverlapLeft) || (coll.collider.tag == "RightPlatform" && !platformScript.isColliderOverlapRight)) {
				rigidbody.velocity = new Vector2 (coll.collider.tag == "LeftPlatform" ? -potentialXVelocity : potentialXVelocity, currentYVelocity);

				Debug.Log ("mudando direçao");
				goingRight = rigidbody.velocity.x > 0;
				goingLeft = !goingRight; 
			}
		}

		if (coll.gameObject.tag == "Boulder") {		
			Vector2 velocity = new Vector2 (potentialXVelocity, currentYVelocity);
			Vector2 location = new Vector2 (transform.position.x, transform.position.y);
			Destroy (this.gameObject);
			Destroy (coll.gameObject);
			boulderCollisionScript.makeBoulder (velocity, location, goingRight);
		}

		if (coll.gameObject.tag == "RightSide") {
			savedYVelocity = rigidbody.velocity.y;
			if (!touchingPlatforms && !touchingRight && !touchingBottom && !touchingTop) {
				cornerYVelocity = rigidbody.velocity.y;
			}
			touchingRight = true;
			rigidbody.velocity = new Vector2 (-potentialXVelocity, currentYVelocity);
			goingRight = false;
			goingLeft = true;
		}

		if (coll.gameObject.tag == "LeftSide") {
			savedYVelocity = rigidbody.velocity.y;				
			if (!touchingPlatforms && !touchingLeft && !touchingBottom && !touchingTop) {
				cornerYVelocity = rigidbody.velocity.y;
			}
			touchingLeft = true;
			rigidbody.velocity = new Vector2 (potentialXVelocity, currentYVelocity);
			goingRight = true;
			goingLeft = false;
		}

		if (coll.gameObject.tag == "TopSide") {
			savedYVelocity = rigidbody.velocity.y;				
			if (!touchingPlatforms && !touchingLeft && !touchingBottom && !touchingTop) {
				cornerYVelocity = rigidbody.velocity.y;
			}
			touchingTop = true;
			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, -currentYVelocity);
			goingDown = true;
			goingUp = false;
		}
		if (coll.gameObject.tag == "BottomSide") {
			savedYVelocity = rigidbody.velocity.y;				
			if (!touchingPlatforms && !touchingLeft && !touchingBottom && !touchingTop) {
				cornerYVelocity = rigidbody.velocity.y;
			}
			touchingBottom = true;
			rigidbody.velocity = new Vector2 (goingRight ? potentialXVelocity : -potentialXVelocity, currentYVelocity);
			goingDown = false;
			goingUp = true;
		}
	}
}