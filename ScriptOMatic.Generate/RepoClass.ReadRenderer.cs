using System;
using System.Collections.Generic;
using System.Linq;
using ScriptOMatic.Generate.Extensions;
using ScriptOMatic.Pages;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate
{
    partial class RepoClass
    {
        private class ReadRenderer
        {
            private readonly ColumnRenderer _cr;
            private readonly PopulatedBundle _bundle;
            private readonly string _name;
            private readonly ColumnProperties _col;
            public List<ColumnProperties> ParameterColumns { get; set; }
            public ColumnProperties QualifyingColumn { get; set; }
            public List<string> ExtraParameters { get; set; }
            public Func<string, string> ExtraFormatter { get; set; }

            public ReadRenderer(ColumnRenderer cr, PopulatedBundle bundle, string name, ColumnProperties col)
            {
                _cr = cr;
                _bundle = bundle;
                _col = col;

                if (name.EndsWith("Id"))
                    name = name.Substring(0, name.Length - 2);
                _name = name;
            }

            public IEnumerable<IAppendable> GetRead(bool returnsList, bool includeColumn, string methodName)
            {
                ExtraParameters = ExtraParameters ?? new List<string>();
                ParameterColumns = ParameterColumns ?? new List<ColumnProperties>();
                ExtraFormatter = ExtraFormatter ?? (s => s);

                var parameters = _cr.ParameterStrings(ParameterColumns).Union(ExtraParameters).ToList();
                var extraNames = ExtraParameters.Select(ep => ep.SplitREE(" ")[1]).Select(n => n.SubstringBefore(" ="));
                var psCasted = ParameterColumns.Select(_cr.Casted).Union(extraNames.Select(ExtraFormatter)).ToList();
                var psNames = ParameterColumns.Select(_cr.ParameterName).Union(extraNames).ToList();
                if (includeColumn)
                {
                    parameters.Add(_cr.ParameterString(_col));
                    psCasted.Add(_cr.Casted(_col));
                    psNames.Add(_cr.ParameterName(_col));
                }

                methodName = methodName ?? _name;

                if (QualifyingColumn != null)
                {
                    //psCasted.Add("null");
                    var ret = new List<IAppendable>();
                    if (_name.ContainsOic("Between"))
                        ret.AddRange(RenderMethod(true, methodName, parameters, psCasted));
                    else if (_name.ContainsOic("ForMax"))
                        ret.AddRange(RenderMethod(true, methodName, parameters, psCasted.Union(new []{"null"}).ToList()));
                    else if(_name.Contains("For"))
                        ret.AddRange(RenderMethod(true, methodName, parameters, psCasted.Union(new []{"null"}).ToList()));
                    if (psNames.ContainsOic(_cr.ParameterName(QualifyingColumn))) return ret;
                    psCasted.Add(_cr.Casted(QualifyingColumn));
                    parameters.Add(_cr.ParameterString(QualifyingColumn));
                    ret.AddRange(RenderMethod(false, methodName, parameters, psCasted));
                    return ret;
                }
                return RenderMethod(returnsList, methodName, parameters, psCasted);
            }

            private IEnumerable<IAppendable> RenderMethod(bool returnsList, string methodName, List<string> parameters, List<string> psCasted)
            {
                var query = "QueryOne";
                var returns = _bundle.Table.Singular;
                var toList = "";
                if (returnsList)
                {
                    returns = $"List<{returns}>";
                    query = "Query";
                    toList = $"?.ToList()??new {returns}()";
                }
                else if (QualifyingColumn != null && parameters.ContainsOic(_cr.ParameterString(QualifyingColumn)))
                {
                    query = "Query";
                    toList = ".FirstOrDefault()";
                }

                var fb = new FunctionBuilder()
                    .WithName(methodName)
                    .Returning(returns)
                    .WithParameters(parameters);
                var clean = _bundle.Dto.Stateful ? ".AsClean()" : "";

                var loadCall = $"_db.{query}(def.{_name}({psCasted.Join(", ")}))";
                if (!_bundle.RenderRepoAggregates() || _bundle.Read.OutputJson)
                {
                    yield return fb.Inline($"{loadCall}{clean}{toList};");
                    yield break;
                }

                var sep = psCasted.Any() ? ", " : "";

                yield return fb.Inline($"{methodName}({psCasted.Join(", ")}{sep}{_bundle.Table.Singular}.Aggregates.None);");
                yield return fb.Overload()
                    .WithParameter($"{_bundle.Table.Singular}.Aggregates load")
                    .WithoutOptionalParameters()
                    .Inline($"LoadAggregates({loadCall}, load){clean}{toList};");
            }
        }
    }
}