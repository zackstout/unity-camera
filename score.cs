
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

	public jump jumpScript; // Need to drag in the script.
	public Text scoreText; // For this to work we need to drag in the Text, self-reference

	// Update is called once per frame
	void Update () {
		scoreText.text = jumpScript.totalScore.ToString();
	}
}
