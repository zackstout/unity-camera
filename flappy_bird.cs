
using UnityEngine;

public class jump : MonoBehaviour {

	public Rigidbody rb; // refers to itself...Seems like should be implicit but whatever.
	public int forwardForce = 35;

	void Start () {
		createHoop (10, 10, 70);
		createHoop (7, 5, 100);
		createHoop (12, 7, 130);
		createHoop (8, 12, 160);
		createHoop (9, 6, 190);
		createHoop (10, 4, 220);
		createHoop (8, 5, 250);
		createHoop (11, 4, 280);
		createHoop (15, 6, 310);
		createHoop (9, 3, 340);
	}
	
	void FixedUpdate () {
		if (Input.GetKey ("s")) {
			rb.AddForce (0, 500, 0);
		}
		if (Input.GetKey ("d")) {
			rb.AddForce (0, -300, 0);
		}

		// Seems like we don't want the ForceMode.VelocityChange, which might be making the force addition cumulative?
		rb.AddForce (0, 0, forwardForce);
	}

	// Takes in a positional height and a radius, and a z-position. x always 0 for hor-bars:
	void createBar (int x, int y, int z, int r) {
		GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.position = new Vector3 (x, y, z);

		if (x != 0) {
			bar.transform.localScale = new Vector3 (1, 2*r, 1);
		} else {
			bar.transform.localScale = new Vector3 (2 * r, 1, 1);
		}
	}


	void createHoop (int y, int r, int z) {
		// horizontal bars:
		createBar (0, y + r, z, r);
		createBar (0, y - r, z, r);

		// vertical bars:
		createBar (r, y, z, r);
		createBar (-r, y, z, r);
	}

}
