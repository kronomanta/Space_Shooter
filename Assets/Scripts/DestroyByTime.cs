using UnityEngine;

public class DestroyByTime : MonoBehaviour {
    public float LifetimeInSecond;
	void Start () {
        Destroy(gameObject, LifetimeInSecond);
	}
}
