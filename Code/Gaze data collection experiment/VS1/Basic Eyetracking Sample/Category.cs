using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEyetrackingSample
{
    class Category
    {
        string categoryName;
        string categoryFullName;
        string image;
        string imagesDirectory;
        Dictionary<string, int> imagesPathCount= new Dictionary<string, int>();
        int selected = 0;       


        public Category(string name, string fullName, string imgPath, string imgzPath)
        {
            this.CategoryName = name;
            this.CategoryFullName = fullName;
            this.image = imgPath;
            this.ImagesDirectory = imgzPath;
            string[] Images = Directory.GetFiles(this.ImagesDirectory,"*.jpg");
            foreach (string i in Images) {
                this.ImagesPathCount.Add(i, 0);
            }

        }

        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }
        public string CategoryFullName
        {
            get
            {
                return categoryFullName;
            }

            set
            {
                categoryFullName = value;
            }
        }

        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        public string ImagesDirectory
        {
            get
            {
                return imagesDirectory;
            }

            set
            {
                imagesDirectory = value;
            }
        }

        public Dictionary<string, int> ImagesPathCount
        {
            get
            {
                return imagesPathCount;
            }

            set
            {
                imagesPathCount = value;
            }
        }

        public int Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
            }
        }
        
    }
}
