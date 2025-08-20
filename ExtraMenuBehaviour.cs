using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraMenuBehaviour : MonoBehaviour
{
    [SerializeField] List<TMP_Text> stats = new List<TMP_Text>();

    void Start()
    {
        SetStatistics();
    }

    public void SetStatistics()
    {
        stats[0].text = PlayerPrefs.GetInt("RemovedCount").ToString();
        stats[1].text = PlayerPrefs.GetInt("MistakeCount").ToString();
        stats[2].text = PlayerPrefs.GetInt("LossesCount").ToString();
    }
}
