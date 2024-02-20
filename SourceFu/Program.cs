// See https://aka.ms/new-console-template for more information

using System.Text;
using Newtonsoft.Json;
using SourceFu;
using SourceFuLib;
using SupplyChain.Dto;

var swagger = new SwaggerClientGenerator(@"c:\temp\swagger.json", "");
swagger.Run();

partial class Program
{
    private CodeEnvironment _env;
    public void ApiClient()
    {
        
    }

    public void Env(string name){}
    private void LoadEnvironment(string name)
    {
        var settings =SourceFuSettingsUtil.GetSettings(false);
        _env = settings.GetEnvironment(name);
    }
}