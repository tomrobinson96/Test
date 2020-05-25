using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);

        if (col.gameObject.layer == 9)
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.layer == 10)
        {
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
        if (col.gameObject.tag == "Shield")
        {
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
        if (col.gameObject.tag == "Jet")
        {
            print("Ignore");
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
    }
}
