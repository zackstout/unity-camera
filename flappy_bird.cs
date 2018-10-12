
using UnityEngine;
using System.Collections.Generic; // need this to use Lists
using System.Collections;


public class jump : MonoBehaviour {

	public Rigidbody rb; // refers to itself...Seems like should be implicit but whatever.
	public int forwardForce = 35;
	public int sidewaysForce = 400;
	public int starting_dist = 500;

	public Material wall_mat;
	public Material wall_mat2;
	public Material coin_mat;

	private int zCounter = 50;
	public int obstacle_interval = 50;


	public int pass_z = 550;
	public int pass_y;
	public int pass_r;

	public int totalScore = 0;

	// This is embarrassing, to have two lists...:
//	public List<int> obstacle_ys = new List<int>();
//	public List<int> obstacle_zs = new List<int>();


	// NOTE: at some point we need to get a reference to this, after an element has been added to it.. -- can just do inside the Update
	public List<int[]> all_obstacles = new List<int[]>();



	void Start () {
		// Well... at least a step above creating them manually in the GUI:
		// Put em in a loop:
		createHoop (10, 10, 70, false);
		createHoop (7, 5, 100, false);
		createHoop (12, 7, 130, false);
		createHoop (8, 12, 160, false);
		createHoop (9, 6, 190, false);
		createHoop (10, 4, 220, false);
		createHoop (8, 5, 250, false);
		createHoop (11, 4, 280, false);
		createHoop (15, 6, 310, false);
		createHoop (9, 3, 340, false);

		createHoop (8, 4, 380, false);
		createHoop (5, 5, 410, false);
		createHoop (3, 4, 440, false);
		createHoop (7, 6, 470, false);
//		createHoop (9, 3, 500);
//		createHoop (12, 4, 530);

		// Add an initial forward force to the player:
		rb.AddForce (0, 0, 45, ForceMode.VelocityChange);

		// Create the coins:
		for (int i = starting_dist; i < starting_dist + 1000; i++) {
			if (i % 15 == 0) {
				createCoin (Random.Range(2, 18), i);
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.name == "coin") {
//			Debug.Log ("got a coin");
			Destroy (col.gameObject);
			totalScore++;
		} 
	}



	void FixedUpdate () {

		// Vertical forces:
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

		// Create new obstacles:
		if (rb.transform.position.z > zCounter) {
			createHoop (Random.Range(3, 15), Random.Range(3, 9), zCounter + starting_dist, true); 
			zCounter += obstacle_interval;

		}


		if (all_obstacles.Count > 0) {
			pass_y = all_obstacles [0] [0];
			pass_r = all_obstacles [0] [1];
			pass_z = all_obstacles [0] [2];
		}



		// So first one should appear at z=550. Then 600, etc.
		// So first counter should be 550; once we get to like 560, need to check whether we're in the range of the FIRST element in our yList.
		if (rb.transform.position.z > pass_z) {
			checkForPass (pass_y, pass_r, rb.transform.position.y);
			all_obstacles.RemoveAt (0);
		}


//		Debug.Log (obstacle_ys.Count);

		if (all_obstacles.Count > 1) {
			Debug.Log (all_obstacles [1][0]);
		}
			
	}


	void checkForPass(int y, int r, int py) {
		Debug.Log ("checking for pass!");
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


	void createCoin(int y, int z) {
		GameObject coin = GameObject.CreatePrimitive (PrimitiveType.Sphere);

		coin.transform.position = new Vector3 (0, y, z);
		coin.GetComponent<MeshRenderer> ().material = coin_mat;
		coin.transform.localScale = new Vector3 (1, 1, 0.2f); // Probably not necessary -- But this is good! We can flatten the coins this way!
		// For it to show up, we need some shading/lighting though.
		coin.name = "coin"; // allowing for deletion upon collision

		coin.GetComponent<Collider>().isTrigger = true;
	}


	void createHoop (int y, int r, int z, bool fromScript) {
		// Horizontal bars:
		createBar (0, y + r, z, r);
		createBar (0, y - r, z, r);

		// Vertical bars:
		createBar (r, y, z, r);
		createBar (-r, y, z, r);

		// Add to list for checking whether user went through hoop:
		// Main problem is going to be that user's height could be legitimately outside the range by the time the check occurs, but that would still trigger a Loss.


		// We should just do this when we create the hoop, not inside this function, then don't need the bool.
		if (fromScript) {

			int[] data = new int[] {y, r, z};
			all_obstacles.Add (data);
			// Of course: Just push the whole Hoop...
		}
		// NOTE: This is going to add all the ones we manually positioned at the beginning! (Of which there are 15).
		// Fixed that by wrapping in conditional.
	}
}
