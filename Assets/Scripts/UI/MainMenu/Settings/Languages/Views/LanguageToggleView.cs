using UI.MainMenu.Settings.Languages.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Settings.Languages.Views
{
    [RequireComponent(typeof(Toggle))]
    public class LanguageToggleView : MonoBehaviour
    {
        private Toggle _toggle;
        private LanguageTogglePresenter _languagePresenter;

        private void Awake() =>
            _toggle = GetComponent<Toggle>();

        private void OnEnable() =>
            _toggle.onValueChanged.AddListener(isActive => OnClicked(isActive));

        private void OnDisable() =>
            _toggle.onValueChanged.RemoveListener(isActive => OnClicked(isActive));

        public void Construct(LanguageTogglePresenter languagePresenter) =>
            _languagePresenter = languagePresenter;

        private void OnClicked(bool isActive) =>
            _languagePresenter.ShowButtonSaveSettings();
    }
}