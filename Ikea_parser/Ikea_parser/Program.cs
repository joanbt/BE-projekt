using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;
using System.IO;
namespace Ikea_parser
{
    class Program
    {
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        private static string DecodeFromUtf8(string utf8_String)
        {
            byte[] bytes = Encoding.Default.GetBytes(utf8_String);
            utf8_String = Encoding.UTF8.GetString(bytes);
            return utf8_String;
        }
        public static void Parse(string PATHTOFILE, string FILENAME, string CATEGORYMETANAME, string SUBCATEGORYMETANAME, bool first=false)
        {
            List<float> prices = new List<float>();
            List<string> names = new List<string>();
            List<string> imageLink = new List<string>();
            List<string> sizes = new List<string>();
            var doc = new HtmlDocument();
            string fulldescription = "";
            string category = "";
            string subcategory = "";
            string metatitle = "";
            string keywords = "";
            Random rnd = new Random();
            doc.Load(PATHTOFILE + FILENAME);
            
            var mdnode = doc.DocumentNode.SelectSingleNode("//meta[@name='description']");

            if (mdnode != null)
            {
                var desc = mdnode.Attributes["content"];
                fulldescription = DecodeFromUtf8(desc.Value);
            }

            var mtnode = doc.DocumentNode.SelectSingleNode("//meta[@name='title']");

            if (mtnode != null)
            {
                var desc = mtnode.Attributes["content"];
                metatitle = DecodeFromUtf8(desc.Value);
            }

            var mknode = doc.DocumentNode.SelectSingleNode("//meta[@name='keywords']");

            if (mtnode != null)
            {
                var desc = mknode.Attributes["content"];
                keywords = DecodeFromUtf8(desc.Value);
            }

            var mcnode = doc.DocumentNode.SelectSingleNode("//meta[@name='"+CATEGORYMETANAME+"']");

            if (mcnode != null)
            {
                var cat = mcnode.Attributes["content"];
                category = DecodeFromUtf8(cat.Value);
            }

            var mcsubnode = doc.DocumentNode.SelectSingleNode("//meta[@name='"+SUBCATEGORYMETANAME+"']");

            if (mcsubnode != null)
            {
                var subcat = mcsubnode.Attributes["content"];
                subcategory = DecodeFromUtf8(subcat.Value);
            }

            var htmlNodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'price regularPrice')]");
            foreach (var node in htmlNodes)
            {
                var price = node.SelectSingleNode("//div[contains(@class,'price regularPrice')]");
                string curr = node.InnerText.ToString().Replace("&nbsp;", "").Replace("Cena za sztuke", "").Replace("PLN", "").Replace("	", "").Replace("\n", "").Replace("/2szt.", "");
                prices.Add(float.Parse(curr));
            }

            var htmlNodes1 = doc.DocumentNode.SelectNodes("//div[contains(@class,'productTitle')]");
            foreach (var node in htmlNodes1)
            {
                names.Add(node.InnerText.ToString());
            }

            var htmlImages = doc.DocumentNode.SelectNodes("//div[@class='image']");
            foreach (var node in htmlImages)
            {
                string val = node.InnerHtml.ToString();
                string data = getBetween(val, "./", "\"");
                imageLink.Add(data);
            }

            var htmlSizes = doc.DocumentNode.SelectNodes("//div[@class='moreInfo']");
            foreach (var node in htmlSizes)
            {
                bool added = false;
                foreach (var node2 in node.SelectNodes(".//span[@class='size']"))
                {
                    string val = DecodeFromUtf8(node2.InnerHtml.ToString().Replace("\n", "").Replace("\t", "").Replace(":","=").Replace(",","|").Replace(" ",""));
                    if (val!="")
                    {
                        sizes.Add(val);
                        added = true;
                        break;
                    }
                }
                if (!added) sizes.Add("");
            }

            System.IO.Directory.CreateDirectory(PATHTOFILE + "IMAGES");
            if (first)
            {
                using (StreamWriter sw = File.AppendText(PATHTOFILE + "parse.csv"))
                {
                    sw.WriteLine("sku|store_view_code|attribute_set_code|product_type|categories|product_websites|name|description|short_description|weight|product_online|tax_class_name|visibility|price|special_price|special_price_from_date|special_price_to_date|url_key|meta_title|meta_keywords|meta_description|base_image|base_image_label|small_image|small_image_label|thumbnail_image|thumbnail_image_label|created_at|updated_at|new_from_date|new_to_date|display_product_options_in|map_price|msrp_price|map_enabled|gift_message_available|custom_design|custom_design_from|custom_design_to|custom_layout_update|page_layout|product_options_container|msrp_display_actual_price_type|country_of_manufacture|additional_attributes|qty|out_of_stock_qty|use_config_min_qty|is_qty_decimal|allow_backorders|use_config_backorders|min_cart_qty|use_config_min_sale_qty|max_cart_qty|use_config_max_sale_qty|is_in_stock|notify_on_stock_below|use_config_notify_stock_qty|manage_stock|use_config_manage_stock|use_config_qty_increments|qty_increments|use_config_enable_qty_inc|enable_qty_increments|is_decimal_divided|website_id|deferred_stock_update|use_config_deferred_stock_update|related_skus|crosssell_skus|upsell_skus|additional_images|additional_image_labels|hide_from_product_page|custom_options|bundle_price_type|bundle_sku_type|bundle_price_view|bundle_weight_type|bundle_values|associated_skus");
                }
            }
            for (int i = 0; i < imageLink.Count; i++)
            {
                using (StreamWriter sw = File.AppendText(PATHTOFILE + "parse.csv"))
                {
                    sw.WriteLine(i + DecodeFromUtf8(names[i].ToString()) + "||Default|simple|" +
                        "\"Default Category/" + category +  "|" + "Default Category/" + category + "/" + subcategory + "\"" + "|base|" + DecodeFromUtf8(names[i].ToString()) + "|"  + "\""+fulldescription+ "\"" +
                        "||" + rnd.Next(0, 20) + "|1||\"Catalog, Search\"|" + prices[i].ToString().Replace(",", ".") + "||||" + i + DecodeFromUtf8(names[i].ToString()) + "|\"" + metatitle + "\"|\"" + keywords + "\"|\"" + fulldescription + "\"|" + getBetween(imageLink[i], "/", ".JPG") + ".JPG" + "|" + "|" + getBetween(imageLink[i], "/", ".JPG") + ".JPG" + "||" + getBetween(imageLink[i], "/", ".JPG") + ".JPG" + "||\"2015-10-25 03:33:33\"|\"2015-10-25 03:33:33\"|||\"Block after Info Column\"|||||||||||||" + "\"" + sizes[i] + "\"" + "|" + rnd.Next(0, 100) + "|0|1|0|0|1|1|1|10000|1|1|1|1|1|1|1|1|0|0|0|1|||||||||||||||");
                }
                if (System.IO.File.Exists(PATHTOFILE + imageLink[i]) && !System.IO.File.Exists(PATHTOFILE + "IMAGES//" + getBetween(imageLink[i], "/", ".JPG") + ".JPG"))
                {
                    File.Copy(PATHTOFILE + imageLink[i], PATHTOFILE + "IMAGES//" + getBetween(imageLink[i], "/", ".JPG") + ".JPG");
                }
            }
        }

        static void Main(string[] args)
        {
            string PATH = "../../../../DATA/";//CHANGE IT

            File.Create(PATH + "parse.csv");

            string FILENAME1 = "Sofy - IKEA.html";
            Parse(PATH, FILENAME1, "IRWStats.categoryLocal", "IRWStats.subCategoryContainerLocal", true);
            string FILENAME2 = "Stoliki kawowe - IKEA.html";
            Parse(PATH, FILENAME2, "IRWStats.categoryLocal", "IRWStats.subCategoryLocal");
            string FILENAME3 = "Stoliki RTV - Stoliki pod telewizor - IKEA.html";
            Parse(PATH, FILENAME3, "IRWStats.categoryLocal", "IRWStats.subCategoryLocal");



        }
    }
}
