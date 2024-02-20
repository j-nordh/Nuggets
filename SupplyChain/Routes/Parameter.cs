using System;
using System.Collections.Generic;
using UtilClasses;

namespace SupplyChain.Routes
{
    internal class Parameter : IAppendable
    {
        public string Name { get; }
        public string Type { get; }
        public ParameterTypes ParameterType { get; }

        public string RealName { get; private set; }
        public bool Optional { get; }

        public enum ParameterTypes
        {
            Query,
            Body,
            Header,
            Route
        }

        public Parameter(string name, string type, ParameterTypes parameterType, bool optional)
        {
            Name = name;
            Type = Globals.FixForbidden(type);
            ParameterType = parameterType;
            Optional = optional;
            RealName = name;
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb) => 
            sb.AppendLine(ToString());

        public override string ToString()
        {
            switch (ParameterType)
            {
                case ParameterTypes.Query:
                    return $".AddParameter(\"{Name}\", {Name})";
                case ParameterTypes.Body:
                    return $".SetBody({Name})";
                case ParameterTypes.Header:
                    return $".WithHeader(\"{RealName}\", {Name})";
                case ParameterTypes.Route:
                    return $".WithRouteParam(\"{Name}\", {Name})";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Parameter WithRealName(string realName)
        {
            RealName = realName;
            return this;
        }
    }
}