using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float Tumble;

	void Start () {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Tumble;
	}
	
}
