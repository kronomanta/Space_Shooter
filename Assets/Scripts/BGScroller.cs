using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float ScrollerSpeed;
    private float _tileSizeZ;

    private Vector3 _startPosition;
    private Transform _transform;

	void Start () {
        _transform = transform;
        _startPosition = _transform.position;
        _tileSizeZ = _transform.localScale.y;
	}

    // Update is called once per frame
    void Update()
    {
        _transform.position = _startPosition + Vector3.forward * Mathf.Repeat(Time.time * ScrollerSpeed, _tileSizeZ);
    }
}
