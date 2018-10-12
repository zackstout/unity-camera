
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

	public int totalScore = 0;


	// Idea still is flawed [not "won't work"]: we want to check whether y value is within certain range IF .... what? Similar to problem we ran into with detecting WHEN to build a new obstacle.
	// So how do we tell whether the player has passed through a hoop? 

	public List<int> obstacle_ys = new List<int>();


	void Start () {
		// Well... at least a step about creating them manually in the GUI:
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

			Debug.Log ("got a coin");
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

		if (rb.transform.position.z > zCounter) {
			createHoop (Random.Range(3, 15), Random.Range(3, 9), zCounter + starting_dist);
			zCounter += obstacle_interval;

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
		coin.name = "coin"; // allowing for deletion upon collision

		coin.GetComponent<Collider>().isTrigger = true;

	}


	void createHoop (int y, int r, int z) {
		// horizontal bars:
		createBar (0, y + r, z, r);
		createBar (0, y - r, z, r);

		// vertical bars:
		createBar (r, y, z, r);
		createBar (-r, y, z, r);

		// Add to list for checking:
		obstacle_ys.Add(y);
	}

}
