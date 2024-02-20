using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.LangSid
{
    internal class Resource
    {
        public readonly string Id;
        public readonly string Description;
        public readonly Dictionary<string, string> Translations;
        private string _name;
        private bool _isSectionId;

        public Resource(string id, string description, Dictionary<string, string> translations)
        {
            Id = id;
            Description = description;
            Translations = translations;
            _isSectionId = false;
        }

        public void Init(string className)
        {
            
            _name = Id.Replace(" ", "");
            if (_name.EqualsIc2(className))
            {
                _name = "SectionSid";
                _isSectionId = true;
            }
            else
            {
                _name = char.ToUpper(_name.First()) + _name.Substring(1);
                if (_name.StartsWithOic(className + "_") && !_name.EndsWithIc2(className))
                    _name = _name.Substring(className.Length + 1);
                if (!_name.Equals(_name.ToUpper(), StringComparison.InvariantCulture)) return;
                _name = _name.First() + _name.Substring(1).ToLower();
                while (true)
                {
                    int i = _name.IndexOf('_');
                    if (i < 0) break;
                    _name = _name.Substring(0, i) + _name.Substring(i + 1, 1).ToUpper() +
                            _name.Substring(i + 2);
                }
            }
        }

        public string Render()
        {
            _name = _name.Replace("_", "");
            var comment = Description;
            if (!String.IsNullOrEmpty(comment))
            {
                comment = " //" + comment;
            }//;
            return _isSectionId? $"public new static StringIdentifier {_name} => new StringIdentifier(SECTION, \"{Id}\", true);{comment}" 
                : $"public static StringIdentifier {_name} => new StringIdentifier(SECTION, \"{Id}\");{comment}";
        }
    }
}