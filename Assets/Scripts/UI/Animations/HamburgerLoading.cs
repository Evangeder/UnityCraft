using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.Animations
{
    public class HamburgerLoading : MonoBehaviour
    {
        private enum AnimationType
        {
            Horizontal,
            Vertical
        }

        [SerializeField]
        private AnimationType animationType;

        [SerializeField]
        private Image[] dotsToAnimate;
        private float[] dotScale;

        [SerializeField]
        private float animationSpeed;
        [SerializeField]
        private float barScale;

        private void Start()
        {
            dotScale = new float[dotsToAnimate.Length];

            for (int i = 0; i < dotScale.Length; i++)
            {
                dotScale[i] = i * (1f / dotScale.Length);
            }
        }

        private void Update()
        {
            for (int i = 0; i < dotsToAnimate.Length; i++)
            {
                dotScale[i] += Time.deltaTime * animationSpeed;
                var scale = Mathf.Abs(Mathf.Sin(dotScale[i])) * barScale + 1;
                
                switch (animationType)
                {
                    case AnimationType.Horizontal:
                        dotsToAnimate[i].transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
                        break;

                    case AnimationType.Vertical:
                        dotsToAnimate[i].transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);
                        break;
                }
            }
        }
    }
}