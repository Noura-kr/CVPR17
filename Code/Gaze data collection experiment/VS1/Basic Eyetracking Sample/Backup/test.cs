using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEyetrackingSample
{
    class test
    {
        Category v1 = new Category("Vireo1", @"..\..\wikiPics\Vireo\V1-Black-capped_Vireo.jpg", @"..\..\dataSet\V1\");
        Category v2 = new Category("Vireo2", @"..\..\wikiPics\Vireo\V2-blue-headed_Vireo.jpg", @"..\..\dataSet\V2\");
        //Category v3 = new Category("Vireo3", @"..\..\wikiPics\Vireo\V3-philadelphicus_Vireo.jpg", @"..\..\dataSet\V3\");
        List<Category> categories = new List<Category>();
        public experiment create() {
            experiment e = new experiment();
            this.categories.Add(v1);
            this.categories.Add(v2);
            //this.categories.Add(v3);
            int iterations = 0;
            foreach (Category c in categories){
                iterations += c.ImagesPathCount.Count;
            }
            Random rand = new Random(); 
            while (iterations > 0) {
                //select a category to collect data for 
                bool ok = false;
                int catIndex = -1;
                Category selectedCat=null;
                while (!ok) {
                    catIndex = rand.Next(categories.Count);
                    selectedCat = categories[catIndex];
                    if (selectedCat.Selected != selectedCat.ImagesPathCount.Count)
                    {
                        selectedCat.Selected++;
                        ok = true;
                    }
                }
                
                Console.Out.WriteLine("Selected cat for collection: " + selectedCat.CategoryName);
                //select a category for comparing (training)
                ok = false;
                Category selectedCompCat=null;
                while (!ok) {
                    catIndex = rand.Next(categories.Count);
                    selectedCompCat = categories[catIndex];
                    if (selectedCat != selectedCompCat) {
                        ok = true;
                    }
                }
                Console.Out.WriteLine("Selected cat for comparing: " + selectedCompCat.CategoryName);
                List<string> temp = new List<string>();
                temp.Add(selectedCat.Image);
                temp.Add(selectedCompCat.Image);
                e.Compare.Add(temp);
                //select image from collection category
                List<string> Keys = Enumerable.ToList(selectedCat.ImagesPathCount.Keys);
                int imgKey= -1;
                ok = false;
                while (!ok)
                {
                    imgKey = rand.Next(Keys.Count);
                    if (selectedCat.ImagesPathCount[Keys[imgKey]] == 0)
                    {
                        ok = true;
                        selectedCat.ImagesPathCount[Keys[imgKey]] = 1;
                    }
                }
                e.Record.Add(Keys[imgKey]);
                Console.Out.WriteLine("Selected image for collection: " + Keys[imgKey]);
                iterations--;
                Console.Out.WriteLine("Iterations: " + iterations.ToString());
            }
            return e;
        }

    }
}
