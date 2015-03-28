using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour
{
	public CircleCollider2D colLeft;
	public CircleCollider2D colRight;
	public int edgesLayerMask;
	public int platformBodyLayerMask;
	public int platformLeftLayerMask;
	public int platformRightLayerMask;
	public int boulderLayerMask;
	public int edgesLayer;
	public int plaformBodyLayer;
	public int platformLeftLayer;
	public int platformRightLayer;
	public int boulderLayer;
	public Collider2D isColliderOverlapLeft;
	public Collider2D isColliderOverlapRight;

	// Use this for initialization
	void Start ()
	{		
		platformLeftLayer = LayerMask.NameToLayer("PlatformLeftCollider");
		platformRightLayer = LayerMask.NameToLayer("PlatformRightCollider");
		edgesLayer = LayerMask.NameToLayer("Edges");
		int platformLeftLayerMask = 1 << platformLeftLayer;
		int platformRightLayerMask = 1 << platformRightLayer;
		int edgesLayerMask = 1 << edgesLayer;
		Transform col1 = transform.Find ("LeftCollider");
		Transform col2 = transform.Find ("RightCollider");
		colLeft = (CircleCollider2D) col1.GetComponent <CircleCollider2D> ();
		colRight = (CircleCollider2D) col2.GetComponent <CircleCollider2D> ();		
		RaycastHit2D rayLeft = Physics2D.Linecast(colLeft.transform.position, new Vector2(colLeft.transform.position.x - 1, colLeft.transform.position.y), platformRightLayerMask | edgesLayerMask);
		RaycastHit2D rayRight = Physics2D.Linecast(colRight.transform.position, new Vector2(colRight.transform.position.x + 1, colRight.transform.position.y), platformLeftLayerMask | edgesLayerMask);
		isColliderOverlapLeft = rayLeft.collider;
		isColliderOverlapRight = rayRight.collider;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
