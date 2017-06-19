using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEyetrackingSample
{
    class experimentRunner
    {
        
        List<Category> categories = new List<Category>();
        public experiment create(string type) {
            if (type == "V")
                this.addVireoCategories();
            else if (type == "W")
                this.addWoodPeckersCategories();
            else
                return null;
            experiment e = new experiment();            
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
                
                //Console.Out.WriteLine("Selected cat for collection: " + selectedCat.CategoryName);
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
                //Console.Out.WriteLine("Selected cat for comparing: " + selectedCompCat.CategoryName);
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
        public void addVireoCategories() {
            Category v1 = new Category("Vireo1", "Black capped Vireo", @"..\..\classesPics\V\1.jpg", @"..\..\dataSet\V1\");
            Category v2 = new Category("Vireo2", "Blue headed Vireo", @"..\..\classesPics\V\2.jpg", @"..\..\dataSet\V2\");
            Category v3 = new Category("Vireo3", "Philadelphia Vireo",  @"..\..\classesPics\V\3.jpg", @"..\..\dataSet\V3\");
            Category v4 = new Category("Vireo4", "Red eyed Vireo",  @"..\..\classesPics\V\4.jpg", @"..\..\dataSet\V4\");
            Category v5 = new Category("Vireo5", "Warbling Vireo", @"..\..\classesPics\V\5.jpg", @"..\..\dataSet\V5\");
            Category v6 = new Category("Vireo6", "White eyed Vireo", @"..\..\classesPics\V\6.jpg", @"..\..\dataSet\V6\");
            Category v7 = new Category("Vireo7", "Yellow throated Vireo", @"..\..\classesPics\V\7.jpg", @"..\..\dataSet\V7\");
            this.categories.Add(v1);
            this.categories.Add(v2);
            this.categories.Add(v3);
            this.categories.Add(v4);
            this.categories.Add(v5);
            this.categories.Add(v6);
            this.categories.Add(v7);
        }
        public void addWoodPeckersCategories() {
            Category w1 = new Category("Woodpecker1", "Northern Flicker", @"..\..\classesPics\W\1.jpg", @"..\..\dataSet\W1\");
            Category w2 = new Category("Woodpecker2", "American Three toed Woodpecker", @"..\..\classesPics\W\2.jpg", @"..\..\dataSet\W2\");
            Category w3 = new Category("Woodpecker3", "Pileated_Woodpecker", @"..\..\classesPics\W\3.jpg", @"..\..\dataSet\W3\");
            Category w4 = new Category("Woodpecker4", "Red bellied Woodpecker", @"..\..\classesPics\W\4.jpg", @"..\..\dataSet\W4\");
            Category w5 = new Category("Woodpecker5", "Red cockaded Woodpecker", @"..\..\classesPics\W\5.jpg", @"..\..\dataSet\W5\");
            Category w6 = new Category("Woodpecker6", "Red headed Woodpecker", @"..\..\classesPics\W\6.jpg", @"..\..\dataSet\W6\");
            Category w7 = new Category("Woodpecker7", "Downy Woodpecker", @"..\..\classesPics\W\7.jpg", @"..\..\dataSet\W7\");
            this.categories.Add(w1);
            this.categories.Add(w2);
            this.categories.Add(w3);
            this.categories.Add(w4);
            this.categories.Add(w5);
            this.categories.Add(w6);
            this.categories.Add(w7);
        }
    }
}
