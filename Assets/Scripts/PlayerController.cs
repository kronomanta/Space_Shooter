using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed;
    public float Tilt;
    public Boundary Boundary;

    #region shoot
    public GameObject ShootPrefab;
    public Transform ShotSpawn;
    public float FireRate;
    private float _nextFire;
    #endregion

    #region audio
    private AudioSource _audio;
    #endregion

    private Rigidbody _rigid;

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
            Instantiate(ShootPrefab, ShotSpawn.position, ShotSpawn.rotation);
            _audio.Play();
        }
    }

    private void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        _rigid.velocity = new Vector3(moveHorizontal, 0, moveVertical) * Speed;
        _rigid.position = new Vector3(Mathf.Clamp(_rigid.position.x, Boundary.XMin, Boundary.XMax),0, Mathf.Clamp(_rigid.position.z, Boundary.ZMin, Boundary.ZMax));

        _rigid.rotation = Quaternion.Euler(0, 0, _rigid.velocity.x * -Tilt);
    }
}
