//Gabriel 'DiosMussolinos' Vergari
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundsHolder : MonoBehaviour
{

    //Audio
    [SerializeField]
    private AudioClip _click;

    [SerializeField]
    private AudioClip[] _spawnPopUps;

    [SerializeField]
    private AudioClip _backFeedback;

    [SerializeField]
    private AudioClip _goodFeedback;

    [SerializeField]
    private AudioClip[] _destroyPopUps;

    [SerializeField]
    private AudioClip[] _static;


    //GameObjects
    [SerializeField]
    private GameObject _lSpearker;

    [SerializeField]
    private GameObject _rSpearker;

    private AudioSource _leftSide;
    private AudioSource _rightSide;

    public void Start()
    {
        _leftSide = _lSpearker.GetComponent<AudioSource>();
        _rightSide = _rSpearker.GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        _leftSide.PlayOneShot(_click);
        _rightSide.PlayOneShot(_click);
    }

    public void PlayBadFeedback()
    {
        _leftSide.PlayOneShot(_backFeedback);
        _rightSide.PlayOneShot(_backFeedback);
    }

    public void PlayGoodFeedback()
    {
        _leftSide.PlayOneShot(_goodFeedback);
        _rightSide.PlayOneShot(_goodFeedback);
    }

    public void PlaySpawnPop()
    {
        int rand = Random.Range(0, _spawnPopUps.Length);

        _leftSide.PlayOneShot(_spawnPopUps[rand]);
        _rightSide.PlayOneShot(_spawnPopUps[rand]);

    }

    public void PlayDestroyPop()
    {
        int rand = Random.Range(0, _destroyPopUps.Length);

        _leftSide.PlayOneShot(_destroyPopUps[rand]);
        _rightSide.PlayOneShot(_destroyPopUps[rand]);
    }

    public void PlayStatic()
    {
        int rand = Random.Range(0, _static.Length);

        _leftSide.PlayOneShot(_static[rand]);
        _rightSide.PlayOneShot(_static[rand]);
    }
   

}
