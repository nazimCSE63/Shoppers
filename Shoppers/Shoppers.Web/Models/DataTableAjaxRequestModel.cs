using Microsoft.Extensions.Primitives;
using System.Text;

namespace Shoppers.Web.Models
{

    public class DataTablesAjaxRequestModel
    {
        private HttpRequest _request;
        public DataTablesAjaxRequestModel(HttpRequest request)
        {
            _request = request;
        }
        private int Start
        {
            get
            {
                var tmp = Convert.ToInt32(_request.Query["start"]);
                return tmp;
            }
        }
        public int Length
        {
            get
            {
                var tmp = Convert.ToInt32(_request.Query["length"]);
                return tmp;
            }
        }
        public string SearchText
        {
            get
            {
                var tmp = _request.Query["search[value]"];
                return tmp;
            }
        }
        public int PageIndex
        {
            get
            {
                if (Length > 0)
                    return (Start / Length) + 1;
                else
                    return 1;
            }
        }
        public int PageSize
        {
            get
            {
                if (Length == 0)
                    return 10;
                else
                    return Length;
            }
        }
        public static object EmptyResult
        {
            get
            {
                return new
                {
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = (new string[] { }).ToArray()
                };
            }
        }
        public string GetSortText(string[] columnNames)
        {
            var method = _request.Method.ToLower();
            if (method == "get")
            {
                var tmp = ReadValues(_request.Query, columnNames);
                return tmp;
            }
            else if (method == "post")
            {
                var tmp = ReadValues(_request.Form, columnNames);
                return tmp;
            }
            else
                throw new InvalidOperationException("Http method not supported, use get or post");
        }
        private string ReadValues(IEnumerable<KeyValuePair<string, StringValues>>
            requestValues, string[] columnNames)
        {
            var sortText = new StringBuilder();
            for (var i = 0; i < columnNames.Length; i++)
            {
                if (requestValues.Any(x => x.Key == $"order[{i}][column]"))
                {
                    if (sortText.Length > 0)
                        sortText.Append(",");

                    var columnValue = requestValues.Where(x => x.Key == $"order[{i}][column]").FirstOrDefault();
                    var directionValue = requestValues.Where(x => x.Key == $"order[{i}][dir]").FirstOrDefault();

                    var column = int.Parse(columnValue.Value.ToArray()[0]);
                    var direction = directionValue.Value.ToArray()[0];
                    var sortDirection = $"{columnNames[column]} {(direction == "asc" ? "asc" : "desc")}";
                    sortText.Append(sortDirection);
                }
            }
            return sortText.ToString();
        }
    }
}
