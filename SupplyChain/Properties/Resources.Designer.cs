﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupplyChain.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SupplyChain.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select  
        ///   &apos;Parameter_name&apos; = name,  
        ///   &apos;Type&apos;   = type_name(user_type_id),  
        ///   &apos;Length&apos;   = max_length,  
        ///   &apos;Prec&apos;   = case when type_name(system_type_id) = &apos;uniqueidentifier&apos; 
        ///              then precision  
        ///              else OdbcPrec(system_type_id, max_length, precision) end,  
        ///   &apos;Scale&apos;   = OdbcScale(system_type_id, scale),  
        ///   &apos;Param_order&apos;  = parameter_id,  
        ///   &apos;Collation&apos;   = convert(sysname, 
        ///                   case when system_type_id in (35, 99, 167, 175, 231, 239)  
        ///             [rest of string was truncated]&quot;;.
        /// </summary>
        public static string ParameterQuery {
            get {
                return ResourceManager.GetString("ParameterQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT name
        ///FROM   dbo.sysobjects 
        ///WHERE  id = Object_id(N&apos;[dbo].[{0}]&apos;) 
        ///		AND Objectproperty(id, N&apos;IsProcedure&apos;) = 1.
        /// </summary>
        public static string ProcExistsQuery {
            get {
                return ResourceManager.GetString("ProcExistsQuery", resourceCulture);
            }
        }
    }
}