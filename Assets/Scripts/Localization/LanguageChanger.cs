using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private List<Toggle> _toggles = new List<Toggle>();

    [field: SerializeField] public LanguageSaveView LanguageSaveView { get; private set; }
    [field: SerializeField] public LanguageToggleView LanguageToggleRU { get; private set; }
    [field: SerializeField] public LanguageToggleView LanguageToggleEN { get; private set; }
    [field: SerializeField] public LanguageToggleView LanguageToggleTR { get; private set; }

    private ToggleGroup _toggleGroup;

    private void Start()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
        SetCurrentToggleActive();
    }

    private void OnDisable()
    {
        foreach (Toggle toggle in _toggles)
            toggle.gameObject.SetActive(false);
    }

    private void SetCurrentToggleActive()
    {
        int activeToggleIndex = PlayerPrefs.GetInt("LanguageIndex");

        _toggles[activeToggleIndex].isOn = true;

        foreach (Toggle toggle in _toggles)
            toggle.gameObject.SetActive(true);
    }

    public void ChangeLanguage()
    {
        Toggle activeToggle = _toggleGroup.GetFirstActiveToggle();
        int languageIndex = activeToggle.transform.GetSiblingIndex();

        PlayerPrefs.SetInt("LanguageIndex", languageIndex);
        PlayerPrefs.SetInt("LanguageWasChanged", 1);
    }
}