SimpleCMS
Sample ASP .Net MVC Project
Sean Bennett

In order to compile:

* Install nuget command line and add it to the path (http://nuget.org)
* Install ruby (or iron ruby) and add it to the path (http://rubyinstaller.org)
* From a CMD window run: gem install rake bundler
* Then run: rake (should build)
* Then rune: rake setup:dep (should download all the nuget dependencies)
* Then run: rake test (should run all the tests)
* Then run: rake test:acceptance (should run all features)

Description:
The demos is a ASP.NET MVC 3 application that uses the REST API to show the presenters, sessions and tracks.