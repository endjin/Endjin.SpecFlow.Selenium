#Release Notes

##2.0.0.1 

###Breaking changes

Upgraded Nunit to 3.2.1.

Some web_test.config.json property names have been changed for clarity:
- 'auto_start_iis' is now 'auto_start_local_iis'
- 'hide_iis' is now 'hide_local_iis'
- 'remote_browser' is now 'sauce_labs_remote_browser'
- 'remote_browser_version' is now ''
- 'remote_url' is now 'sauce_labs_remote_url'
- 'remote_key' is now 'sauce_labs_remote_key'
- 'remote_platform' is now 'sauce_labs_remote_platform'
- 'remote_username' is now 'sauce_labs_remote_username'
A 'run_using_sauce_labs' property is now expected.

###New features

Added support for directly running tests on a deployed site via TeamCity:
Previously tests could only be run in TeamCity using SauceLabs.

Exposed the EventFiringWebDriver as a read only property:
Step definitions can now interact directly with the Selenium Driver when more control is required.

###Bug fixes

Updated ChromeDriver and PhantomJS to fix issues when running tests with older versions of these packages.

Fixed build actions for files which are inserted into the project under WebsiteUnderTest. 

Fixed missing stepAssemblies section in app.config. 
