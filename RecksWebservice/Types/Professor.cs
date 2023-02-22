namespace RecksWebservice.Types
{
    public class Professor
    {
        private string _name;
        private int _rating;
        private string[] _reviews;

        public Professor(string name, int rating, string[] reviews)
        {
            _name = name;
            _rating = rating;
            _reviews = reviews;
        }

        public Professor()
        {
            _name = string.Empty;
            _rating = 0;
            _reviews = Array.Empty<string>();
        }

        //Implement Methods

    }
}
