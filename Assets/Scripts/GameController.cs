using UnityEngine;

public class GameController : MonoBehaviour
{
    enum State
    {
        Game,
        GameOver
    }

    public GameObject HazardPrefab;
    public Vector3 SpawnValues;
    public int HazardCount;
    public float SpawnWaitInSecond;
    public float StartWaitInSecond;
    public float WaveWaitInSecond;

    #region GUI
    public UnityEngine.UI.Text ScoreText;
    public UnityEngine.UI.Text RestartText;
    public UnityEngine.UI.Text GameoverText;
    private int _score;
    #endregion

    private State _state = State.Game;

    private void Start()
    {
        GameoverText.enabled = false;
        RestartText.enabled = false;

        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (_state == State.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    private System.Collections.IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(StartWaitInSecond);

        while (_state == State.Game)
        {

            for (int i = 0; i < HazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                Instantiate(HazardPrefab, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(SpawnWaitInSecond);
            }

            yield return new WaitForSeconds(WaveWaitInSecond);
        }

        //gives the player some time to relax
        RestartText.enabled = true;
    }

    private void UpdateScore()
    {
        ScoreText.text = "Score: " + _score;
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScore();
    }

    public void GameOver()
    {
        GameoverText.enabled = true;
        _state = State.GameOver;
    }

}
