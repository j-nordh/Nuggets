
using Newtonsoft.Json;
using SupplyChain.Dto;
using System;
using System.IO;
using UtilClasses;
using System.Text;
namespace SourceFuLib;
public static class SourceFuSettingsUtil
{
    public static string SettingsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SourceFu", "Settings.json");
    private static string OldSettingsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ScriptOMatic", "Settings.json");

    public static SourceFuSettings GetSettings(bool acceptDefault=true)
    {
        string? path = null;
        if (File.Exists(SettingsPath)) path = SettingsPath;
        else if(File.Exists(OldSettingsPath)) path = OldSettingsPath;
        if(null!= path)
            return JsonConvert.DeserializeObject<SourceFuSettings>(File.ReadAllText(path, Encoding.UTF8))!;

        if (acceptDefault)
            return Default();
        
        throw new FileNotFoundException("Could not find a ScriptFu settings file!!!");
        
    }
    public static CodeEnvironment CreateEnvironment(this SourceFuSettings s, string name, string path)
    {
        s.Environments.Add(name, path);
        var ret = new CodeEnvironment() { Name = name };
        s.SaveEnvironment(ret);
        return ret;
    }

    public static void SaveEnvironment(this SourceFuSettings s, CodeEnvironment env)
    {
        var path = s.Environments[env.Name];
        var dir = Path.GetDirectoryName(path);
        env.SetPathsRelative(dir);
        File.WriteAllText(s.Environments[env.Name], JsonConvert.SerializeObject(env, Formatting.Indented));
        env.SetPathsAbsolute(dir);
    }

    public static CodeEnvironment GetEnvironment(this SourceFuSettings s, string name)
    {
        if (!s.Environments.ContainsKey(name)) return null;
        var path = s.Environments[name];
        var dir = Path.GetDirectoryName(path);
        var ret = JsonConvert.DeserializeObject<CodeEnvironment>(File.ReadAllText(path));
        ret.SetPathsAbsolute(dir);
        return ret;
    }
    public static SourceFuSettings Default()
    {
        var envDir = new DictionaryOic<string> { { "Eco", "C:\\Prog\\EnergyCostOptimizer\\CodeEnvironment.json" } };
        var s = new SourceFuSettings()
        {
            Environments = envDir,
            BasePath = "C:\\Prog",
            MsBuildPath = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Professional\\MSBuild\\15.0\\Bin\\amd64\\MSBuild.exe",
            SupplyChainPath = "C:\\Source\\Nuggets\\SupplyChain\\bin\\Debug\\SupplyChain.exe",
            DrawSqlPath = "C:\\Source\\Nuggets\\DrawSql\\bin\\Debug\\DrawSql.exe"
        };
        return s;
    }
}