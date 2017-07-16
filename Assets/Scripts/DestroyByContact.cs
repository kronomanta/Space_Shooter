using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject Explosion;

    public int ScoreValue;
    private GameController _gameController = null;

    private void Start()
    {
        var gameControllerGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerGameObject != null)
            _gameController = gameControllerGameObject.GetComponent<GameController>();

        if (_gameController == null)
            Debug.LogError("GameController not found");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) return;

        if (Explosion != null)
            Instantiate(Explosion, transform.position, transform.rotation);

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Death();
            _gameController.GameOver();
        }

        _gameController.AddScore(ScoreValue);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
