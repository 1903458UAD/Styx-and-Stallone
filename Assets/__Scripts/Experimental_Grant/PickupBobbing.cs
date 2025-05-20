using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBobbing : MonoBehaviour
{
    private Vector3 posStart;
    private bool updownController; //True = up || False = Down\\
    private bool delayed;

    private void Start()
    {
        updownController = true;
        delayed = false;
        StartCoroutine(bobDelay());
    }

    private void Update()
    {
        if (updownController && delayed)
        {
            transform.position = new Vector3(posStart.x, (transform.position.y + 0.01f), posStart.z);

            if (transform.position.y >= (posStart.y + 0.9f))
            {
                updownController = false;
            }
        }
        else if (!updownController && delayed)
        {
            transform.position = new Vector3(posStart.x, (transform.position.y - 0.01f), posStart.z);

            if(transform.position.y <= (posStart.y - 0.1f))
            {
                updownController = true;
            }
        }
    }

    private IEnumerator bobDelay()
    {
        yield return new WaitForSeconds(2);

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        delayed = true;
        posStart = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
