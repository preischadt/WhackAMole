using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : MonoBehaviour {

    public GameObject ExplosionPrefab;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            Instantiate(ExplosionPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation, null);
        }
        Destroy(this.gameObject);
    }
}
