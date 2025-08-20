using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GoldenFreddy : MonoBehaviour
{
    [SerializeField] List<GameObject> scares = new List<GameObject>();
    [SerializeField] GameObject scareAudio;

    private float counter = 0.0f, deathCounter = 0.0f,inteval = 2.0f;
    private bool canDie = false;

    void Update()
    {
        counter += Time.deltaTime;
        if(canDie)
        {
            deathCounter += Time.deltaTime;
        }

        if((counter >= inteval) && (GameObject.FindWithTag("GameController").GetComponent<CamereBehaviour>().currentCamera == 7))
        {
            generateScares();
            counter = 0.0f;
        }

        if (CamereBehaviour.isReady)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraScreenBehaviour>().SwitchButtons();
            canDie = true;
            CamereBehaviour.isReady = false;
            CamereBehaviour.canPause = false;
        }


        if (deathCounter >= 5.0f)
        {
            inteval = 1.5f;
        }
        if (deathCounter >= 10.0f)
        {
            inteval = 1.0f;
        }
        if (deathCounter >= 15.0f)
        {
            inteval = 0.5f;
        }
        if (deathCounter >= 20.0f)
        {
            StopCoroutine("allucinate");
            cleanUI();
            StartCoroutine(die());
        }
    }

    void generateScares()
    {
        int r = UnityEngine.Random.Range(0, 5);
        StartCoroutine(allucinate(r));
    }

    void cleanUI()
    {
        for(int i = 0; i<scares.Count;i++)
        {
            scares[i].gameObject.SetActive(false);
        }
    }

    private IEnumerator allucinate(int x)
    {
        yield return new WaitForSeconds(0.1f);
        scares[x].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        scares[x].SetActive(false);
    }

    private IEnumerator die()
    {
        scares[5].gameObject.SetActive(true);
        scareAudio.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        UnityEngine.Application.Quit();
    }
}
