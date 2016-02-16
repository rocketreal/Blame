using UnityEngine;

[ExecuteInEditMode]
public class ParticaleDieController : MonoBehaviour
{

    private ParticleSystem pr = null;
    private bool isActive;

    void Start()
    {
        pr = GetComponent<ParticleSystem>();
        Stop();
    }

    void Update()
    {
        if (isActive && false == pr.IsAlive())
        {
            Stop();
            OnComplete();
        }
    }

    public void Play()
    {
        isActive = true;
        pr.Play();
    }

    private void Stop()
    {
        isActive = false;
        pr.Stop();
    }

    private void OnComplete()
    {
        MainController.Current.EndGame();
        GameData.Instance.canPlay = true;
    }

}
