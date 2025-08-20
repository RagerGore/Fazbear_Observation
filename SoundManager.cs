using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<GameObject> soundObjects = new List<GameObject> ();

    void Update()
    {
        if(CamereBehaviour.isPaused)
        {
            for(int i = 0; i < soundObjects.Count; i++)
            {
                soundObjects[i].GetComponent<AudioSource>().Pause();
            }
        }
        else
        {
            for (int i = 0; i < soundObjects.Count; i++)
            {
                soundObjects[i].GetComponent<AudioSource>().UnPause();
            }
        }
    }
}
