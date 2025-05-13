using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] private Image skipProgress;
    [SerializeField] private float fillDuration;
    [SerializeField] private float dropSpeed;
    private float _currentFill;
    private Action _onPlayEnd;
    private bool _isEnded;
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isEnded)
        {
            skipProgress.fillAmount = 1;
            return;
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            _currentFill += Time.deltaTime;
        }
        else
        {
            _currentFill -= Time.deltaTime * dropSpeed;
            if (_currentFill < 0) _currentFill = 0;
        }

        skipProgress.fillAmount = _currentFill / fillDuration;

        if (skipProgress.fillAmount >= 1)
        {
            PlayerOnloopPointReached(player);
            _isEnded = true;
        }
    }

    public void Play(Action onPlayEnd)
    {
        gameObject.SetActive(true);
        player.Play();

        _onPlayEnd = onPlayEnd;
        player.loopPointReached += PlayerOnloopPointReached;
    }

    private void PlayerOnloopPointReached(VideoPlayer source)
    {
        player.loopPointReached -= PlayerOnloopPointReached;
        source.Stop();

        if (_onPlayEnd != null) _onPlayEnd();
    }
}