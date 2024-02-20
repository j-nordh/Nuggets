using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ScriptOMatic.Generate;
using ScriptOMatic.Generate.Extensions;
using SourceFuLib;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using UtilClasses.Extensions.Objects;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;

namespace SQLInjector
{
    public class SQLInjector
    {
        public enum ContentType
        {
            Bundle,
            StoredProcedures,
            DTO,
            Repo

        }

        public static string Run(CodeEnvironment env, bool sql = true, bool dto = false, bool repo = false)
        {
            var settings = SourceFuSettingsUtil.GetSettings();
            var def = env.GetDbDef();
            var bundles = env.GetDbDef().Bundles;

            var loader = new DbLoader(env);
            loader.Init(3);

            var sb = new IndentingStringBuilder("\t");
            var sps = new IndentingStringBuilder("\t");

            foreach (var bundle in bundles)
            {
                var popBundle = loader.Populate(bundle);
                var content = EnvHelper.GetContent(popBundle, ContentType.StoredProcedures, env);

                if (sql)
                {
                    sb.Encapsulate("SQL", EnvHelper.RunSql(content, env));
                    sps.AppendLines(content).AppendLines("", "GO", "");
                }

                if (dto) sb.Encapsulate("DTO", EnvHelper.RunDto(EnvHelper.GetContent(popBundle, ContentType.DTO, env), popBundle, env));
                if (repo) sb.Encapsulate("Repo", EnvHelper.RunRepo(EnvHelper.GetContent(popBundle, ContentType.Repo, env), popBundle, env));
            }

            sb.AppendLines();

            if (sql)
            {
                var workDir = new DirectoryInfo(settings.Environments[env.Name]).Name;
                var filePath = Path.Combine(workDir, env.Db.RestoreDir, "structure", "Stored procedures.sql");
                File.WriteAllText(filePath, sps.ToString());
            }

            return sb.ToString();
        }
        public static string Run(string envName)
        {
            var settings = SourceFuSettingsUtil.GetSettings();
            var env = settings.GetEnvironment(envName);

            return Run(env);
        }

    }
}

