using MiniStore.Domain;
using MiniStore.Infrastructure.Persistence;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniStore.DevConsole
{
    public static class Extensions
    {
        public static void AddChild(this Category category, string name)
        {
            category.AddChildCategory(Category.Create(name));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var dla_niej = Category.Create("Dla niej", true);
            dla_niej.AddChildCategory(Category.Create("Bielizna"));

            var kobieca_drogeria = Category.Create("Kobieca drogeria");
            kobieca_drogeria.AddChildCategory(Category.Create("Afrodyzjaki i feromony"));
            kobieca_drogeria.AddChildCategory(Category.Create("Pielęgnacja piersi"));
            kobieca_drogeria.AddChildCategory(Category.Create("Pielęgnacja piersi"));
            kobieca_drogeria.AddChildCategory(Category.Create("Lubrykanty"));
            dla_niej.AddChildCategory(kobieca_drogeria);

            var erotyczne_dodatki = Category.Create("Erotyczne dodatki");
            erotyczne_dodatki.AddChildCategory(Category.Create("Biżuteria"));
            erotyczne_dodatki.AddChildCategory(Category.Create("Maski"));
            erotyczne_dodatki.AddChildCategory(Category.Create("Naklejki na sutki"));
            erotyczne_dodatki.AddChildCategory(Category.Create("Ozdoby do ciała"));
            dla_niej.AddChildCategory(erotyczne_dodatki);

            var kulki_gejszy = Category.Create("Kulki gejszy");
            kulki_gejszy.AddChildCategory(Category.Create("Klasyczne"));
            kulki_gejszy.AddChildCategory(Category.Create("Wibrujące"));
            dla_niej.AddChildCategory(kulki_gejszy);

            dla_niej.AddChildCategory(Category.Create("Dildo"));

            var dla_niego = Category.Create("Dla niego", true);
            dla_niego.AddChildCategory(Category.Create("Masażery prostaty"));
            var pierscienie_erekcyjne = Category.Create("Pierścienie erekcyjne");
            pierscienie_erekcyjne.AddChildCategory(Category.Create("Wibrujące"));
            pierscienie_erekcyjne.AddChildCategory(Category.Create("Bez wibracji"));
            dla_niego.AddChildCategory(pierscienie_erekcyjne);
            dla_niego.AddChildCategory(Category.Create("Masturbatory"));
            dla_niego.AddChildCategory(Category.Create("Erekcja i libido"));
            dla_niego.AddChildCategory(Category.Create("Powiększanie penisa"));

            var dla_par = Category.Create("Dla par", true);
            var gra_wstepna = Category.Create("Gra wstępna");
            gra_wstepna.AddChild("Masaż erotyczny");
            gra_wstepna.AddChild("Kąpiel erotyczna");
            gra_wstepna.AddChild("Dodatki do gry");
            dla_par.AddChildCategory(gra_wstepna);

            var bdsm = Category.Create("BDSM");
            bdsm.AddChild("Zaciski na sutki");
            bdsm.AddChild("Pejcze, szpicruty i packi do klapsów");
            bdsm.AddChild("Piórka do łaskotania");
            bdsm.AddChild("Kajdanki");
            bdsm.AddChild("Akcesoria do krępowania");
            bdsm.AddChild("Maski i opaski na oczy");
            dla_par.AddChildCategory(bdsm);

            var seks_analny = Category.Create("Seks analny");
            seks_analny.AddChild("Korki, zatyczki i koraliki analne");
            seks_analny.AddChild("Wibratory analne");
            seks_analny.AddChild("Masażery prostaty");
            seks_analny.AddChild("Higiena w seksie analnym");
            seks_analny.AddChild("Lubrykanty do seksu analnego");
            seks_analny.AddChild("Dildo");
            dla_par.AddChildCategory(seks_analny);

            var seks_oralny = Category.Create("Seks oralny");
            seks_oralny.AddChild("Żele i spraye");
            seks_oralny.AddChild("Błyszczyki");
            seks_oralny.AddChild("Lubrykanty smakowe");
            seks_oralny.AddChild("Gadżety do seksu oralnego");
            dla_par.AddChildCategory(seks_oralny);

            var pomysl_na_upominek = Category.Create("Pomysł na upominek");
            pomysl_na_upominek.AddChild("Zestawy prezentowe BDSM");
            pomysl_na_upominek.AddChild("Zestawy prezentowe do gry wstępnej");
            pomysl_na_upominek.AddChild("Upominek dla kobiety");
            pomysl_na_upominek.AddChild("Upominek dla mężczyzny");
            dla_par.AddChildCategory(pomysl_na_upominek);

            var wibratory = Category.Create("Wibratory", true);
            wibratory.AddChild("Klasyczne");
            wibratory.AddChild("Króliczki");
            wibratory.AddChild("Masażery prostaty");
            wibratory.AddChild("Miniwibratory");
            wibratory.AddChild("Wibrujące jajka");
            wibratory.AddChild("Punkt G");
            wibratory.AddChild("Analne");
            wibratory.AddChild("Masażery łechtaczki");
            wibratory.AddChild("Zdalnie sterowane");
            wibratory.AddChild("Na palec");
            wibratory.AddChild("Dla par");
            wibratory.AddChild("Masażery do ciała");
            wibratory.AddChild("Inne");

            var drogeria = Category.Create("Drogeria", true);
            var kosmetyki_erotyczne = Category.Create("Kosmetyki erotyczne");
            kosmetyki_erotyczne.AddChild("Balsamy");
            kosmetyki_erotyczne.AddChild("Farbki do ciała");
            kosmetyki_erotyczne.AddChild("Pudry do ciała");
            kosmetyki_erotyczne.AddChild("Higiena zabawek erotycznych");
            kosmetyki_erotyczne.AddChild("Prezerwatywy");
            drogeria.AddChildCategory(kosmetyki_erotyczne);

            var lubrykanty = Category.Create("Lubrykanty");
            lubrykanty.AddChild("Wodne");
            lubrykanty.AddChild("Silikonowe");
            lubrykanty.AddChild("Hybrydowe");
            lubrykanty.AddChild("Organiczne");
            drogeria.AddChildCategory(lubrykanty);

            var root = new List<Category>
            {
                dla_niej,
                dla_niego,
                dla_par,
                wibratory,
                drogeria
            };


            var leafs = root.SelectMany(x => x.GetAllChildCategories()).Where(x => x.Categories.Count == 0);
            var prods = new MongoClient("mongodb://localhost:27017")
                .GetDatabase("MiniStore")
                .GetCollection<Product>("Products")
                .Find(_ => true)
                .ToList();

            for (var i = 0; i < leafs.Count(); i++)
            {
                foreach (var p in prods.Skip(15 * i).Take(15))
                {
                    leafs.ElementAt(i).AddProduct(p);
                }
            }

            //File.WriteAllText("MOCK_CATEGORIES.json", JsonConvert.SerializeObject(root));

            var categoryRepository = new CategoryRepository(new MongoClient("mongodb://localhost:27017").GetDatabase("MiniStore"));

            foreach (var r in root)
            {
                categoryRepository.Add(r);
            }

            if (args.Length >= 2)
            {
                if (args[0] == "import-products")
                {
                    var productRepository = new ProductRepository(new MongoClient("mongodb://localhost:27017").GetDatabase("MiniStore"));

                    var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(File.ReadAllText(args[1]));

                    foreach (var product in products)
                    {
                        productRepository.Add(product);
                    }
                }
            }

            
        }
    }
}
