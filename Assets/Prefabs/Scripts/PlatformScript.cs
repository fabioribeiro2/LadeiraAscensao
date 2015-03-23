using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour
{
	
	public bool isLeftTouch = false;
	public bool isRightTouch = false;
	public CircleCollider2D colLeft;
	public CircleCollider2D colRight;

	// Use this for initialization
	void Start ()
	{		
		Transform col1 = transform.Find ("LeftCollider");
		Transform col2 = transform.Find ("RightCollider");
		colLeft = (CircleCollider2D) col1.GetComponent <CircleCollider2D> ();
		colRight = (CircleCollider2D) col2.GetComponent <CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		isLeftTouch = colLeft.IsTouchingLayers (8);
		isRightTouch = colRight.IsTouchingLayers (8);
	}
}
