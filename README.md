# lifxFluentService
Fluent wrapper for lifx state updates, plus small script I use to make my lights adjust according to a schedule via AWS.

I would like to credit https://github.com/mensly/LifxHttpNet project for the lifxColor class I used. The fork at https://github.com/caleblanchard/LifxHttpNet is probably easiest to use if you want an API and there's a nuget.

I would have used that library but I needed to  be able to change colour without affecting the power state, plus run a number of updates with one call, neither of which were possible with the original library. Plus I really like Fluent API libraries to work with.

The waypoints for lights are contained in the `TimeConfig.cs`, change according to preference.

The environment variables you'll need to set on AWS are:

-  LifxApiToken: your token
- DaynightLights: comma seperated list of lights you want to set via kelvin, by label.
- FullColourLights: comma seperated list of lights you want to set by colour, by label.
- TimezoneId: the full text timezone ID, in AWS these are linux so in the format "Australia/Sydney" - if you run on a windows machine this will need to look more like "AUS Eastern Standard Time".
- ChangeDuration: optional, defaults to 10 minutes. integer of number of seconds.

I can fork and add the fluent interface if anyone is interested, or add some functions and publish it as a standalone nuget.

Currently my lights are happily updating from the cloud however so I'm likely to leave this as is.

To create a package to upload, use `dotnet lambda package` command line argument. You'll need to install the AWS command line tools, see here: https://docs.aws.amazon.com/lambda/latest/dg/lambda-dotnet-coreclr-deployment-package.html

I haven't worked with AWS before so I didn't set anything up that was particularly complex, I just have the CloudEvents trigging a run every 15 minutes.
