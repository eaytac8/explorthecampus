using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public Text scoreText;
	public int examAPoints;
    public int examFPoints;
    
	private int score;

    public int Points
    {
        get
        {
            return score;
        }
    }

	void Start () {
		score = 0;
		UpdateScore ();
	}

	void OnTriggerEnter2D (Collider2D other) {
        //other.GetComponent(ItemPoints);
        score += examAPoints;
		UpdateScore ();
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Bomb") {
			score += examFPoints;
			UpdateScore ();
		}
	}

	void UpdateScore () {
		scoreText.text = "SCORE:\n" + score;
	}
}
