using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tobii.EyeTracking.IO;

namespace BasicEyetrackingSample
{
    class dataItem
    {
        string state;

        string decision;
        string leftClass;
        string rightClass;
        Point imgLocation;
        string imgPath;

        string rightImgPath;
        string leftImgPath;
        Point rightLocation;
        Point leftLocation;

        public dataItem()
        {
            this.state = "calibration_2";
        }

        public dataItem( string lClass, string rClass, Point imgLoc,string imgPath)
        {
            this.state = "record";
            this.leftClass = lClass;
            this.rightClass = rClass;
            this.imgLocation = imgLoc;
            this.imgPath = imgPath;
        }
       
        public dataItem(string rPath, string lPath, Point rLoc, Point lLoc)
        {
            this.state = "training";
            this.rightImgPath = rPath;
            this.leftImgPath = lPath;
            this.rightLocation = rLoc;
            this.leftLocation = lLoc;
        }

        public string State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public string Decision
        {
            get
            {
                return decision;
            }

            set
            {
                decision = value;
            }
        }

        public string LeftClass
        {
            get
            {
                return leftClass;
            }

            set
            {
                leftClass = value;
            }
        }

        public string RightClass
        {
            get
            {
                return rightClass;
            }

            set
            {
                rightClass = value;
            }
        }

        public Point ImgLocation
        {
            get
            {
                return imgLocation;
            }

            set
            {
                imgLocation = value;
            }
        }

        public string ImgPath
        {
            get
            {
                return imgPath;
            }

            set
            {
                imgPath = value;
            }
        }

        public string RightImgPath
        {
            get
            {
                return rightImgPath;
            }

            set
            {
                rightImgPath = value;
            }
        }

        public string LeftImgPath
        {
            get
            {
                return leftImgPath;
            }

            set
            {
                leftImgPath = value;
            }
        }

        public Point RightLocation
        {
            get
            {
                return rightLocation;
            }

            set
            {
                rightLocation = value;
            }
        }

        public Point LeftLocation
        {
            get
            {
                return leftLocation;
            }

            set
            {
                leftLocation = value;
            }
        }

        public string getLog()
        {
            string result="";
            if (this.state == "training")
            {
                
                result += this.leftImgPath + ",";
                result += this.rightImgPath + ",";
                result += this.LeftLocation.X.ToString() + ",";
                result += this.LeftLocation.Y.ToString() + ",";
                result += this.RightLocation.X.ToString() + ",";
                result += this.RightLocation.Y.ToString() ;
            }
            else if (this.state == "record")
            {
                result += this.LeftClass + ",";
                result += this.RightClass + ",";
                result += this.imgLocation + ",";
                result += this.imgPath + ",";
                result += this.decision;
            }
            return result;
        }
    }
}
