using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ExplorTheCampus;

public class GameController : MonoBehaviour {
	
	public Camera cam;
    public GameObject[] exams;
	public Text timerText;
	public Text gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HatController hatController;
    public Score score;

    public Module module;
    public int maxItems = 1;
    public float minSeconds = 1f;
    public float maxSeconds = 2f;

    public float time;
    private float timeLeft;
   
    private float maxWidth;
	private bool counting;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
        timeLeft = time;

		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = exams[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);
	}

	void FixedUpdate () {
		if (counting) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
                timeLeft = 0;
			}
			timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);
		}
	}

	public void StartGame () {
		//splashScreen.SetActive (false);
		startButton.SetActive (false);
		hatController.ToggleControl (true);
		StartCoroutine (Spawn ());
	}

	public IEnumerator Spawn () {
		yield return new WaitForSeconds (2.0f);
		counting = true;
		while (timeLeft > 0) {
            for (int i = 0; i < maxItems; i++)
            {
			    GameObject exam = exams [Random.Range (0, exams.Length)];
			    Vector3 spawnPosition = new Vector3 (
				    transform.position.x + Random.Range (-maxWidth, maxWidth), 
				    transform.position.y, 
				    0.0f
			    );
			    Quaternion spawnRotation = Quaternion.identity;
			    Instantiate (exam, spawnPosition, spawnRotation);
            }
            yield return new WaitForSeconds (Random.Range (minSeconds, maxSeconds));
		}
		yield return new WaitForSeconds (2.0f);
        StopGame();
		yield return new WaitForSeconds (2.0f);
		restartButton.SetActive (true);
	}

    private void StopGame()
    {
        float mark = 5 - (score.Points / time);
        module.Mark = mark;
        gameOverText.text = "Mark: " + module.Mark + "\nScore" + module.Score;
    }

    public void LoadScene()
    {
        StartCoroutine(SceneManager.instance.LoadMainScene());
    }
}