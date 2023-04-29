using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.Game.Health
{
    public class BaseContainer : MonoBehaviour
    {
        [SerializeField]
        private Image filling;
        [SerializeField]
        private Image fillingDamaged;
        [HideInInspector]
        public RectTransform RectTransform;
        [HideInInspector]
        public Image Image;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            Image = GetComponent<Image>();
        }

        public void Show(Sprite filling)
        {
            this.filling.sprite = filling;
            this.filling.gameObject.SetActive(true);
            this.fillingDamaged.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        public void ShowDamaged(Sprite fillingDamaged)
        {
            this.fillingDamaged.sprite = fillingDamaged;
            this.fillingDamaged.gameObject.SetActive(true);
        }

        public void HideDamaged()
        {
            fillingDamaged.gameObject.SetActive(false);
        }

        public void HideFilling()
        {
            filling.gameObject.SetActive(false);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}