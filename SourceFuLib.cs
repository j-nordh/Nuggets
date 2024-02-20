public class EnvironmentUtil
{
    public static CodeEnvironment CreateEnvironment(this ScriptomaticSettings s, string name, string path)
    {
        s.Environments.Add(name, path);
        var ret = new CodeEnvironment() { Name = name };
        s.SaveEnvironment(ret);
        return ret;
    }

    public static void SaveEnvironment(this ScriptomaticSettings s, CodeEnvironment env)
    {
        var path = s.Environments[env.Name];
        var dir = Path.GetDirectoryName(path);
        env.SetPathsRelative(dir);
        File.WriteAllText(s.Environments[env.Name], JsonConvert.SerializeObject(env, Formatting.Indented));
        env.SetPathsAbsolute(dir);
    }

    public static CodeEnvironment GetEnvironment(this ScriptomaticSettings s, string name)
    {
        if (!s.Environments.ContainsKey(name)) return null;
        var path = s.Environments[name];
        var dir = Path.GetDirectoryName(path);
        var ret = JsonConvert.DeserializeObject<CodeEnvironment>(File.ReadAllText(path));
        ret.SetPathsAbsolute(dir);
        return ret;
    }
}
