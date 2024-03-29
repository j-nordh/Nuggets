﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScriptOMatic.Generate {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Queries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Queries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ScriptOMatic.Generate.Queries", typeof(Queries).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT
        ///    c.CONSTRAINT_NAME,
        ///    cu.TABLE_NAME AS ReferencingTable, cu.COLUMN_NAME AS ReferencingColumn,
        ///    ku.TABLE_NAME AS ReferencedTable, ku.COLUMN_NAME AS ReferencedColumn
        ///FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
        ///INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
        ///ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
        ///INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
        ///ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME.
        /// </summary>
        internal static string ForeingKeysQuery {
            get {
                return ResourceManager.GetString("ForeingKeysQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT KU.COLUMN_NAME
        ///FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
        ///INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KU
        ///	ON TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
        ///WHERE TC.TABLE_NAME=&apos;{0}&apos;
        ///AND TC.CONSTRAINT_TYPE = &apos;PRIMARY KEY&apos;.
        /// </summary>
        internal static string PrimaryKeyQuery {
            get {
                return ResourceManager.GetString("PrimaryKeyQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT
        ///    OBJECT_NAME(OBJECT_ID) Name,
        ///    Definition
        ///FROM
        ///    sys.sql_modules
        ///WHERE
        ///    objectproperty(OBJECT_ID, &apos;IsProcedure&apos;) = 1.
        /// </summary>
        internal static string ProcedureQuery {
            get {
                return ResourceManager.GetString("ProcedureQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  ORDINAL_POSITION, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,NUMERIC_SCALE
        ///       , IS_NULLABLE
        ///FROM INFORMATION_SCHEMA.COLUMNS
        ///WHERE TABLE_NAME = &apos;{0}&apos;.
        /// </summary>
        internal static string TableQuery {
            get {
                return ResourceManager.GetString("TableQuery", resourceCulture);
            }
        }
    }
}
