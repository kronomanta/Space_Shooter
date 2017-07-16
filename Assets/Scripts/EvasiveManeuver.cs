using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public float DodgeSize;
    public float Smoothing;
    public float Tilt;

    public Vector2 StartWaitInSecond;
    public Vector2 ManeuverTimeInSecond;
    public Vector2 ManeuverWaitInSecond;

    private Transform _targetTransform;

    public Boundary Boundary;


    private float _currentSpeed;
    private float _targetManeuver;
    //private Transform _transform;
    private Rigidbody _rigid;

	void Start () {
        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //_transform = transform;
        _rigid = GetComponent<Rigidbody>();
        _currentSpeed = _rigid.velocity.z;

        StartCoroutine(Evade());	
	}
	
	void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards(_rigid.velocity.x, _targetManeuver, Time.deltaTime * Smoothing);
        _rigid.velocity = new Vector3(newManeuver, 0, _currentSpeed);
        _rigid.position = new Vector3(Mathf.Clamp(_rigid.position.x, Boundary.XMin, Boundary.XMax), 0, Mathf.Clamp(_rigid.position.z, Boundary.ZMin, Boundary.ZMax));
        _rigid.rotation = Quaternion.Euler(0, 0, _rigid.velocity.x * -Tilt);
    }

    System.Collections.IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(StartWaitInSecond.x, StartWaitInSecond.y));
        while (_targetTransform != null)
        {
            //keep the ship inside the screen
            //_targetManeuver = Random.Range(1, DodgeSize) * -Mathf.Sign(_transform.position.x);
            _targetManeuver = _targetTransform.position.x;
            yield return new WaitForSeconds(Random.Range(ManeuverTimeInSecond.x, ManeuverTimeInSecond.y));
            _targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(ManeuverWaitInSecond.x, ManeuverWaitInSecond.y));
        }
    }
}
