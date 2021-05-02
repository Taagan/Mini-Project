using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour
{
    [SerializeField] private string m_BossName = "boss_name";
    [SerializeField] private Text m_NameText;
    [SerializeField] private Slider m_HealthBar;

    private void Start()
    {
        m_NameText.text = m_BossName;
    }

    public void Notify(BossModel model)
    {
        m_HealthBar.value = model.Health / model.MaxHealth;
    }
}
