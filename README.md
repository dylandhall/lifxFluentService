# lifxFluentService
Fluent wrapper for lifx state updates, plus small script I use to make my lights adjust according to a schedule via AWS.

I would like to credit https://github.com/mensly/LifxHttpNet project for the lifxColor class I used. The fork at https://github.com/caleblanchard/LifxHttpNet is probably easiest to use if you want an API and there's a nuget.

I would have used that library but I needed to  be able to change colour without affecting the power state, plus run a number of updates with one call, neither of which were possible with the original library. Plus I really like Fluent API libraries to work with.

I can fork and add the fluent interface if anyone is interested, or add some functions and publish it as a standalone nuget.

Currently my lights are happily updating from the cloud however so I'm likely to leave this as is.
