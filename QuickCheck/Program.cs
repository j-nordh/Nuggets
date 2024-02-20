using CLAP;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using UtilClasses;
using UtilClasses.Extensions.Bytes;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.WebClient.Extensions;

namespace QuickCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            var p = new Parser<Program>();
            p.Run(args, prog);
        }

        [Verb(Aliases = "FTP")]
        void TestFtpListing()
        {
            GetLines();
        }

        private (string Address, string Username, string Password) _api = (
            "ftp://ftp.nordpoolspot.com/Elspot/Elspot_file/", "goteborgenergi", "jslo873");
        string[] GetLines()
        {
            FtpWebRequest getRequest(Uri uri)
            {
                var ret = WebRequest.Create(uri) as FtpWebRequest;
                ret.Credentials = new NetworkCredential(_api.Username, _api.Password);
                return ret;
            }

            DateTime getDate(string line) => line
                .Split(';')
                .Skip(5)
                .First()
                .Split('.')
                .Reverse()
                .Join("-")
                .AsDateTime();


            bool isOfficial(string line) => line.Split(';').Skip(1).First().EqualsOic("SO");

            if (File.Exists(_api.Address))
                return File.ReadAllLines(_api.Address);
            var uri = new Uri(_api.Address);
            if (!uri.Scheme.EqualsOic(Uri.UriSchemeFtp)) throw new NotImplementedException("");
            bool isFile = _api.Address.ContainsOic("elspot_file");

            var enc = Encoding.ASCII;
            var req = getRequest(uri);
            if (!isFile)
            {

                return new string[] { };
            }

            if (isFile)
            {
                var area = "SE3";
                var currency = "SEK";
                //Get list of files, select the last 2.
                req.Method = WebRequestMethods.Ftp.ListDirectory;
                var files = req.GetResponseString(enc)
                    .SplitLines()
                    .Where(l => l.EndsWithOic(".sdv"))
                    .OrderBy(s => s)
                    .Reverse()
                    .Take(2)
                    .ToList();

                //filter the ones concerning the correct area and currency
                var lines = new List<string>();
                foreach (var f in files.OrderBy(n => n))
                {
                    req = getRequest(new Uri(uri, f));
                    req.Method = WebRequestMethods.Ftp.DownloadFile;
                    req.GetResponseString(enc)
                        .SplitLines()
                        .Where(l => l.ContainsOic($";{area};{currency};"))
                        .Into(lines);
                }

                //prioritize official prices but allow preliminary if official are missing
                var filterDict = new Dictionary<DateTime, string>();
                foreach (var l in lines)
                {
                    var d = getDate(l);
                    filterDict[d] = isOfficial(l)
                        ? l
                        : filterDict.Maybe(d) ?? l;
                }
                lines = filterDict.Values.ToList();

                //Reformat the lines to suit the parser
                var ret = new List<string>();
                foreach (var l in lines)
                {
                    var parts = new List<string>();
                    var fields = l.Split(';');
                    parts.Add(fields[5]);
                    fields.Skip(8).Into(parts);
                    ret.Add(parts.Join(";"));
                }
                return ret.ToArray();
            }

            return null;
        }

        [Verb(Aliases = "TestConnString,TestString,TestConn,TestConnStr,TCS")]
        void TestConnectionString()
        {
            string connStr =
                "Server=tcp:ecodbserver.database.windows.net,1433;" +
                "Initial Catalog=EcoDb;" +
                "Persist Security Info=False;" +
                "User ID=EcoUser;" +
                "Password=\"dark WEATHER wish FLORIDA 8\";" +
                "MultipleActiveResultSets=False;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;" +
                "Connection Timeout=30;";
            try
            {
                using var conn = new SqlConnection(connStr);
                conn.Open();
                var tables = conn.Query<string>(
                    "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' order by TABLE_NAME");
                Console.WriteLine("Connected successfully\r\n. Found the following tables:");
                foreach (var table in tables)
                {
                    Console.WriteLine($"\t{table}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [Verb]
        void Timezone()
        {
            Console.WriteLine($"Local timezone: {TimeZoneInfo.Local.Id}");
            Console.WriteLine("Available:");
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
            {
                Console.WriteLine($"\t{tz.Id}");
            }
            ;
        }
        [Verb]
        public void GetAdInfo(string names)
        {
            var nameArr = names.Split(',').Select(n => n.Trim());
            var ad = new PrincipalContext(ContextType.Domain, "sp.se");


            foreach (var name in nameArr)
            {
                var up = new UserPrincipal(ad) { SamAccountName = name };
                var searcher = new PrincipalSearcher(up);
                var res = (UserPrincipal)searcher.FindOne();
                Console.WriteLine($"{res.Name}: {res.EmailAddress}");
            }
        }
        [Verb]
        public void ParseStuff()
        {
            var strings = new[]
            {
                "{\"response\":\"NoData\",\"result\":\"NoData\"}",
                "{\"response\":{\"status\":\"ack\",\"msg\":\"sending cmd to ESOs\",\"transId\":\"Viva1617176487084_0\"},\"result\":\"NoData}\"}",
                "{\"response\":{\"status\":\"ack\",\"msg\":\"sending cmd to ESOs\",\"transId\":\"Viva1617176487084_0\"},\"result\":{\"status\":\"ack\",\"msg\":\"all ESOs have changed setting\",\"transId\":\"Viva1617176487084_0\"}}"
            };
            foreach (var s in strings)
            {
                Console.WriteLine(GetResult(s));
            }
        }

        private (bool? Response, bool? Result) GetResult(string s)
        {
            var obj = JObject.Parse(s);
            return (GetSubResult(obj["response"]), GetSubResult(obj["result"]));
        }

        private bool? GetSubResult(JToken tok)
        {
            if (null == tok) return null;
            if (!tok.HasValues) return null;
            var r = tok.ToObject<FerroampRes>();
            return r.Status.EqualsOic("ack");
        }

        public class FerroampRes
        {
            [JsonProperty("msg")]
            public string Message { get; set; }
            [JsonProperty("status")]
            public string Status { get; set; }
            [JsonProperty("transId")]
            public string TransactionId { get; set; }
        }

        [Verb]
        public void BitArray()
        {
            Console.WriteLine(@"
    ____  _       ______          ___           
   / __ )(_)___ _/ ____/___  ____/ (_)___ _____ 
  / __  / / __ `/ __/ / __ \/ __  / / __ `/ __ \
 / /_/ / / /_/ / /___/ / / / /_/ / / /_/ / / / /
/_____/_/\__, /_____/_/ /_/\__,_/_/\__,_/_/ /_/ 
        /____/                                  
");
            BitArray(true);
            Console.WriteLine(@"

   _____                 __________          ___           
  / ___/____ ___  ____ _/ / / ____/___  ____/ (_)___ _____ 
  \__ \/ __ `__ \/ __ `/ / / __/ / __ \/ __  / / __ `/ __ \
 ___/ / / / / / / /_/ / / / /___/ / / / /_/ / / /_/ / / / /
/____/_/ /_/ /_/\__,_/_/_/_____/_/ /_/\__,_/_/\__,_/_/ /_/ 
                                                           
");
            BitArray(false);
        }
        public void BitArray(bool bigEndian)
        {
            Console.WriteLine("Bit\tBytes\t\t31             Bits               0");
            for (int i = 0; i < 32; i++)
            {
                var val = 1 << i;
                var bytes = EndianBitConverter.GetBytes(val, bigEndian);
                var arr1 = new BitArray(bytes);


                var b1 = new byte[4];
                arr1.CopyTo(b1, 0);
                Console.WriteLine($"{i}\t{b1.ToHexString()}\t{arr1.Cast<bool>().Paginate(8).Select(by=>by.Reverse().Select(b => b ? "1" : "-").Join("")).Join(" ")} Val:{val}");
            }
        }
    }
}
