using TMPro;
using UnityEngine;

namespace NOJUMPO
{
    public class ConveyorObjectSpawnerUI : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] TextMeshProUGUI m_amountOneText;
        [SerializeField] TextMeshProUGUI m_amountTwoText;
        [SerializeField] TextMeshProUGUI m_amountThreeText;


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void SetAmountOneText(string text) {
            m_amountOneText.SetText(text);
        }

        public void SetAmountTwoText(string text) {
            m_amountTwoText.SetText(text);
        }

        public void SetAmountThreeText(string text) {
            m_amountThreeText.SetText(text);
        }
    }
}