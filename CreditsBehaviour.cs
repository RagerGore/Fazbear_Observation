using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBehaviour : MonoBehaviour
{
    [SerializeField] List<GameObject> pages = new List<GameObject>();
    [SerializeField] GameObject previousButton, nextButton;
    private int currentPage;

    void Start()
    {
        currentPage = 0;
        previousButton.SetActive(false);
    }

    public void NextPage()
    {
        currentPage++;
        previousButton.SetActive(true);
        if (currentPage == pages.Count - 1)
        {
            nextButton.SetActive(false);
        }
        SetCurrentPage(currentPage);

    }

    public void PreviousPage()
    {
        currentPage--;
        nextButton.SetActive(true);
        if (currentPage == 0)
        {
            previousButton.SetActive(false);
        }
        SetCurrentPage(currentPage);
    }

    private void SetCurrentPage(int currentPage)
    {
        for(int i=0; i < pages.Count;i++)
        {
            pages[i].SetActive(false);
        }
        pages[currentPage].SetActive(true);
    }
}
