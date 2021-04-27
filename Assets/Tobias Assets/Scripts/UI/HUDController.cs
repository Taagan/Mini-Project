using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode]
public class HUDController : MonoBehaviour
{
    [Space(3), Header("Bars")]
    [SerializeField] private Slider m_HealthBar;
    [SerializeField] private Slider m_StaminaBar;

    [Space(3), Header("Player Properties")]
    [SerializeField, Range(0, 100)] private float m_PlayerHealth;
    [SerializeField, Range(1, 100)] private float m_PlayerMaxHealth;
    [SerializeField, Range(0, 100)] private float m_PlayerStamina;
    [SerializeField, Range(1, 100)] private float m_PlayerMaxStamina;

    private void Start()
    {
        // Get player properties here
    }

    private void LateUpdate()
    {
        m_HealthBar.value = m_PlayerHealth / m_PlayerMaxHealth;
        m_StaminaBar.value = m_PlayerStamina / m_PlayerMaxStamina;
    }
}
