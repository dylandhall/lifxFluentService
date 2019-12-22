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
            //figure out what the time will be after the change duration
            hourOfDay += (float)((double)changeDuration / 60d / 60d);
            if (hourOfDay > 24) hourOfDay -= 24;

            var next = TimeConfig.Daynights.FirstOrDefault(a => a.hour > hourOfDay) ?? TimeConfig.Daynights.Last();
            var previous = TimeConfig.Daynights.LastOrDefault(a => a.hour <= hourOfDay) ?? TimeConfig.Daynights.First();

            var percentThroughPeriod = (hourOfDay - previous.hour) / (next.hour - previous.hour);
            var colourTemp = new LifxColor.HSBK(kelvin: (int)percentThroughPeriod.Between(previous.kelvin, next.kelvin));
            var brightness = percentThroughPeriod.Between(previous.brightness, next.brightness);

            var nextFc = TimeConfig.Fullcolour.FirstOrDefault(a => a.hour > hourOfDay) ?? TimeConfig.Fullcolour.Last();
            var previousFc = TimeConfig.Fullcolour.LastOrDefault(a => a.hour <= hourOfDay) ?? TimeConfig.Fullcolour.FirstOrDefault();

            percentThroughPeriod = (hourOfDay - previousFc.hour) / (nextFc.hour - previousFc.hour);

            var newColour = new Rgb
            {
                R = (int)percentThroughPeriod.Between(previousFc.color.R, nextFc.color.R),
                G = (int)percentThroughPeriod.Between(previousFc.color.G, nextFc.color.G),
                B = (int)percentThroughPeriod.Between(previousFc.color.B, nextFc.color.B)
            };

            //lifx uses Hsv, windows System.Drawing.Color is HSB, I'm using RGB above to blend
            var newHsv = newColour.To<Hsv>();

            var brightnessFc = percentThroughPeriod.Between(previousFc.brightness, nextFc.brightness);

            var colourHue = new LifxColor.HSB(
                hue: (float)newHsv.H,
                saturation: (float)newHsv.S
                //brightness controlled in light updates
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
                .WithBrightness(brightnessFc)
                .WithColor(colourHue);

                foreach (var label in fullcolourLights)
                    fullcolourState.ByLabel(label);
            }
            
            await update.ApplyAsync(fast).ConfigureAwait(false);
        }
    }
}
