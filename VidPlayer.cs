using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] string badEndVideoFileName;
    [SerializeField] string goodEndVideoFileName;

    private string currentVideoFileName;

    // Start is called before the first frame update
    void Start()
    {
        if (!AnomalyBehaviour.gameStatus)
        {
            currentVideoFileName = badEndVideoFileName;
            this.GetComponent<VideoPlayer>().isLooping = true;
        }

        if (AnomalyBehaviour.gameStatus)
        {
            currentVideoFileName = goodEndVideoFileName;
            this.GetComponent<VideoPlayer>().isLooping = false;
        }

        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if(videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, currentVideoFileName);
            UnityEngine.Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }
}
