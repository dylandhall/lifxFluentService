using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LightController
{
    public static class TimeConfig
    {
        public static DaynightUpdate[] Daynights = new[] {
            new DaynightUpdate(hour: 0f, kelvin: 1500, brightness: 0.01f),
            new DaynightUpdate(hour: 6.75f, kelvin: 1500, brightness: 0.01f),
            new DaynightUpdate(hour: 7.25f, kelvin: 3000, brightness: 0.5f),
            new DaynightUpdate(hour: 7.5f, kelvin: 6500, brightness: 1),
            new DaynightUpdate(hour: 15, kelvin: 6500, brightness: 1),
            new DaynightUpdate(hour: 20, kelvin: 2000, brightness: 0.25f),
            new DaynightUpdate(hour: 21.5f, kelvin: 1500, brightness: 0.1f),
            new DaynightUpdate(hour: 22.5f, kelvin: 1500, brightness: 0.01f),
            new DaynightUpdate(hour: 24f, kelvin: 1500, brightness: 0.01f)
        }.OrderBy(a => a.hour).ToArray();


        public static FullColourUpdate[] Fullcolour = new[] {
            new FullColourUpdate(hour: 0, Color.Red, brightness: 0.01f),
            new FullColourUpdate(hour: 6.75f, Color.Red, brightness: 0.01f),
            new FullColourUpdate(hour: 7.25f, Color.Yellow, brightness: 0.5f),
            new FullColourUpdate(hour: 7.5f, Color.SkyBlue, brightness: 1f),
            new FullColourUpdate(hour: 10, Color.BlueViolet, brightness: 1f),
            new FullColourUpdate(hour: 12, Color.ForestGreen, brightness: 1f),
            new FullColourUpdate(hour: 14, Color.Aquamarine, brightness: 1f),
            new FullColourUpdate(hour: 15.5f, Color.Blue, brightness: 1f),
            new FullColourUpdate(hour: 17, Color.Yellow, brightness: 0.8f),
            new FullColourUpdate(hour: 20,  Color.MediumVioletRed, brightness: 0.3f),
            new FullColourUpdate(hour: 21f, Color.Orange, brightness: 0.2f),
            new FullColourUpdate(hour: 22.5f, Color.Red, brightness: 0.01f),
            new FullColourUpdate(hour: 24, Color.Red, brightness: 0.01f)
        }.OrderBy(a => a.hour).ToArray();
    }

    public class FullColourUpdate
    {
        public FullColourUpdate(float hour, Color color, float brightness)
        {
            this.hour = hour;
            this.color = color;
            this.brightness = brightness;
        }
        public float hour { get; set; }
        public Color color { get; set; }
        public float brightness { get; set; }

    }
	public class DaynightUpdate
	{
		public DaynightUpdate(float hour, float kelvin, float brightness)
		{
			this.hour = hour;
			this.kelvin = kelvin;
			this.brightness = brightness;
		}
		public float hour { get; set; }
		public float kelvin { get; set; }
		public float brightness { get; set; }
	}

}
