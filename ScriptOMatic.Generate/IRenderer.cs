using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Objects;

namespace ScriptOMatic.Generate
{
    public abstract class Renderer<T>
    {
        private readonly List<IAppendable> _parts;
        private bool _setup;
        private bool _tornDown;
        protected Renderer()
        {
            _parts = new List<IAppendable>();
        }

        private void Setup()
        {
            _setup = true;
            _parts.InsertRange(0, PerformSetup());
        }
        private void Teardown()
        {
            _tornDown = true;
            _parts.AddRange(PerformTeardown());
        }

        protected virtual IEnumerable<IAppendable> PerformSetup()
        {
            var s = SimpleSetup();
            if (s.IsNotNullOrEmpty())
                yield return new StringAppendable(s);

        }
        protected virtual IEnumerable<IAppendable> PerformTeardown()
        {
            var s = SimpleTeardown();
            if(s.IsNotNullOrEmpty())
                yield return new StringAppendable(s);
        }

        protected virtual string SimpleSetup() => null;
        protected virtual string SimpleTeardown() => null;
        protected abstract void CombineTo(List<IAppendable> parts, IndentingStringBuilder sb);

        public override string ToString()
        {
            if (!_tornDown)
                Teardown();
            var sb = new IndentingStringBuilder("  ");
            CombineTo(_parts.NotNull().ToList(), sb);
            return sb.ToString();
        }
        public Renderer<T> Append(IEnumerable<T> items) => this.Do(() => items.ForEach(Append));
        public Renderer<T> Append(T obj)
        {
            if (!_setup)
                Setup();
            _parts.AddRange(Render(obj));
            return this;
        }


        protected virtual IEnumerable<IAppendable> Render(T obj) => SimpleRender(obj).Select(s => new StringAppendable(s));
        public Renderer<T> AppendLinkTable(PopulatedLinkTable ptl)
        {
            if (!_setup)
                Setup();
            _parts.AddRange(RenderLinkTable(ptl));
            return this;
        }

        protected virtual IEnumerable<IAppendable> RenderLinkTable(PopulatedLinkTable ptl) => SimpleLinkTableRender(ptl).Select(s => new StringAppendable(s));

        protected virtual IEnumerable<string> SimpleRender(T obj)
        {
            yield break;
        }
        protected virtual IEnumerable<string> SimpleLinkTableRender(PopulatedLinkTable t)
        {
            yield break;
        }

        public Renderer<T> RenderTo(IndentingStringBuilder sb)
        {
            if(!_tornDown) Teardown();
            sb.AppendObjects(_parts.NotNull());
            return this;
        }


        public IEnumerable<IAppendable> Parts
        {
            get
            {
                if (!_tornDown) Teardown();
                return _parts.NotNull();
            }
        }


    }
}
