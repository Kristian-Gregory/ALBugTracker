# ALBugTracker

Write how to build, run and test

# Difficulties encountered
Needed to switch to a more powerful development machine as the containers, particularly the sequel server container, were quite heavyweight and I initially set out on an inadequate machine for a dev task involving locally hosting this many containers.
Documentation was more difficult to come by for developing the app after I separated the API and the WebApp into separate containers
EF6.0 allowed default lazy loading, whereas EFCore requires child objects to be explicitly requested for loading. This took some time to identify and debug.
