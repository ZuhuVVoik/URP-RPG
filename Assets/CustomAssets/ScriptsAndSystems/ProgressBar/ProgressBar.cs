using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [Header("References")]
    public Slider slider;
    public Text procText;

    private float processTime = 1f;
    private float currentStrength = 1f;
    public void StartProgressBar(float processTime)
    {
        this.gameObject.SetActive(true);

        this.processTime = processTime;

        CalcStrength();
        StartCoroutine(ProgressRoutine());
    }
    IEnumerator ProgressRoutine()
    {
        while(slider.value < 1)
        {
            slider.value += currentStrength;

            procText.text = Mathf.Ceil(slider.value * 100) + "%";

            yield return null;
        }

        EndProgress();
    }
    private void EndProgress()
    {
        processTime = 1f;
        currentStrength = 1f;
        slider.value = 0;
        this.gameObject.SetActive(false);
    }
    private void CalcStrength()
    {
        currentStrength = (1 / processTime) * Time.deltaTime;
    }
    public void StopProgress()
    {
        StopAllCoroutines();
        EndProgress();
    }
}
