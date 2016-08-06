using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logobot2_0
{
    class LogoGetAndLoadJSON
    {
        private string currentUser = "";

        private string searchCompanyStr = "";

        private string searchStr = "";

        private string screenWhereStr = "";

        private string logoDetails = "";

        private string category = "";

        private string companiesInCategory = "";

        private Image imgLogo;

        private List<LogoGetAndLoad> lstLGAL = new List<LogoGetAndLoad>();

        List<string> searchCompanyNames;

        private ScreenSources screenDataSource;

        private bool screenNoLogo;

        private int searchDBCount;

        private bool moreResults;

        private bool secondSearch;

        private byte[] imageArray;

        private string companyDetails;

        private string country;

        private string primarySector;

        private string primarySectorCount;

        private string primaryIndustry;

        private string primaryIndustryCount;

        public LogoGetAndLoadJSON()
        {
        }

        public LogoGetAndLoadJSON(Image img, String logoDetails)
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

        public byte[] ImageArray
        {
            get { return this.imageArray; }
            set { imageArray = value; }
        }

        public string CompanyDetails
        {
            get { return this.companyDetails; }
            set { companyDetails = value; }
        }

        public string Country
        {
            get { return this.country; }
            set { country = value; }
        }

        public string PrimarySector
        {
            get { return this.primarySector; }
            set { primarySector = value; }
        }

        public string PrimarySectorCount
        {
            get { return this.primarySectorCount; }
            set { primarySectorCount = value; }
        }

        public string PrimaryIndustry
        {
            get { return this.primaryIndustry; }
            set { primaryIndustry = value; }
        }

        public string PrimaryIndustryCount
        {
            get { return this.primaryIndustryCount; }
            set { primaryIndustryCount = value; }
        }
    }
}
