using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskThrowController : MonoBehaviour {

    public Transform DiskSpawnPosition;

    public GameObject DiskPrefab;

    private float lastFire = 0;

    public float FireCadence = 3;
    public float DiskSpeed = 2000;

	// Use this for initialization
	void Start () {
		
	}

    public void FireDisk()
    {
        var disk = Instantiate(DiskPrefab, DiskSpawnPosition.position, DiskSpawnPosition.rotation, null);
        disk.GetComponent<Rigidbody>().AddForce(DiskSpawnPosition.forward * DiskSpeed);
        Destroy(disk, 30);
    }
	
	// Update is called once per frame
	void Update () {
		if((Time.time - lastFire) > FireCadence)
        {
            FireDisk();
            lastFire = Time.time;
        }
	}
}
