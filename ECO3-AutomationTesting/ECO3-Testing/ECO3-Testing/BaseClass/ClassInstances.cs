using ECO3_Testing.Pages;

namespace ECO3_Testing.BaseClass
{
    public class ClassInstances
    {
        static LoginPage logingPage;
        static InternalHomePage internalHomePage;
        static MeasureProcessingErrorFilePage measureProcessingErrorFilePage;
        static UploadSummaryPage uploadSummaryPage;
        static StartMeasureProcessingPage startMeasureProcessingPage;

        public LoginPage LoginPageInstance
        {
            get { return LogingPage; }
            set { LogingPage = value; }
        }

        public InternalHomePage InternalHomePageInstance
        {
            get { return InternalHomePage; }
            set { InternalHomePage = value; }
        }

        public  MeasureProcessingErrorFilePage MeasureProcessingErrorFilePageInstance
        {
            get { return MeasureProcessingErrorFilePage; }
            set { MeasureProcessingErrorFilePage = value; }

        }

        public UploadSummaryPage GetUploadSummaryPage
        {
            get { return UploadSummaryPage; }
            set { UploadSummaryPage = value; }
        }

        public StartMeasureProcessingPage StartMeasureProcessingPageInstance
        {
            get { return StartMeasureProcessingPage; }
            set { StartMeasureProcessingPage = value; }
        }

        private MeasureProcessingErrorFilePage MeasureProcessingErrorFilePage { get => measureProcessingErrorFilePage; set => measureProcessingErrorFilePage = value; }
        private LoginPage LogingPage { get => logingPage; set => logingPage = value; }
        private InternalHomePage InternalHomePage { get => internalHomePage; set => internalHomePage = value; }
        private UploadSummaryPage UploadSummaryPage { get => uploadSummaryPage; set => uploadSummaryPage = value; }
        private StartMeasureProcessingPage StartMeasureProcessingPage { get => startMeasureProcessingPage; set => startMeasureProcessingPage = value; }
    }
}
