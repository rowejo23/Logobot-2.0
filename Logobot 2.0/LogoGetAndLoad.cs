using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logobot2_0
{
    public class LogoGetAndLoad
    {
        private string currentUser = "";

        private string searchCompanyStr = "";

        private string searchStr = "";

        private string screenWhereStr = "";

        private string logoDetails = "";

        private string category = "";

        private string companiesInCategory = "";

        private Image imgLogo;

        private List<LogoGetAndLoad> lstLGAL= new List<LogoGetAndLoad>();

        List<string> searchCompanyNames;

        private WorkerProgress wp;

        private WorkerProgressMarquee wpm;

        private ScreenSources screenDataSource;

        private bool screenNoLogo;

        private int searchDBCount;

        private bool moreResults;

        private bool secondSearch;

        public LogoGetAndLoad()
        {
        }

        public LogoGetAndLoad(Image img, String logoDetails)
        {
            this.logoDetails = logoDetails;
            this.imgLogo = img;
        }

        public enum SendingDGV { User, Screen, Search }

        public SendingDGV SendingDGVProperty { get; set; }

        public Image ImgLogo
        {
            get { return this.imgLogo; }
            set { imgLogo = value; }
        }

        public int SearchDBCount
        {
            get { return this.searchDBCount; }
            set { searchDBCount = value; }
        }

        public bool MoreResults
        {
            get { return this.moreResults; }
            set { moreResults = value; }
        }

        public bool SecondSearch
        {
            get { return this.secondSearch; }
            set { secondSearch = value; }
        }
        public string SearchCompanyStr
        {
            get { return this.searchCompanyStr; }
            set { searchCompanyStr = value; }
        }

        public string SearchStr
        {
            get { return this.searchStr; }
            set { searchStr = value; }
        }

        public string CurrentUser
        {
            get { return this.currentUser; }
            set { currentUser = value; }
        }

        public string ScreenWhereStr
        {
            get { return this.screenWhereStr; }
            set { screenWhereStr = value; }
        }

        public string SearchLogoDetailsStr
        {
            get { return this.logoDetails; }
            set { logoDetails = value; }
        }

        public string Category
        {
            get { return this.category; }
            set { category = value; }
        }

        public string CompaniesInCategory
        {
            get { return this.companiesInCategory; }
            set { companiesInCategory = value; }
        }

        public Image SearchLogoImage
        {
            get { return this.imgLogo; }
            set { imgLogo = value; }
        }

        public List<LogoGetAndLoad> SearchListGAL
        {
            get { return this.lstLGAL; }
            set { lstLGAL = value; }
        }

        public List<string> SearchCompanyNames
        {
            get { return this.searchCompanyNames; }
            set { searchCompanyNames = value; }
        }

        public WorkerProgress WP
        {
            get { return this.wp; }
            set { wp = value; }
        }

        public WorkerProgressMarquee WPM
        {
            get { return this.wpm; }
            set { wpm = value; }
        }

        public ScreenSources ScreenDataSource
        {
            get { return this.screenDataSource; }
            set { screenDataSource = value; }
        }

        public bool ScreenNoLogo
        {
            get { return this.screenNoLogo; }
            set { screenNoLogo = value; }
        }
    }
}
