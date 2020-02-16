using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageEffect : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public AudioSource audioSource;
    public AudioClip soundEffect;

    public GameObject player;
    float widthTimer = 0;
    float widthTimerMax = 2;

    float disappearTimer = 10f;
HealthBar healthBar;
    public static void Create(Vector3 position)
    {
        Transform rageEffectTransform = Instantiate(GameAssets.i.rageEffect, position, Quaternion.identity);
        RageEffect rageEffect = rageEffectTransform.GetComponent<RageEffect>();
        rageEffect.Setup();
    }
    private void Awake()
    {
        player = GameObject.Find("Player");
        healthBar = Camera.main.GetComponentInChildren<HealthBar>();
        healthBar.health.healthRegenAmount = -3f;
        lineRenderer = transform.GetComponent<LineRenderer>();
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = soundEffect;

    }

    public void Setup()
    {
        Vector2 position = player.transform.position;
        position.y += 3;
        lineRenderer.transform.position = position;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(audioSource.clip, 1);
    }

    private void Update()
    {
        updateRageEffectLines();

    }

    private void updateRageEffectLines()
    {
        updateWidth();
        updatePosition();
        updateDisappearTimer();


    }

    public void updateWidth()
    {

        if (widthTimer > widthTimerMax / 2)
        {
            float increaseScaleAmount = .5f;
            lineRenderer.endWidth += increaseScaleAmount * Time.deltaTime;
            lineRenderer.startWidth += increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = .5f;
            lineRenderer.endWidth -= decreaseScaleAmount * Time.deltaTime;
            lineRenderer.startWidth -= decreaseScaleAmount * Time.deltaTime;
        }
        widthTimer += Time.deltaTime;
        if (widthTimer > widthTimerMax)
        {
            widthTimer = 0f;
        }

    }
    public void updatePosition()
    {

        Vector2 playerPosition = player.transform.position;

        Vector2 firstPoint = playerPosition;
        firstPoint.x -= 1;
        firstPoint.y += 2;

        Vector2 secondPoint = playerPosition;
        secondPoint.x -= 1;
        secondPoint.y += 2.5f;

        Vector2 thirdPoint = playerPosition;
        thirdPoint.x -= .5f;
        thirdPoint.y += 2;

        Vector2 fourthPoint = playerPosition;
        fourthPoint.x += 0;
        fourthPoint.y += 3;

        Vector2 fifthPoint = playerPosition;
        fifthPoint.x += .5f;
        fifthPoint.y += 2;

        Vector2 sixthPoint = playerPosition;
        sixthPoint.x += 1;
        sixthPoint.y += 2.5f;

        Vector2 seventhPoint = playerPosition;
        seventhPoint.x += 1;
        seventhPoint.y += 2;

        Vector3[] positions = { firstPoint, secondPoint, thirdPoint, fourthPoint, fifthPoint, sixthPoint, seventhPoint };
        lineRenderer.SetPositions(positions);

    }

    public void updateDisappearTimer()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            Color startColor = lineRenderer.startColor;
            Color endColor = lineRenderer.endColor;
            startColor.a -= disappearSpeed * Time.deltaTime;
            endColor.a -= disappearSpeed * Time.deltaTime;
            lineRenderer.startColor = startColor;
            lineRenderer.endColor = endColor;
            if (startColor.a < 0)
            {
                healthBar.health.healthRegenAmount = 5f;
                Destroy(gameObject);
            }
        }
    }
}
