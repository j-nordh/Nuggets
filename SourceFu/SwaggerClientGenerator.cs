using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UtilClasses;
using UtilClasses.Extensions.Assemblies;
using UtilClasses.Extensions.Strings;

namespace SourceFu
{
    internal class SwaggerClientGenerator
    {
        private readonly string _jsonFile;
        private readonly string _destDir;

        public SwaggerClientGenerator(string jsonFile, string destDir)
        {
            _jsonFile = jsonFile;
            _destDir = destDir;
        }

        public void Run()
        {
            var json = File.ReadAllText(_jsonFile, Encoding.UTF8);
            var swagger = JObject.Parse(json);
            var title = swagger["info"]!["title"]!.ToString().SubstringAfterLast(".")+"Client";

            var template = GetType().Assembly.GetResourceString("SwaggerFileTemplate.txt");

            var kvr = new KeywordReplacer().Add("name", title);

            foreach (var path in swagger["paths"]!.Cast<JProperty>())
            {
                var url = path.Name;
                Console.WriteLine(path);
            }

            Console.WriteLine(kvr.Run(template));
        }
    }

    /*public async Task<SourceInfo?> Info() => await _client.GetJsonAsync<SourceInfo>("Source/Info");
    public async Task<ISourceConfig?> Get() => await _client.GetJsonAsync<ISourceConfig>("Source");
    public async Task TearDown() => await _client.PostAsync(new RestRequest("Source/Teardown", Method.Post));
    public async Task<IHardwareDefinition?> Hardware() => await _client.GetJsonAsync<IHardwareDefinition>("Source/Hardware");
    public async Task Init(IInitialParameters ps) => await _client.PostAsync(new RestRequest("Source/Init", Method.Post).AddBody(ps));
    public async Task Configure(ISourceConfig sc) => await _client.PostAsync(new RestRequest("Source/Configure", Method.Post).AddBody(sc));
    public async Task<List<MetaChannel>?> Channels() => await _client.GetJsonAsync<List<MetaChannel>>("Source/Channels");*/
}
