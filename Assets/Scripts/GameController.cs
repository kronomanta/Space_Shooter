using UnityEngine;

public class GameController : MonoBehaviour
{
    enum State
    {
        Game,
        GameOver
    }

    public GameObject[] HazardPrefabs;
    public Vector3 SpawnValues;
    public int HazardCount;
    public float SpawnWaitInSecond;
    public float StartWaitInSecond;
    public float WaveWaitInSecond;

    #region GUI
    public UnityEngine.UI.Text ScoreText;
    public GameObject RestartButton;
    public UnityEngine.UI.Text GameoverText;
    private int _score;
    #endregion

    private State _state = State.Game;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        GameoverText.enabled = false;
        RestartButton.SetActive(false);

        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private System.Collections.IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(StartWaitInSecond);

        while (_state == State.Game)
        {

            for (int i = 0; i < HazardCount; i++)
            {
                //spawn behind the player:

                //bool flag = Random.value > 0.5f;
                //if (flag)
                //{
                //    SpawnValues.z(16 or - 16)
                //}

                Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                /*GameObject hazard =*/
                Instantiate(HazardPrefabs[Random.Range(0, HazardPrefabs.Length)], spawnPosition, Quaternion.identity);

                //if (flag)
                //{
                //    ReverseDirection(hazard);
                //}

                yield return new WaitForSeconds(SpawnWaitInSecond);
            }

            yield return new WaitForSeconds(WaveWaitInSecond);
        }

        
    }

    private void ReverseDirection(GameObject clone)
    {
        Quaternion rotation = clone.transform.rotation;
        clone.transform.rotation = rotation * Quaternion.Euler(0, 180, 0); //turn back
        Mover moverScript = clone.GetComponent<Mover>();
        moverScript.Speed *= -1;
    }

    private void UpdateScore()
    {
        ScoreText.text = "Score: " + _score;
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScore();

        if (score > 0 && _score % 200 == 0)
            _playerController.IncreaseLevel();
    }

    public void GameOver()
    {
        RestartButton.SetActive(true);
        GameoverText.enabled = true;
        _state = State.GameOver;
    }

    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            // Exit condition for Desktop devices
            if (Input.GetKey("escape"))
                Application.Quit();
        }
        else
        {
            // Exit condition for mobile devices
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }

}
