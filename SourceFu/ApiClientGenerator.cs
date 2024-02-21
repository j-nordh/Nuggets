using UtilClasses;
using UtilClasses.CodeGeneration;
using UtilClasses.CodeGeneration;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.CodeGen;
using UtilClasses.Dto;
using UtilClasses.Interfaces;

namespace SourceFu
{
    internal class ApiClientGenerator
    {
        private readonly string _basePath;
        private readonly TextFileMap _map;

        public ApiClientGenerator(string basePath, TextFileMap map)
        {
            _basePath = basePath;
            _map = map;
        }

        //public void Run( Dictionary<string, List<VerbDscriptor>> controllers)
        //{
        //    foreach (var name in controllers.Keys)
        //    {
        //        var cls = new ClassBuilder() {AccessModifier = "public", Name = name};
        //    }
        //}
        //public class VerbDescriptor : IHasName
        //{
        //    public string Name { get; set; }
        //    public string FromBody { get; set; }
        //    public string Returns { get; set; }
        //}

        //private class Verb:ICodeElement
        //{
        //    private readonly VerbDescriptor _vd;
        //    private List<string> _requires;
        //    private TextFileMap _map;

        //    public Verb(VerbDescriptor vd, TextFileMap map)
        //    {
        //        _map = map;
        //        _vd = vd;
        //        Name = vd.Name;
        //        _requires = new List<string>();

        //        Add(vd.FromBody);
        //        Add(vd.Returns);
        //        _vd.Parameters
        //            .Select(p => p.Type)
        //            .ForEach(Add);
                
        //        if (null != vd.FromBody)
        //            _requires.Add(map.GetNamespace(vd.FromBody.Name));
        //        if(!(null==vd.Returns || vd.Returns.Name.EqualsOic("void")))
        //            _requires.Add(map.GetNamespace(vd.Returns.Name));
        //    }

        //    void Add(TypeDescriptor? td)
        //    {
        //        if (null == td) return;
        //        Add(td.Name);
        //    }

        //    void Add(string t)
        //    {
        //        if (t.EqualsOic("void")) return;
        //        _requires.Add(_map.GetNamespace(t));
        //    }

        //    public string Name { get; }
        //    public IEnumerable<string> Requires => _requires;
        //    public void AppendTo(IndentingStringBuilder sb)
        //    {
        //        string Opt(VerbParameterDescriptor? vpd)
        //        {
        //            if (null == vpd) return "";
        //            if (vpd.Optional == false) return"";
        //            return " = default";
        //        }
        //        var ret = _vd.Returns == null || _vd.Returns.Name.EqualsOic("void") 
        //            ? null 
        //            : _vd.Returns.Name;
        //        var fn = new FunctionBuilder()
        //        {
        //            Name = _vd.Name,
        //            Returns = ret,
        //            Parameters = _vd.Parameters.OrderBy(p=>p.Optional).Select(p=>$"{p.Type} {p.Name}{Opt(p)}").ToList(),
        //            Modifier = "public",
        //            Inline = false
        //        };
        //        fn.AppendObject(sb);
        //    }
        //}
    }
}
