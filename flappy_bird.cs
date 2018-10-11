
using UnityEngine;

public class jump : MonoBehaviour {

	public Rigidbody rb; // refers to itself...Seems like should be implicit but whatever.
	public int forwardForce = 35;
	public Material wall_mat;
	public Material wall_mat2;

	private int zCounter = 50;
	private int obstacle_interval = 50;
	private int sidewaysForce = 400;

	// next steps: 
//	add fog of war, 
//	add score (how to determine whether player went through?), 
//	procedurally generate new hoops as player progresses. 
//	Perhaps let x position of the hoops vary.


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

		createHoop (8, 4, 380);
		createHoop (5, 5, 410);
		createHoop (3, 4, 440);
		createHoop (7, 6, 470);
//		createHoop (9, 3, 500);
//		createHoop (12, 4, 530);

//		Debug.Log (Random.Range(1, 100));

		rb.AddForce (0, 0, 20, ForceMode.VelocityChange);
	}
	
	void FixedUpdate () {
		if (Input.GetKey ("s")) {
			rb.AddForce (0, 500, 0);
		}
		if (Input.GetKey ("d")) {
			rb.AddForce (0, -300, 0);
		}

		// Horizontal forces:
		if (Input.GetKey ("a")) {
			rb.AddForce (- sidewaysForce, 0, 0);
		}
		if (Input.GetKey ("f")) {
			rb.AddForce (sidewaysForce, 0, 0);
		}


		// Ahh, z is changing in much too coarse-grained a way for this to work:
//		if (rb.transform.position.z % 50 <= 0.1) {
//			Debug.Log("got one boss!");
//		}

		if (rb.transform.position.z > zCounter) {
			createHoop (Random.Range(3, 15), Random.Range(3, 9), zCounter + 500);
			zCounter += obstacle_interval;

			// Lame way to deal with accumulation of forward force:
			if (zCounter > 500) {
				obstacle_interval = 100;
			}
		}
			

		// Seems like we don't want the ForceMode.VelocityChange, which might be making the force addition cumulative?
		rb.AddForce (0, 0, forwardForce);
	}

	// Takes in a positional height and a radius, and a z-position. x always 0 for hor-bars:
	void createBar (int x, int y, int z, int r) {
		GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.position = new Vector3 (x, y, z);

		if (x != 0) {
			bar.transform.localScale = new Vector3 (1, 2 * r, 1);
		} else {
			bar.transform.localScale = new Vector3 (2 * r, 1, 1);
		}

		// Color it in (should really be on the hoop but eh..), alternating every other:
		if (z % 4 == 0) {
			bar.GetComponent<MeshRenderer> ().material = wall_mat;
		} else {
			bar.GetComponent<MeshRenderer> ().material = wall_mat2;
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
