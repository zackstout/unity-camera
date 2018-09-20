
using UnityEngine;

public class p1 : MonoBehaviour {

	public cam cam_info;
	public Vector3 offset;
	float x=0;
	float y=0;
	bool startedJumping = false;
	bool readyToJump = false;
	public Rigidbody rb; // Used Unity's slot to self-refer.

	void Start () {
		Debug.Log (cam_info.offset);
	}
	
	void Update () {

		// MOVE:
		if (Input.GetKey ("w")) {
			offset.x = Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			offset.z = Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			transform.position += offset;
			//			Debug.Log (transform.rotation.eulerAngles);
		}
		if (Input.GetKey ("s")) {
			offset.x = - Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			offset.z = - Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			transform.position += offset;
		}
		if (Input.GetKey ("a")) {
			offset.x = - Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			offset.z = Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			transform.position += offset;
		}
		if (Input.GetKey ("d")) {
			offset.x =  Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			offset.z = - Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			transform.position += offset;
		}

		// Initiate JUMP/HOVER:
		if (Input.GetKey ("space")) {
			if (!startedJumping) {
				readyToJump = true;
			}
		}

		// ROTATE (i and k are weird now that we are rotating player instead of camera):
		if (Input.GetKey ("i")) {		
			x -= Time.deltaTime * 50;
		}
		if (Input.GetKey ("k")) {
			x += Time.deltaTime * 50;
		}
		if (Input.GetKey ("j")) {
			y -= Time.deltaTime * 50;
		}
		if (Input.GetKey ("l")) {
			y += Time.deltaTime * 50;
		}

		transform.rotation = Quaternion.Euler(x,y,0);
	}

	void FixedUpdate() {
		// Might be cool to make it so you can only jump again once you've touched the ground.
		// I like the hovering too though.
		if (readyToJump) {
			rb.AddForce (0, 1, 0, ForceMode.VelocityChange);
			readyToJump = false;
		}

	}
}
