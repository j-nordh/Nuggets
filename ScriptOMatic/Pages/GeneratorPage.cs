using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilClasses.Extensions.Strings;
using SupplyChain.Dto;

namespace ScriptOMatic
{
    public partial class GeneratorPage : UserControl
    {
        private List<string> _cols;
        private CodeEnvironment _env;
        protected event Action EnvChanged;

        protected GeneratorPage()
        {
            InitializeComponent();
            _cols = new List<string>();
        }

        public CodeEnvironment Env
        {
            get => _env; set
            {
                _env = value;
                EnvChanged?.Invoke();
            }
        }

        public virtual string Caption => GetType().Name.SubstringBefore("Page");

        public enum ContentType
        {
            NotSpecified,
            Sql,
            DbDefinition,
            DtoClass,
            Repo,
            Controller,
            Enum,
            Error
        }
    }
}
