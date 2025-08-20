using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingObject : MonoBehaviour
{
    [SerializeField] GameObject flashingObject;
    [SerializeField] float counter = 0.0f, inteval = 0.5f;

    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= inteval)
        {
            if (flashingObject.activeSelf)
            {
                flashingObject.SetActive(false);
            }
            else
            {
                flashingObject.SetActive(true);
            }
            counter = 0.0f;
        }
    }
}
