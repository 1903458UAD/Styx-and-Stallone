using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour
{

    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent<Animator>(out animator))
        {
            animator = other.GetComponent<Animator>();
            animator.enabled = false;
            StartCoroutine(Reanimate());
        }
    }

    private IEnumerator Reanimate()
    {
        yield return new WaitForSeconds(3);

        animator.enabled = true;
    }
}
