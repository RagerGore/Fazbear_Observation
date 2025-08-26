using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StaringFreddy : MonoBehaviour
{
    [SerializeField] List<GameObject> lights = new List<GameObject>();
    [SerializeField] GameObject deathVideo;
    [SerializeField] AudioSource breakingSound;

    void Update()
    {
        if(CamereBehaviour.isReady)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraScreenBehaviour>().SwitchButtons();
            StartCoroutine("dyingSequence");
            CamereBehaviour.isReady = false;
            CamereBehaviour.canPause = false;
        }
    }

    private IEnumerator dyingSequence()
    {
        yield return new WaitForSeconds(4.0f);
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5.0f);
        breakingSound.Play();
        lights[0].SetActive(false);
        yield return new WaitForSeconds(2.0f);
        breakingSound.Play();
        lights[1].SetActive(false);
        yield return new WaitForSeconds(2.0f);
        breakingSound.Play();
        deathVideo.SetActive(true);
        yield return new WaitForSeconds(9.2f);
        GameObject.FindWithTag("GameController").GetComponent<AnomalyBehaviour>().SendToMenu("Connection Terminated",false);
    }
}
