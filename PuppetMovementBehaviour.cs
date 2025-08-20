using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject leftPresentTopper,rightPresentTopper,puppetObject;

    private float openingSpeed, puppetSpeed;

    void Start()
    {
        leftPresentTopper.transform.rotation = new Quaternion(0, 0, 0, 0);
        rightPresentTopper.transform.rotation = new Quaternion(0, 0, 0, 0);

        openingSpeed = 0.01f;
        puppetSpeed = 0.0005f;
    }

    void Update()
    {
        if(!Level2AnomalyBehaviour.puppetAgro)
        {
            leftPresentTopper.transform.Rotate(0.0f, -openingSpeed, 0.0f, Space.Self);
            rightPresentTopper.transform.Rotate(0.0f, openingSpeed, 0.0f, Space.Self);
            puppetObject.transform.Translate(new Vector3(0.0f, puppetSpeed, 0.0f));
        }

        /*if(puppetObject.transform.position.z <= -1.3f)
        {
            Vector3 newPosition = new Vector3(puppetObject.transform.position.x, puppetObject.transform.position.y , -1.3f);
            puppetObject.transform.position = newPosition;
        }*/
    }

    public void SetOpeningSpeed(float x) { openingSpeed = x; }
    public void SetPuppetSpeed(float x) { puppetSpeed = x; }
}
