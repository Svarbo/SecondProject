using TMPro;
using UnityEngine;

namespace Oranges
{
    public class OrangesCountText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;

        public void SetCountText(int collectedOrangesCount, int allOrangesCount) =>
            _countText.text = $"{collectedOrangesCount}/{allOrangesCount}";
    }
}