namespace RecksWebservice.Types
{
    public class Professor
    {
        private string _name;
        private int _rating;
        private string[] _reviews;

        #region Constructors
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
        #endregion

        #region GET-SET Methods
        public string GetName() => _name;
        public int GetRating() => _rating;
        public string[] GetReviews() => _reviews;
        public void SetRating(int rating) => _rating = rating;
        public void SetName(string name) => _name = name;
        public void FillReviews(string[] reviews) => _reviews = reviews;
        #endregion

    }
}
