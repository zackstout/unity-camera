
using System; // needed for Math
using UnityEngine;
using System.Collections.Generic; // need this to use Lists
using System.Collections;

public class jump : MonoBehaviour {
	// Player control:
	public Rigidbody rb; // refers to itself...
	public int forwardForce = 35;
	public int sidewaysForce = 400;

	// Materials:
	public Material wall_mat;
	public Material wall_mat2;
	public Material coin_mat;

	// Generating the obstacles and coins:
	private int zCounter = 50;
	public int obstacle_interval = 50;
	public int obstacle_starting_dist = 500;
	public int first_obstacle = 70;
	public int coin_starting_dist = 50;
	public int leeway = 2;

	// Checking whether player went through the hoop:
	public int pass_z = 70; // huh why can't we reference that variable?
	public int pass_y;
	public int pass_r;

	// For text on the screen:
	public int totalScore = 0;
	public string message = "Keep it up!";

	public List<int[]> all_obstacles = new List<int[]>();

	// Note: need some way to End the game, ask if want to restart. Should listen for colliding to obstacles as well as missing them.

	void Start () {

		// Generate initial 15 obstacles:
		for (int i = 0; i < 15; i++) {
			int y = UnityEngine.Random.Range (3, 15);
			int r = UnityEngine.Random.Range (3, 9);
			int z = first_obstacle + i * 30;
			createHoop (y, r, z);

			int[] data = new int[] {y, r, z};
			all_obstacles.Add (data);
		}

		// Add an initial forward force to the player:
		rb.AddForce (0, 0, 45, ForceMode.VelocityChange);

		// Create the coins:
		for (int i = coin_starting_dist; i < coin_starting_dist + 1000; i++) {
			if (i % 15 == 0) {
				createCoin (UnityEngine.Random.Range(2, 18), i);
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.name == "coin") {
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
			int y = UnityEngine.Random.Range (3, 15);
			int r = UnityEngine.Random.Range (3, 9);
			int z = (int) zCounter + obstacle_starting_dist;
			createHoop (y, r, z); 

			int[] data = new int[] {y, r, z};
			all_obstacles.Add (data);

			zCounter += obstacle_interval;
		}

		// Strictly speaking not needed -- could just do this in the following conditional:
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
		// Main problem is going to be that user's height could be legitimately outside the range by the time the check occurs, but that would still trigger a Loss.

	}


	void checkForPass(int y, int r, float py) {
		float diff = Math.Abs (y - py);

		if (diff > (r + leeway)) {
			message = "You screwed the pooch!";
		}
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


	void createHoop (int y, int r, int z) {
		// Horizontal bars:
		createBar (0, y + r, z, r);
		createBar (0, y - r, z, r);

		// Vertical bars:
		createBar (r, y, z, r);
		createBar (-r, y, z, r);
	}
}
