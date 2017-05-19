using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Xero.Api.Infrastructure.ThirdParty.Dust.Core.SignatureBaseStringParts.Parameters {
    internal class RequestParameters : IEnumerable<Parameter> {
        private readonly Dictionary<string, StringValues> _values;
        private readonly Parameters _parameters;

        public RequestParameters(Request request) {
            _values = QueryHelpers.ParseQuery(request.Url.Query);
            _parameters = new Parameters(MapAll());
        }

        private Parameter[] MapAll() {
            return _values.Keys.SelectMany(Map).ToArray();
        }

        private IEnumerable<Parameter> Map(string key) {
            return ValuesFor(key).Select(v => new Parameter(key, v));
        }

        private string[] ValuesFor(string key) {
            StringValues result;

            if(_values.TryGetValue(key, out result)) {
                return result.ToArray();
            }

            return new string[0];
        }

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<Parameter> GetEnumerator() {
            return _parameters.GetEnumerator();
        }

        #endregion

        internal void Add(Parameters what) {
            _parameters.Add(what);
        }

        public override string ToString() {
            return _parameters.ToString();
        }
    }
}