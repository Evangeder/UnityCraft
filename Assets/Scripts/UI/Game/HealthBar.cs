using UnityEngine;

namespace UnityCraft.UI.Game.Health
{
    public class HealthBar : BaseBar
    {
        private void Start()
        {
            BarHurtAnimation = new HealthBarHurtAnimationIndev();
        }
    }
}