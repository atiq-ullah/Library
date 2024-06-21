namespace Library
{
    public class SearchParameters
    {
        public SearchParameters() : this(new List<string>(), "") { }
        public SearchParameters(List<string> fields, string query)
        {
            Fields = new List<string>(fields);
            Query = new string(query);
        }

        public string NormalizeFields()
        {
            return String.Join(",", Fields);
        }

        public string NormalizeQuery()
        {
            return String.Join("+", Query.Split(' '));
        }

        public IEnumerable<string> Fields { get; set; }
        public string Query { get; set; }

    }
}
