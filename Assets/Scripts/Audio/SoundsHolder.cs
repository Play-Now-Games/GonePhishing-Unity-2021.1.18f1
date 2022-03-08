using UnityEngine;
using UnityEngine.Audio;

public class SoundsHolder : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip _click;

    [SerializeField]
    private AudioClip[] _spawnPopUps;

    [SerializeField]
    private AudioClip[] _destroyPopUps;

    [SerializeField]
    private AudioClip[] _static;


    public void PlayClick()
    {
        source.PlayOneShot(_click);
    }

    public void PlaySpawnPop()
    {
        int rand = Random.Range(0, _spawnPopUps.Length);

        source.PlayOneShot(_spawnPopUps[rand]);
    }

    public void PlayDestroyPop()
    {
        int rand = Random.Range(0, _destroyPopUps.Length);

        source.PlayOneShot(_destroyPopUps[rand]);
    }

    public void PlayStatic()
    {
        int rand = Random.Range(0, _static.Length);

        source.PlayOneShot(_static[rand]);
    }

}
