using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.LevelMenu.LevelChoosers
{
    public class LevelChooser : MonoBehaviour
    {
        [SerializeField] private Levels _level;
        [SerializeField] private Button _button;

        private LevelChooserPresenter _levelChooserPresenter;

        private void OnEnable() =>
            _button.onClick.AddListener(OnClick);

        private void OnDisable()
        {
            Hide();
            _button.onClick.RemoveListener(OnClick);
        }

        public void Construct(LevelChooserPresenter levelChooserPresenter) =>
            _levelChooserPresenter = levelChooserPresenter;

        public void Hide()
        {
            _button.interactable = false;
            _button.GetComponent<Image>().color = new Color(0, 0, 0);
        }

        public void Show()
        {
            _button.GetComponent<Image>().color = new Color(0, 255, 32);
            _button.interactable = true;
        }

        private void OnClick() =>
            _levelChooserPresenter.StartGame(_level.ToString());
    }
}