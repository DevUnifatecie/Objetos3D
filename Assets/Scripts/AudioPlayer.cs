using DG.Tweening;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider musicDurationSlider;
    [SerializeField] private CanvasGroup canvasSlider;

    [Header("Play/Pause")]
    [SerializeField] private Image pausePlayImg;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;

    [Header("Audio Speed")]
    [SerializeField] private CanvasGroup canvasSpeed;
    [SerializeField] private TextMeshProUGUI speedText;
    private float currentSpeed = 1f;


    private AudioSource sourceRef;

    private void Start()
    {
        currentSpeed = 1f;

        sourceRef = GetComponent<AudioSource>();
        UpdateVisualMusic();
    }

    private void Update()
    {
        if (sourceRef)
        {
            if (sourceRef.time >= musicDurationSlider.maxValue)
            {
                sourceRef.time = 0f;
                sourceRef.Pause();
                pausePlayImg.sprite = playSprite;
                return;
            }

            if (sourceRef.time <= musicDurationSlider.maxValue)
            {
                musicDurationSlider.value = sourceRef.time;
            }
        }
    }

    public void UpdateVisualMusic()
    {
        musicDurationSlider.value = 0;
        musicDurationSlider.maxValue = sourceRef.clip.length;
    }

    public void PlayPauseSong()
    {
        if (sourceRef.isPlaying)
        {
            pausePlayImg.sprite = playSprite;
            sourceRef.Pause();
        }
        else
        {
            pausePlayImg.sprite = pauseSprite;
            sourceRef.Play();

            if(canvasSlider.alpha == 0)
            {
                canvasSlider.DOFade(1f, 0.66f);
                canvasSpeed.DOFade(1f, 0.66f);
            }
        }
    }

    public void OnSlideValueChange(float newTime)
    {
        sourceRef.time = newTime;
    }

    public void ChangeAudioSpeed()
    {
        if(currentSpeed == 1f)
        {
            ChangeAudioSpeed(1.5f);
            return;
        }
        else if (currentSpeed == 1.5f)
        {
            ChangeAudioSpeed(2f);
            return;
        }
        else if (currentSpeed == 2f)
        {
            ChangeAudioSpeed(0.5f);
            return;
        }
        else
        {
            ChangeAudioSpeed(1f);
            return;
        }
    }

    private void ChangeAudioSpeed(float speed)
    {
        currentSpeed = speed;
        sourceRef.pitch = currentSpeed;

        if(speed != 1 && speed != 2)
            speedText.text = currentSpeed.ToString("F1", CultureInfo.InvariantCulture) + "x";
        else
            speedText.text = currentSpeed.ToString("F0", CultureInfo.InvariantCulture) + "x";
    }
}
