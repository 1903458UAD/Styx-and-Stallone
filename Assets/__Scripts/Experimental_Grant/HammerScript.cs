using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour
{

    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if(other.CompareTag("Player") && other.TryGetComponent<Animator>(out anim))
        {
            anim = other.GetComponent<Animator>();
            anim.enabled = false;
            StartCoroutine(Reanimate());
        }
    }

    private IEnumerator Reanimate()
    {
        yield return new WaitForSeconds(3);

        anim.enabled = true;
    }
}
