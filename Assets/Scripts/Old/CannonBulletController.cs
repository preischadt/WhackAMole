using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletController : MonoBehaviour {

    public GameObject ExplosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionPrefab, collision.contacts[0].point, Quaternion.identity, null);
        Destroy(this.gameObject);
    }
}
