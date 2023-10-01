using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldText;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider shieldSlider;

    [SerializeField] private Image shieldBG;

    [Header("Health / Shield Max Values")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxShield;

    private int curHealth;
    private int curShield;


    void Start()
    {
        curHealth = maxHealth;
        curShield = 0;

        healthSlider.maxValue = maxHealth;
        shieldSlider.maxValue = maxShield;

        UpdateHealthUI();
        UpdateShieldUI();
    }

    public void DamagePlayer(int damage)
    { 
        if(curShield > 0)
        {
            DamageShield(damage);
            return;
        }

        curHealth -= damage;

        if (curHealth <= 0)
        {
            curHealth = 0;
            PlayerDeath();
        }
        UpdateHealthUI();
    }

    void DamageShield(int damage)
    {
        int remainingDamage = damage - curShield;

        curShield -= damage;
        if (remainingDamage > 0)
        {
            DamagePlayer(remainingDamage);
            curShield = 0;
        }
        UpdateShieldUI();
    }

    public void HealPlayer(int healing)
    {
        curHealth += healing;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        UpdateHealthUI();
    }

    public void AddShieldToPlayer(int shieldAmount)
    {
        curShield += shieldAmount;
        if (curShield > maxShield)
            curShield = maxShield;

        UpdateShieldUI();
    }

    void PlayerDeath()
    {
        // player is dead
    }

    void UpdateHealthUI()
    {
        healthText.text = curHealth.ToString();
        healthSlider.DOValue(curHealth, 0.2f);
    }

    void UpdateShieldUI()
    {
        if (!shieldBG.enabled)
        {
            shieldBG.enabled = true;
            shieldText.enabled = true;
        }
        
        string shieldTextString = "+" + curShield.ToString();
        shieldText.text = shieldTextString;
        shieldSlider.DOValue(curShield, 0.2f);

        if (curShield <= 0)
        {
            shieldBG.enabled = false;
            shieldText.enabled = false;
            return;
        }
    }
}
