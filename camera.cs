
using UnityEngine;

public class cam : MonoBehaviour {

	public Vector3 offset;
	public Quaternion rot;
	float x = 0;
	float y = 0;
	bool isJumping = false;

	// Going to be a bit more involved to ensure that MOVEMENT keys move us RELATIVE to our current angle...

	// Use this for initialization
	void Start () {
		Debug.Log ("ahoy hoy!");
	}
	
	// Update is called once per frame
	void Update () {

		// JUMP (won't work since Camera isn't a Rigid Body):
		if (Input.GetKey("space")) {
			if (isJumping) {
				
			} else {
				isJumping = true;

			}
//			Debug.Log ("space!");
		}

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



		// ROTATE:
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
}
