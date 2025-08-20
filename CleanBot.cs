using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanBot : MonoBehaviour
{
    [SerializeField] List<Transform> movementPoints = new List<Transform>();
    [SerializeField] float speed;

    private int currentTarget = 0;

    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movementPoints[currentTarget].position, step);
        transform.LookAt(transform.position - (movementPoints[currentTarget].position - transform.position));

        if (Vector3.Distance(transform.position, movementPoints[currentTarget].position) < 0.001f)
        {
            currentTarget++;
            if(currentTarget >= movementPoints.Count)
            {
                currentTarget = 0;
            }
        }
    }
}
