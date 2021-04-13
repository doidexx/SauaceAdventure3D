using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] Slider slider;
    [SerializeField] float smoothness;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        slider.value = currentHealth / maxHealth;
    }

    private void Update()
    {
        slider.value = Mathf.Lerp(slider.value, currentHealth / maxHealth, smoothness);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if (currentHealth == 0)
            gameObject.SetActive(false);
    }
}
