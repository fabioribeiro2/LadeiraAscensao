using UnityEngine;
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
		for (int j=0; j<9; j++) {
			for (int i=0; i<18; i++) {
				if (i != 8) {
					GameObject platformPiece = Instantiate (PlatformPiece, new Vector3 (-6.6085f + 0.7801f * i, 3.15f - 0.795f * j, 0), new Quaternion()) as GameObject;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeToNextBoulder -= Time.deltaTime;
		if (timeToNextBoulder <= 0) {			
			GameObject boulder = Instantiate (Boulder, new Vector3 (-6 , 3.5f , 0), new Quaternion()) as GameObject;
			Rigidbody2D rigidbody = boulder.GetComponent<Rigidbody2D>();
			//			Rigidbody2D rigidbody = boulder.rigidbody2D;
			rigidbody.AddForce(new Vector2(10,0));
			boulderScript = (BoulderScript) boulder.GetComponent("BoulderScript");
			boulderScript.goingDown = true;
			boulderScript.goingRight = true;
			boulderScript.potentialYVelocity = 2f;
			timeToNextBoulder = Random.Range (30,30);
			
			
			GameObject boulder2 = Instantiate (Boulder, new Vector3 (-6 , 2.0f , 0), new Quaternion()) as GameObject;
			Rigidbody2D rigidbody2 = boulder2.GetComponent<Rigidbody2D>();
			//			Rigidbody2D rigidbody = boulder.rigidbody2D;
			rigidbody2.AddForce(new Vector2(10,0));
			boulderScript2 = (BoulderScript) boulder2.GetComponent("BoulderScript");
			boulderScript2.goingDown = true;
			boulderScript2.goingRight = true;
			boulderScript2.potentialYVelocity = 2f;
			
			GameObject boulder3 = Instantiate (Boulder, new Vector3 (-6 , -3.6f , 0), new Quaternion()) as GameObject;
			Rigidbody2D rigidbody3 = boulder3.GetComponent<Rigidbody2D>();
			//			Rigidbody2D rigidbody = boulder.rigidbody2D;
			rigidbody3.AddForce(new Vector2(10,0));
			boulderScript3 = (BoulderScript) boulder3.GetComponent("BoulderScript");
			boulderScript3.goingDown = true;
			boulderScript3.goingRight = true;
			boulderScript3.potentialYVelocity = 2f;
		}
	}
}
