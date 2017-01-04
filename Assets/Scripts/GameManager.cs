using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using CnControls;


namespace ExplorTheCampus {

    /// <summary>
    /// This script is responsible for controlling the whole game.
    /// Any changes to the game data (score, etc.) should be delegated to this class.
    /// </summary>
    public class GameManager : MonoBehaviour {

        private const string SEMESTER = "semester";

        public static GameManager instance = null;

        public GameObject canvas;
        public GameObject control;
        public PlayerController playerController;
        public int maxAmountSemester = 7;

        [Tooltip("The maximum amount of modules which could be repeated in the whole game --> 2 in Reutlingen University")]
        public int maxRetriesModules = 2;
        [Tooltip("The maximum amount of retries for a module --> 3 in Reutlingen University.")]
        public int maxRetriesPerModule = 3;
        [Tooltip("The amount of modules per semester. The element index+1 corresponds to the semester. Element 0 would be Semester 1")]
        public int[] amountModulesPerSemester = new int[7];
 
        private int amountMinigames;
        private GameDataContainer gameData;
        private Dictionary<string, int> semesterModuleMapping;
        private Vector3 lastPlayerPosition;
        private float[] lastPlayerDirection;
        private int maxCredits;
        /// <summary>
        /// The gamedata which should be persisted.
        /// This inner class should never ever contain any methods!
        /// Only the GameManager is allowed to change the attributes of the Data Container.
        /// </summary>
        [System.Serializable]
        private class GameDataContainer {

            public string userName;
            public int semester = 1; //Level of the user
            public int amountRetriedModules = 0;
            public int attempts = 5; //Lifes of the user
            public float credits = 0; //Score of the user
            public List<Record> records = new List<Record>(); //History
            public List<int> moduleIdsAlreadyPassed = new List<int>();
            public float[] playerPosition = new float[2];
            public float[] playerDirection = new float[2];
        }

        /// <summary>
        /// Add a history record to the list of all records.
        /// Determine if the user has lost and calculate the new attributes.
        /// </summary>
        /// <param name="record"></param>
        public void AddRecord(Record record)
        {
            gameData.records.Add(record);
            gameData.credits += record.Score;

            if (!record.Missed)
            {
                gameData.moduleIdsAlreadyPassed.Add(record.GameId);
            } else {
                gameData.attempts--;
            }
            if (CheckExma())
            {
                Debug.Log("game over");
            }
            if (gameData.moduleIdsAlreadyPassed.Count == semesterModuleMapping[SEMESTER + gameData.semester + ""])
            {
                gameData.semester++;
                gameData.moduleIdsAlreadyPassed.Clear();
                Debug.Log("next level");
            }
        }

        /// <summary>
        /// Check if the user has lost the game.
        /// </summary>
        /// <returns>True if the user has lost. False if not.</returns>
        private bool CheckExma()
        {
            int count = 0;
            foreach (Record record in gameData.records)
            {
                if (record.Missed)
                {
                    for (int i = 0; i < gameData.records.Count; i++)
                    {
                        if (record.GameId == gameData.records[i].GameId && record.TimeStemp != gameData.records[i].TimeStemp)
                        {
                            count++;
                        }
                        if (count == maxRetriesPerModule - 1)
                        {
                            gameData.amountRetriedModules++;
                        }
                        if (count == maxRetriesPerModule || gameData.amountRetriedModules > maxRetriesModules)
                        {
                            return true;
                        }
                    }
                }
            }
            if (gameData.attempts == 0)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Load a random minigame which has not been played.
        /// </summary>
        public void LoadRandomMinigame()
        {
            List<int> moduelIdsNotPlayed = new List<int>();
            Debug.Log("Amount min0 " + amountMinigames);
            for (int i = 0; i < amountMinigames; i++)
            {
                if (!gameData.moduleIdsAlreadyPassed.Contains(i))
                {
                    moduelIdsNotPlayed.Add(i + 3);
                }
            }
            int gameId = moduelIdsNotPlayed[Random.Range(0, moduelIdsNotPlayed.Count)];
            AllowPlayerMovement(false);
            ShowControl(false);
            GameObject player = GameObject.Find("Player");
            Transform playerTransform = player.GetComponent<Transform>();
            Animator playerAnimator = player.GetComponent<Animator>();
            lastPlayerPosition = playerTransform.position;
            lastPlayerDirection[0] = playerAnimator.GetFloat("input_x");
            lastPlayerDirection[1] = playerAnimator.GetFloat("input_y");
            StartCoroutine(SceneManager.instance.LoadScene(gameId));
        }
        
        public void StartGame()
        {
            int firstScene = 2;
            if (gameData.userName == null)
            {
                firstScene = 1;
            }
            StartCoroutine(SceneManager.instance.LoadScene(firstScene));
            //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            ShowControl(true);
        }

        /// <summary>
        /// Return the current level of the player.
        /// </summary>
        public int Semester
        {
            get
            {
                return gameData.semester;
            }
        }

        public string UserName
        {
            get
            {
                return gameData.userName;
            }

            set
            {
                Debug.Log("USERNAME: " + value);
                gameData.userName = value;
            }
        }

        public int MaxCredits
        {
            get
            {
                return maxCredits;
            }
            set
            {
                maxCredits = value;
            }
        }

        public float Attempts
        {
            get
            {
                return gameData.attempts;
            }
        }

        public float Credits
        {
            get
            {
                return gameData.credits;
            }
        }

        public Vector3 LastPlayerPosition
        {
            get
            {
                return lastPlayerPosition;
            }
        }

        public float[] LastPlayerDirection
        {
            get
            {
                return lastPlayerDirection;
            }
        }

        public Vector3 PersistedPlayerPosition
        {
            get
            {
                return new Vector3(gameData.playerPosition[0], gameData.playerPosition[1], -1);
            }
        }

        public float[] PersistedPlayerDirection
        {
            get
            {
                return gameData.playerDirection;
            }
        }

        private void Initialize()
        {
            amountMinigames = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 3;
            semesterModuleMapping = new Dictionary<string, int>();
            lastPlayerDirection = new float[2];

            for (int i = 0; i < amountModulesPerSemester.Length; i++)
            {
                semesterModuleMapping.Add(SEMESTER + (i + 1), amountModulesPerSemester[i]);
            }

            LoadPersistedGameData();
        }

        void Awake() {

            Initialize();
            ShowControl(false);

            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

        }

        void Update() {
            
            if (CnInputManager.GetButtonUp("Fire2"))
            {
                PauseGame(true);
            }
            
        }

        void OnApplicationQuit()
        {
            if (SettingsManager.instance.GetSaveOnQuit()) {
                SaveGameData();
            }
        }

        public void AllowPlayerMovement(bool allow) 
        {
            ((PlayerController) GameObject.Find("Player").GetComponent("PlayerController")).AllowedToMove = allow;
            control.transform.GetChild(0).gameObject.SetActive(allow);
        }

        public void PauseGame(bool pause)
        {
            canvas.SetActive(pause);
        }

        public void ShowControl(bool enable)
        {
            if (control)
            {
                control.SetActive(enable);
            }
        }

        /// <summary>
        /// Load the persistet game data and reinitialize the gameData attribute.
        /// </summary>
        public void LoadPersistedGameData()
        {
            
            GameDataContainer persistedGameData = (GameDataContainer) PersistenceService.Load("gameData");
            if (persistedGameData != null)
            {
                gameData = persistedGameData;
            }
            else
            {
                gameData = new GameDataContainer();
            }
            Debug.Log("Credits: " + gameData.credits);
            Debug.Log("Semester: " + gameData.semester);
            Debug.Log("Attempts: " + gameData.attempts);
            Debug.Log("Amount retried modules: " + gameData.amountRetriedModules);
        } 

        public void SaveGameData()
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                Transform playerTransform = player.GetComponent<Transform>();
                Animator playerAnimator = player.GetComponent<Animator>();
                gameData.playerDirection[0] = playerAnimator.GetFloat("input_x");
                gameData.playerDirection[1] = playerAnimator.GetFloat("input_y");
                gameData.playerPosition[0] = playerTransform.position.x;
                gameData.playerPosition[1] = playerTransform.position.y;
            }
            PersistenceService.Save(gameData, "gameData");
        }

        /// <summary>
        /// Reset all persistet game data.
        /// </summary>
        public void ResetGame()
        {
            gameData = new GameDataContainer();
            SaveGameData();
            SceneManager.instance.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}