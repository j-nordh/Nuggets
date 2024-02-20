using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MsgReader.Outlook;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace Stamper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("There are no message files specified!");
                Console.ReadLine();
                return;
            }
            args.ForEach(Console.WriteLine);

            foreach (var p in args)
            {
                if (!File.Exists(p)) continue;
                var dir = Path.GetDirectoryName(p);
                var sb = new IndentingStringBuilder("");
                
                using (var msg = new Storage.Message(p))
                {
                    var s = msg.Sender.GetInitials();
                    var cc = msg.Recipients.GetInitials(RecipientType.Cc, "(", ")");
                    var bcc = msg.Recipients.GetInitials(RecipientType.Bcc, "[", "]");
                    sb
                        .Append(msg.SentOn?.ToString("yyMMdd HHmm")??"WTF")
                        .Append($" {s} - ")
                        .Append(msg.Recipients.GetInitials(RecipientType.To))
                        .MaybeAppend(cc.IsNotNullOrEmpty(), " " + cc)
                        .MaybeAppend(bcc.IsNotNullOrEmpty(), " " + bcc)
                        .Append(", ")
                        .Append(msg.FileName);
                }

                var newP = Path.Combine(dir, sb.ToString());
                if (p.EqualsOic(newP)) continue;

                File.Move(p, newP);
            }
        }

        
    }

    internal static class Extensions
    {
        public static string GetInitials(this Storage.Sender s)
        {
            return new Person(s.DisplayName, s.Email).GetInitials();
        }

        public static string GetInitials(this Storage.Recipient r)
        {
            return new Person(r.DisplayName, r.Email).GetInitials();
        }

        public static string GetInitials(this IEnumerable<Storage.Recipient> rs, RecipientType t)
        {
            return rs.Where(r => r.Type.Value == t).Select(GetInitials).Join(", ");
        }

        public static string GetInitials(this IEnumerable<Storage.Recipient> rs, RecipientType t, string pre,
            string post)
        {
            var ret = rs.GetInitials(t);
            if (ret.IsNullOrEmpty()) return "";
            return pre + ret + post;
        }

        private static string GetInitials(this Person s)
        {
            return s.DisplayName.Any()
                ? s.DisplayName.GetInitials(' ')
                : s.Email.SubstringBefore("@").GetInitials(' ');
        }

        private static string GetInitials(this string s, char delimiter)
        {
            return s
                .Split(delimiter)
                .Select(w => w.FirstOrDefault())
                .Where(c => c != '\0')
                .AsString();
        }

        private class Person
        {
            public string DisplayName { get; }
            public string Email { get; }

            public Person()
            {
            }

            public Person(string displayName, string email)
            {
                DisplayName = displayName;
                Email = email;
            }
        }
    }
}