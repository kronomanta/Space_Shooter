using UnityEngine;

public class WeaponController : MonoBehaviour {

    //we should pool the bullets
    public GameObject ShotPrefab;
    public Transform ShotSpawn;
    public float FireRate;
    public float DelayInSecond;
    public AudioClip[] Sounds;

    private AudioSource _audioSource;

	void Start () {
        _audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", DelayInSecond, FireRate);
	}
	
    void Fire()
    {
        Instantiate(ShotPrefab, ShotSpawn.position, ShotSpawn.rotation);
        _audioSource.clip = Sounds[Random.Range(0, Sounds.Length)];
        _audioSource.Play();
    }
}
