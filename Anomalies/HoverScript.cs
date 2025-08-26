using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    [SerializeField]
    float amp, freq;

    private float startingY;

    void Start()
    {
        startingY = this.gameObject.transform.position.y;
    }

    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, Mathf.Sin(Time.time * freq) * amp + startingY, this.gameObject.transform.position.z);
    }
}
