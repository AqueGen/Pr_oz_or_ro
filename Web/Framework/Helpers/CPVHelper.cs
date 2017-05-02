using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace Kapitalist.Web.Framework.Helpers
{
    public class Node
    {
        public string Id { get; set; }
        public string Term { get; set; }
        public string ParentId { get; set; }
    }

    public class CPVHelper
    {
        private static List<Node> _cpvNodes = new List<Node>();
        private static SortedDictionary<string, string> _cpvSortedDictionary = new SortedDictionary<string, string>();

        static public List<Node> CpvNodes
        {
            get
            {
                if (_cpvNodes.Count > 0)
                    return _cpvNodes;
                _cpvNodes = Generate();
                return _cpvNodes;
            }
        }

        private static List<Node> Generate()
        {
            string response = String.Empty;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response =
                        client.GetStringAsync("http://standards.openprocurement.org/classifiers/cpv/uk.json").Result;
                }
                catch
                {
                    var path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content/cpv/cpv_ukr.json");
                    if (File.Exists(path))
                    {
                        response = File.ReadAllText(path);
                    }
                }

                if (response.Length == 0) return null;
                SortedDictionary<string, string> cpvSortedDictionary = JsonConvert.DeserializeObject<SortedDictionary<string, string>>(response);
                _cpvSortedDictionary = cpvSortedDictionary;

                List<Node> nodes = new List<Node>();
                StringBuilder codeFirstPartParent = new StringBuilder();
                StringBuilder codeSecondPartParent = new StringBuilder();
                StringBuilder codeParent = new StringBuilder();
                var chArray = new char[] {'-'};
                foreach (var pair in cpvSortedDictionary)
                {
                    //Разделяем части кода вида "00000000-0"
                    var codeArray = pair.Key.Split(chArray, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = codeArray[0].Length - 1; i >= 0; i--)
                    {
                        //Если мы дошли в первой части кода до не 0, то определяем код родителя 
                        if (codeArray[0][i] != '0')
                        {
                            codeFirstPartParent.Append(codeArray[0].Substring(0, i));
                            //Исчем каждый раз обнуляя последнюю цифру
                            for (var s = codeFirstPartParent.Length - 1; s >= 1; s--)
                            {
                                if (cpvSortedDictionary.Keys.FirstOrDefault(
                                    c => c.StartsWith(codeFirstPartParent.ToString() + codeSecondPartParent.ToString())) ==
                                    null)
                                {
                                    codeFirstPartParent.Remove(s, 1);
                                    codeSecondPartParent.Append('0');
                                }
                                else
                                {
                                    codeParent.Append(cpvSortedDictionary.Keys.FirstOrDefault(
                                    c => c.StartsWith(codeFirstPartParent.ToString() + codeSecondPartParent.ToString())));
                                    break;
                                }
                            }
                            
                            codeFirstPartParent.Clear();
                            codeSecondPartParent.Clear();

                            //Если есть родитель то заполняем ParentId
                            nodes.Add(codeParent.Length > 0
                                ? new Node
                                {
                                    Id = pair.Key,
                                    Term = pair.Key + " - " + pair.Value,
                                    ParentId = codeParent.ToString()
                                }
                                : new Node
                                {
                                    Id = pair.Key,
                                    Term = pair.Key + " - " + pair.Value
                                });
                            codeParent.Clear();
                            break;
                        }
                        codeSecondPartParent.Append('0');
                    }
                }
                _cpvNodes = nodes;
                return nodes;
            }
        }

        public static string GetCPVNameUkr(string CPVCode)
        {
            if (_cpvSortedDictionary.Count > 0)
            {
                return _cpvSortedDictionary.FirstOrDefault(c => c.Key == CPVCode).Value;
            }
            return null;
        }
    }
}