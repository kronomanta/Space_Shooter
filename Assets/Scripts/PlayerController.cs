using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public float Tilt;
    public Boundary Boundary;

    public GameObject PlayerExplosion;

    #region shoot
    public GameObject ShootPrefab;
    public TransformList ShotSpawnConfigs;
    public float FireRate;
    private float _nextFire;
    #endregion

    #region audio
    private AudioSource _audio;
    #endregion

    private Rigidbody _rigid;
    private int _currentLevel = 0;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + FireRate;

            foreach (Transform shotSpawn in ShotSpawnConfigs[_currentLevel])
                Instantiate(ShootPrefab, shotSpawn.position, shotSpawn.rotation);
            _audio.Play();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        _rigid.velocity = new Vector3(moveHorizontal, 0, moveVertical) * Speed;
        _rigid.position = new Vector3(Mathf.Clamp(_rigid.position.x, Boundary.XMin, Boundary.XMax), 0, Mathf.Clamp(_rigid.position.z, Boundary.ZMin, Boundary.ZMax));

        _rigid.rotation = Quaternion.Euler(0, 0, _rigid.velocity.x * -Tilt);
    }

    public void Death()
    {
        Instantiate(PlayerExplosion, transform.position, transform.rotation);
    }

    public void IncreaseLevel()
    {
        _currentLevel = Mathf.Min(_currentLevel + 1, ShotSpawnConfigs.Length - 1);
    }
}
