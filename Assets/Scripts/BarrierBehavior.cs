using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBehavior : MonoBehaviour
{
    [SerializeField] private float impactPower;

    public void BurstBarrier()
    {
        if (GetComponent<BoxCollider>())
            GetComponent<BoxCollider>().enabled = false;
        else if (GetComponent<SphereCollider>())
            GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10f, 10f), impactPower/3, impactPower), ForceMode.Impulse);
        //GetComponent<AudioSource>().Play();
        RemoveBarrier(1f);
    }

    private IEnumerator RemoveBarrier(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        gameObject.SetActive(false);
    }
}
