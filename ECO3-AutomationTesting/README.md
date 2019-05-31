#Introduction 
This automation framework is to develop regression tests for ECO3 project. 
Tests will be designed such that they will be run against incremental builds as soon as they are available in QA Environments. 

#Getting Started
1.  Tests are created in a framework well structured to achieve readability, usability and maintainability. 
2.  The Base Class contains the start of all tests where browsers (i.e. chrome, IE or firefox are instantiated) and end of tests where TestContext gives test outcome as passed or failed.
3.  ComponentHelper contains utilities for waiting functionalities for page to load correctly before interacting with it.
    This also contains test functionalities like Navigation, Logging and WindowLoginScripts process runners
4.  Configuration deals with provision of browsers for test and reading App.config for values used in test like URL,browser and user details.
5.  CustomException this is simply an exception handling where customised exceptions are thrown where implemented.
6.  DataSource this is for reading XML AND CSV files
7.  ExcelReader dedicated for reading excel files
8.  FeatureFiles is where BDD test is utilized by aligning with business logics.
9.  FirefoxFiles deals with all implementations of utilizing firefox driver within the framework
10. Intefaces this is used for passing data retrieved from App.config file. It's implementation is in appConfigReader class.
11. Pages is the uitilisation of Page Object Model where page elements are stored and instantiated.
11. Settings this is where App.config values are first retrieved, with ObjectRepository class used to access IWebDriver and IConfig
12. Step Definitions this is the utilizatioin of feature file where features are turned to tests.
13. Tests location for tests withouts BDD attributes.
14. WindowLoginScripts this contains exe files that helps to log on user in window dialog as this is how the internal website is.