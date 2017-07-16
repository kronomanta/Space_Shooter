using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public float Tilt;
    public Boundary Boundary;

    public SimpleTouchPad TouchPad;
    public SimpleTouchAreaButton TouchButton;

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

    private Quaternion _calibrationQuaternion;

    private void Start()
    {
        CalibrateAccelerometer();
        _rigid = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (/*TouchButton.CanFire()*/ Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + FireRate;

            foreach (Transform shotSpawn in ShotSpawnConfigs[_currentLevel])
                Instantiate(ShootPrefab, shotSpawn.position, shotSpawn.rotation);
            _audio.Play();
        }
    }

    private void FixedUpdate()
    {
        //desktop
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            _rigid.velocity = new Vector3(moveHorizontal, 0, moveVertical) * Speed;
        }
        else
        {

            //mobile
            //Vector3 acceleration = FixAcceleration(Input.acceleration);
            //_rigid.velocity = new Vector3(acceleration.x, 0, acceleration.y) * Speed;

            Vector2 direction = TouchPad.GetDirection();

            _rigid.velocity = new Vector3(direction.x, 0, direction.y) * Speed;
        }

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

    private void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(Vector3.back, accelerationSnapshot);
        _calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    private Vector3 FixAcceleration(Vector3 acceleration)
    {
        return _calibrationQuaternion * acceleration;
    }
}
