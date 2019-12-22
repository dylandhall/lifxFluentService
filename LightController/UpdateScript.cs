using ColorMine.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController
{
    public static class UpdateScript
    {

        public static async Task RunUpdate(string token, float hourOfDay, int changeDuration, string[] daynightLights, string[] fullcolourLights, bool fast = false)
        {
            var next = TimeConfig.Daynights.FirstOrDefault(a => a.hour > hourOfDay) ?? TimeConfig.Daynights.Last();
            var previous = TimeConfig.Daynights.LastOrDefault(a => a.hour <= hourOfDay) ?? TimeConfig.Daynights.First();

            hourOfDay += (float)((double)changeDuration / 60d / 60d);
            if (hourOfDay > 24) hourOfDay -= 24;

            var fracThroughPeriod = (hourOfDay - previous.hour) / (next.hour - previous.hour);
            var colourTemp = new LifxColor.HSBK(kelvin: (int)fracThroughPeriod.Between(previous.kelvin, next.kelvin));
            var brightness = fracThroughPeriod.Between(previous.brightness, next.brightness);

            var nextFc = TimeConfig.Fullcolour.FirstOrDefault(a => a.hour > hourOfDay) ?? TimeConfig.Fullcolour.Last();
            var previousFc = TimeConfig.Fullcolour.LastOrDefault(a => a.hour <= hourOfDay) ?? TimeConfig.Fullcolour.FirstOrDefault();

            fracThroughPeriod = (hourOfDay - previousFc.hour) / (nextFc.hour - previousFc.hour);

            var newColour = new Rgb
            {
                R = (int)fracThroughPeriod.Between(previousFc.color.R, nextFc.color.R),
                G = (int)fracThroughPeriod.Between(previousFc.color.G, nextFc.color.G),
                B = (int)fracThroughPeriod.Between(previousFc.color.B, nextFc.color.B)
            };

            var newHsv = newColour.To<Hsv>();

            var brightnessFc = fracThroughPeriod.Between(previousFc.brightness, nextFc.brightness);

            var colourHue = new LifxColor.HSB(
                hue: (float)newHsv.H,//newColour.GetHue(),//fracThroughPeriod.Between(previousFc.color.GetHue(), nextFc.color.GetHue()), 
                saturation: (float)newHsv.S//,//fracThroughPeriod.Between(previousFc.color.GetSaturation(), nextFc.color.GetSaturation()),
                //brightness: Math.Min(brightnessFc, newColour.GetBrightness())//fracThroughPeriod.Between(previousFc.color.GetBrightness(), nextFc.color.GetBrightness()))
            );


            var update = new LifxFluent(token);
             
            update.AddDefaults().WithDuration(changeDuration);

            if (daynightLights != null && daynightLights.Any())
            {
                var daynightState = update.AddLightState()
                    .WithBrightness(brightness)
                    .WithColor(colourTemp);

                foreach (var label in daynightLights)
                    daynightState.ByLabel(label);
            }

            if (fullcolourLights != null && fullcolourLights.Any())
            {
                var fullcolourState = update.AddLightState()
                .WithBrightness(brightnessFc)//fracThroughPeriod.inbetween(previousFc.color.GetBrightness(), nextFc.color.GetBrightness()) * 
                .WithColor(colourHue);

                foreach (var label in fullcolourLights)
                    fullcolourState.ByLabel(label);
            }
            
            await update.ApplyAsync(fast).ConfigureAwait(false);
        }
    }
}
