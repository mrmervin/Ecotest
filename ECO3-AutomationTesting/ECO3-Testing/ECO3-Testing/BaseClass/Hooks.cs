using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using ECO3_Testing.ComponentHelpers;
using log4net;
using System.IO;
using TechTalk.SpecFlow;

namespace ECO3_Testing.BaseClass
{
    [Binding]
    public class Hooks
    {
        #region Report instances
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private string stepType;
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(Hooks));
        #endregion

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string reportPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                 @"ECO3-Testing\ECO3-Testing\Reporting.html");
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Configuration().ChartLocation = AventStack.ExtentReports.Reporter.Configuration.ChartLocation.Bottom;
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeatures()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void Init()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void FailedScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                Logger.Info("This Scenario failed >>>>>>>" + ScenarioContext.Current.ScenarioInfo.Title);
                string shotPath = GenericHelper.TakeScreensots(ScenarioContext.Current);
                scenario.Fail(ScenarioContext.Current.ScenarioInfo.Title).AddScreenCaptureFromPath(shotPath);
            }
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            CheckTestStatus(stepType);
        }

        private void CheckTestStatus(string stepDefType)
        {
            if (ScenarioContext.Current.TestError == null)
            {
                switch (stepDefType)
                {
                    case "Given":
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "When":
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "Then":
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                }
            }
            else if (ScenarioContext.Current.TestError != null || ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "BindingError")
            {
                switch (stepDefType)
                {
                    case "Given":
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text)
                            .Fail(ScenarioContext.Current.TestError.Message);
                        break;
                    case "When":
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text)
                            .Fail(ScenarioContext.Current.TestError.Message);
                        break;
                    case "Then":
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text)
                            .Fail(ScenarioContext.Current.TestError.Message);
                        break;
                }
            }
            else if(ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending"
                    || ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "UndefinedStep")
            {
                switch (stepDefType)
                {
                    case "Given":
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text)
                            .Skip(ScenarioContext.Current.TestError.Message);
                        break;
                    case "When":
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text)
                            .Skip(ScenarioContext.Current.TestError.Message);
                        break;
                    case "Then":
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text)
                            .Skip(ScenarioContext.Current.TestError.Message);
                        break;
                }
            }
        }

    }
}

