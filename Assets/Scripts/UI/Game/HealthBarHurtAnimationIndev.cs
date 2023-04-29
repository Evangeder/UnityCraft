using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCraft.UI.Game.Health
{
    public class HealthBarHurtAnimationIndev : BaseHealthBarHurtAnimation
    {
        private readonly int cycles = 4;
        private readonly float waitSecondsPerCycle = 0.15f;

        public override IEnumerator Animate(BaseBar bar, byte lastValue, byte value)
        {
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                bool halfCycle = cycle % 2 == 0;
                var containerSprite = halfCycle ? bar.Container : bar.ContainerDamage;
                bar.UpdateDamagedBar(halfCycle ? (byte)0 : lastValue);

                for (int i = 0; i < bar.Containers.Length; i++)
                {
                    bar.Containers[i].Image.sprite = containerSprite;
                }

                yield return new WaitForSeconds(waitSecondsPerCycle);
            }

            bar.UpdateDamagedBar(0);
            for (int i = 0; i < bar.Containers.Length; i++)
            {
                bar.Containers[i].Image.sprite = bar.Container;
            }
        }
    }
}