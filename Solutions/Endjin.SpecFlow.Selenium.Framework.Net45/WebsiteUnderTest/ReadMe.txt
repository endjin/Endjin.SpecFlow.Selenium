
************

Instructions

************

1. Create Page Model and Page View classes for the pages you want to test, inheriting from PageModel and PageView.
e.g. 
public class RegisterPageModel : PageModel<RegisterPageView>
public class RegisterPageView : PageView

SpecFlow step definitions will use these classes to interact with the pages.
e.g. 
var page = Navigator.Browser.CurrentPage.As<RegisterPageModel>();
page.EnterPassword(password);

2. Add details of the pages you want to test in the WebsiteNavigationMap class. 
e.g.
this.AddPage<RegisterPageModel>(Page.Register, "Account/Register");

3. Edit the web_test.config.json file to configure the application under test, test browser, and other test settings.

<!-- The name of the web application (in the current solution) that is under test -->
"web_app_name": "web_app_project_name"

<!-- Websites -->
<!-- Local -->
"website_url": "http://localhost:2014/"
<!-- Local HTTPS -->
"website_url": "https://localhost:44300/"
<!-- Test website -->
"website_url": "http://myteswebsite.com/"

<!-- When set to true, Starts/Stops IIS Express automatically when running a feature;
when set to false, start website on local machine 'manually' 
without attaching the debugger before running the tests -->
"auto_start_iis": true,

<!-- When true, shows the IIS Express process in console window -->
"hide_iis": false,

<!-- Pauses the Browser for the specified amount of time (in seconds) before closing; set to 0 for no delay.
Useful when trying to view the browser test just before it closes -->
"delay_close": 0,

<!-- Possible Values (PhantomJs by default):
Default, Chrome, Firefox, InternetExplorer, PhantomJs, Remote, Safari. Set to Remote if you want to run tests through Sauce Labs -->
"web_driver_type": "Default",

<!-- WebDrivers config -->
"accept_untrusted_certificates": true,
"implicitly_wait": 30,
"page_load_timeout": 30,
"script_timeout": 3600,

<!-- Remote Driver Configuration
For a list of available remote browser, version and platforms:
https://saucelabs.com/platforms. Remote driver configuration is only used if the web_driver_type is set to Remote -->
"remote_browser": "chrome",
"remote_browser_version": "34",
"remote_url": "http://ondemand.saucelabs.com:80/wd/hub",
"remote_key": "c775e9de-7aed-402a-8e24-0ad91f83f6a3",
"remote_platform": "Windows 7",
"remote_username": "foobar"