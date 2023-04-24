using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI
{
    public class SplashScreenText : MonoBehaviour
    {
        [SerializeField]
        private float textScale = 2f;
        [SerializeField]
        private float bounceSpeed = 6f;
        [SerializeField]
        private float bounceScale = 0.06f;

        private float currentScale = 0f;

        private void Start()
        {
            GetComponent<Text>().text = new SplashScreenMessages().GetRandomMessage();
        }

        void Update()
        {
            currentScale += Time.deltaTime * bounceSpeed;
            var scale = Mathf.Abs(Mathf.Sin(currentScale)) * bounceScale + textScale;

            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
