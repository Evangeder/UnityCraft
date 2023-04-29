using System.Collections;
using UnityEngine;

namespace UnityCraft.UI.Game.Health
{
    public class HealthBarHurtAnimationModern : BaseHealthBarHurtAnimation
    {
        private readonly int cycles = 4;
        private readonly int offset = 1;
        private readonly float waitSecondsPerCycle = 0.15f;

        public override IEnumerator Animate(BaseBar bar, byte lastValue, byte value)
        {
            var originalPosition = bar.Containers[0].RectTransform.localPosition;

            for (int cycle = 0; cycle < cycles; cycle++)
            {
                bool halfCycle = cycle % 2 == 0;
                int localOffset = halfCycle ? offset : -offset;
                var containerSprite = halfCycle ? bar.Container : bar.ContainerDamage;
                bar.UpdateDamagedBar(halfCycle ? (byte)0 : lastValue);

                for (int i = 0; i < bar.Containers.Length; i++)
                {
                    float newY = i % 2 == 0 ? originalPosition.y + localOffset : originalPosition.y - localOffset;
                    var newPosition = new Vector3(bar.Containers[i].RectTransform.localPosition.x, newY, originalPosition.z);
                    bar.Containers[i].RectTransform.localPosition = newPosition;
                    bar.Containers[i].Image.sprite = containerSprite;
                }

                yield return new WaitForSeconds(waitSecondsPerCycle);
            }

            bar.UpdateDamagedBar(0);
            for (int i = 0; i < bar.Containers.Length; i++)
            {
                var newPosition = new Vector3(bar.Containers[i].RectTransform.localPosition.x, originalPosition.y, originalPosition.z);
                bar.Containers[i].RectTransform.localPosition = newPosition;
                bar.Containers[i].Image.sprite = bar.Container;
            }
        }
    }
}