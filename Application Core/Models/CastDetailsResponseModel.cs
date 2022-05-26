using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Entities;

namespace Application_Core.Models
{
    public class CastDetailsResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }

        // IEumerable
        public List<MovieCardModel> MoviesOfCast { get; set; }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Id + " " + this.Name + " " + this.Gender + " " + this.TmdbUrl + " " + this.ProfilePath + "\n");
            foreach (var card in this.MoviesOfCast)
            {
                sb.Append(card.ToString() + "\n");
            }

            return sb.ToString();
        }

        public static CastDetailsResponseModel FromEntity(Cast cast, List<Movie> movies)
        {
            List<MovieCardModel> movieCards = new List<MovieCardModel>();
            foreach (Movie movie in movies)
            {
                movieCards.Add(MovieCardModel.FromEntity(movie));
            }

            CastDetailsResponseModel castDetails = new CastDetailsResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath,
                MoviesOfCast = movieCards
            };

            return castDetails;
        }
    }
}
