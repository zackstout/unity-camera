
using UnityEngine;

public class cam : MonoBehaviour {

	public Vector3 offset;
	public bool isJumping = false;
	public Rigidbody player;


	// Going to be a bit more involved to ensure that MOVEMENT keys move us RELATIVE to our current angle...

	// Use this for initialization
	void Start () {
		Debug.Log ("ahoy hoy!" + player.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		offset.z = -5 * Mathf.Cos(player.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

		offset.x = -5 * Mathf.Sin (player.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

		transform.position = player.transform.position + offset;

		transform.rotation = Quaternion.Euler (0, player.transform.rotation.eulerAngles.y, 0);


	}
}
