using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
    public Animator buttonAnimator;
    public Animator gateAnimator;

    bool isCaseOpened = false;
    bool isGateMoving = false;
    bool isGateOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (isCaseOpened)
        {
            if (isGateMoving)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                isGateMoving = true;

                if (isGateOpen)
                {
                    isGateOpen = false;
                    gateAnimator.SetTrigger("Close");
                }
                else
                {
                    isGateOpen = true;
                    gateAnimator.SetTrigger("Open");
                }

                Invoke("GateOpeningDelay", 3.5f);
            }
        }
    }

    void GateOpeningDelay()
    {
        isGateMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCaseOpened = true;
            buttonAnimator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCaseOpened = false;
            buttonAnimator.SetTrigger("Close");
        }
    }
}
