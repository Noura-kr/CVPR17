using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tobii.EyeTracking.IO;

namespace BasicEyetrackingSample
{
    class dataItem
    {
        string decision;
        List<IGazeDataItem> gazeData = new List<IGazeDataItem>();
        List<DateTime> timestamp = new List<DateTime>();
        string leftClass;
        string rightClass;
        Guid participantID;
        string imgPath;
        public dataItem(Guid s)
        {
            this.participantID = s;
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

        public List<IGazeDataItem> GazeData
        {
            get
            {
                return gazeData;
            }

            set
            {
                gazeData = value;
            }
        }

        public List<DateTime> Timestamp
        {
            get
            {
                return timestamp;
            }

            set
            {
                timestamp = value;
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

        public Guid ParticipantID
        {
            get
            {
                return participantID;
            }

            set
            {
                participantID = value;
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
    }
}
