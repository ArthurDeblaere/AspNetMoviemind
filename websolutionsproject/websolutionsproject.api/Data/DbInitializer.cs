using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;

namespace websolutionsproject.api.Data
{
    public static class DbInitializer
    {
        private static MovieMindContext _context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<MovieMindContext>();
            UserManager<User> _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<Role> _roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            // Delete existing database and create new database with default test data
            // =======================================================================

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Look for any roles
            // If no roles found then the db is empty
            // Fill the db with test data
            // ======================================

            if (_context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            // Create roles
            // ============

            _ = _roleManager.CreateAsync(new Role { Name = "Administrator", Description = "Administrator" }).Result;
            _ = _roleManager.CreateAsync(new Role { Name = "Editor", Description = "Editor" }).Result;
            _ = _roleManager.CreateAsync(new Role { Name = "Guest", Description = "Guest" }).Result;

            // Create users
            // ============
            User admin = new User
            {
                FirstName = "Admin",
                LastName = "MovieMind",
                Email = "admin@moviemind.com",
                UserName = "admin@moviemind.com",
                Description = "The admin of this site."
            };

            _ = _userManager.CreateAsync(admin, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(admin, new List<string> { "Administrator", "Editor", "Guest" }).Result;


            User administrator = new User
            {
                FirstName = "Arthur",
                LastName = "Deblaere",
                Email = "arthur.deblaere@hotmail.be",
                UserName = "arthur.deblaere@moviemind.com",
                Description = "The founder of this site."
            };

            _ = _userManager.CreateAsync(administrator, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(administrator, new List<string> { "Administrator", "Editor", "Guest"}).Result;

            User editor = new User
            {
                FirstName = "Editor",
                LastName = "MovieMind",
                Email = "editor@moviemind.com",
                UserName = "editor@moviemind.com",
                Description = "The main editor of this site."
            };

            _ = _userManager.CreateAsync(editor, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(editor, new List<string> {"Editor", "Guest"}).Result;

            User guest = new User
            {
                FirstName = "Guest",
                LastName = "MovieMind",
                Email = "guest@moviemind.com",
                UserName = "guest@moviemind.com",
                Description = "A sample guest of this site."
            };

            _ = _userManager.CreateAsync(guest, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(guest, new List<string> {"Guest" }).Result;


            User thomas = new User
            {
                FirstName = "Thomas",
                LastName = "Verstraete",
                Email = "thomas.verstraete@moviemind.com",
                UserName = "thomas.verstraete",
                Description = "Hi, I'm Thomas, a Belgian student and movie lover!"
            };

            _ = _userManager.CreateAsync(thomas, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(thomas, new List<string> { "Guest" }).Result;

            User andreas = new User
            {
                FirstName = "Andreas",
                LastName = "Holvoet",
                Email = "andreas.holvoet@moviemind.com",
                UserName = "andreas.holvoet",
                Description = "Hi, I'm Andreas! Welcome to my profile. I really like a good horror movie with a tense feeling."
            };

            _ = _userManager.CreateAsync(andreas, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(andreas, new List<string> { "Guest" }).Result;

            _context.SaveChanges();

            // Create genres
            // =============

            Genre documentary = new Genre { Name = "Documentary" };
            _context.Genres.Add(documentary);

            Genre thriller = new Genre { Name = "Thriller" };
            _context.Genres.Add(thriller);

            Genre comedy = new Genre { Name = "Comedy" };
            _context.Genres.Add(comedy);

            Genre horror = new Genre { Name = "Horror" };
            _context.Genres.Add(horror);

            Genre action = new Genre { Name = "Action" };
            _context.Genres.Add(action);

            Genre animation = new Genre { Name = "Animation" };
            _context.Genres.Add(animation);

            Genre drama = new Genre { Name = "Drama" };
            _context.Genres.Add(drama);

            _context.SaveChanges();

            // Create actors
            // =============

            Actor diCaprio = new Actor
            {
                FirstName = "Leonardo",
                LastName = "Di Caprio",
                Birth = DateTime.Parse("11/11/1974"),
                Nationality = "American",
                Description = "Few actors in the world have had a career quite as diverse as Leonardo DiCaprio's. " +
                "DiCaprio has gone from relatively humble beginnings, as a supporting cast member of the sitcom Growing Pains (1985) and " +
                "low budget horror movies, such as Critters 3 (1991), to a major teenage heartthrob in the 1990s, as " +
                "the hunky lead actor in movies such as Romeo + Juliet (1996) and Titanic (1997), to then become a leading man in Hollywood " +
                "blockbusters, made by internationally renowned directors such as Martin Scorsese and Christopher Nolan."
            };
            _context.Actors.Add(diCaprio);

            Actor bale = new Actor
            {
                FirstName = "Christian",
                LastName = "Bale",
                Birth = DateTime.Parse("30/01/1974"),
                Nationality = "British",
                Description = "Christian Charles Philip Bale was born in Pembrokeshire, Wales, UK on January 30, 1974, to English parents Jennifer " +  
                "\"Jenny\" (James) and David Bale. His mother was a circus performer and his father, who was born in South Africa, was a commercial pilot. " + 
                "The family lived in different countries throughout Bale's childhood, including England, Portugal, and the United States. Bale acknowledges the constant change was one of the influences on his career choice."
            };
            _context.Actors.Add(bale);

            Actor cooper = new Actor
            {
                FirstName = "Bradley",
                LastName = "Cooper",
                Birth = DateTime.Parse("05/01/1975"),
                Nationality = "American",
                Description = "Bradley Charles Cooper was born on January 5, 1975 in Philadelphia, Pennsylvania. His mother, Gloria (Campano), is of Italian descent, and worked for a local NBC station. His father, Charles John " +  
                "Cooper, who was of Irish descent, was a stockbroker. Immediately after Bradley graduated from the Honors English program at Georgetown University in 1997, he moved to New York City to enroll in the Masters of Fine " + 
                "Arts program at the Actors Studio Drama School at New School University. There, he developed his stage work, culminating with his thesis performance as John Merrick in Bernard Pomerance's \"The Elephant Man\", performed in New York's Circle in the Square."
            };
            _context.Actors.Add(cooper);

            Actor gyllenhaal = new Actor
            {
                FirstName = "Jake",
                LastName = "Gyllenhaal",
                Birth = DateTime.Parse("19/12/1980"),
                Nationality = "American",
                Description = "Jacob Benjamin Gyllenhaal was born in Los Angeles, California, to producer/screenwriter Naomi Foner (née Achs)" +
                " and director Stephen Gyllenhaal. He is the brother of actress Maggie Gyllenhaal, who played his sister in Donnie Darko (2001). " +
                "His godmother is actress Jamie Lee Curtis. His mother is from a Jewish family, and his father's ancestry includes Swedish, English and Swiss-German."
            };
            _context.Actors.Add(gyllenhaal);

            Actor mcConaughey = new Actor
            {
                FirstName = "Matthew",
                LastName = "McConaughey",
                Birth = DateTime.Parse("4/11/1969"),
                Nationality = "American",
                Description = "American actor and producer Matthew David McConaughey was born in Uvalde, Texas. His mother, Mary Kathleen (McCabe), is a substitute " +
                "school teacher originally from New Jersey. His father, James Donald McConaughey, was a Mississippi-born gas station owner who ran an oil pipe supply " +
                "business. He is of Irish, Scottish, English, German, and Swedish descent. Matthew grew up in Longview, Texas, where he graduated from the local High School (1988). " +
                "Showing little interest in his father's oil business, which his two brothers later joined, Matthew was longing for a change of scenery, and spent a year in Australia, " +
                "washing dishes and shoveling chicken manure. Back to the States, he attended the University of Texas in Austin, originally wishing to be a lawyer. But, when he discovered an " +
                "inspirational Og Mandino book The Greatest Salesman in the World before one of his final exams, he suddenly knew he had to change his major from law to film."
            };
            _context.Actors.Add(mcConaughey);

            Actor pitt = new Actor
            {
                FirstName = "Brad",
                LastName = "Pitt",
                Birth = DateTime.Parse("18/12/1963"),
                Nationality = "American",
                Description = "An actor and producer known as much for his versatility as he is for his handsome face, Golden Globe-winner Brad Pitt's most widely recognized role may be Tyler " +
                "Durden in Fight Club (1999). However, his portrayals of Billy Beane in Moneyball (2011), and Rusty Ryan in the remake of Ocean's Eleven (2001) and its sequels, also loom large in his filmography."
            };
            _context.Actors.Add(pitt);

            Actor robbie = new Actor
            {
                FirstName = "Margot",
                LastName = "Robbie",
                Birth = DateTime.Parse("02/07/1990"),
                Nationality = "Australian",
                Description = "Margot Elise Robbie was born on July 2, 1990 in Dalby, Queensland, Australia to Scottish parents. Her mother, Sarie Kessler, is a physiotherapist, " +
                "and her father, is Doug Robbie. She comes from a family of four children, having two brothers and one sister. She graduated from Somerset College in Mudgeeraba, Queensland, Australia, " +
                "a suburb in the Gold Coast hinterland of South East Queensland, where she and her siblings were raised by their mother and spent much of her time at the farm belonging to her" +
                " grandparents. In her late teens, she moved to Melbourne, Victoria, Australia to pursue an acting career."
            };
            _context.Actors.Add(robbie);

            Actor watts = new Actor
            {
                FirstName = "Naomi",
                LastName = "Watts",
                Birth = DateTime.Parse("28/09/1968"),
                Nationality = "British",
                Description = "Naomi Ellen Watts was born on September 28, 1968 in Shoreham, England, to Myfanwy Edwards Miv (Roberts), an antiques dealer " +
                "and costume/set designer, and Peter Watts (Peter Anthony Watts), the road manager to Pink Floyd. Her maternal grandfather was Welsh. Her father " +
                "died when Naomi was seven and she began to follow her mother and her brother around England, until she was fourteen, then they settled in Australia, " +
                "where her maternal grandmother was from. She coaxed her mother into letting her take acting class when they arrived. After bit parts in commercials, " +
                "she landed her first role in For Love Alone (1986). Naomi met her best friend, Nicole Kidman, when they both auditioned for a bikini commercial and they shared a " +
                "taxi ride home. In 1991, Naomi starred along Kidman in the sleeper-hit Flirting (1991) directed by John Duigan. Naomi continued her career by starring in the Australian " +
                "Brides of Christ (1991) co-starring Oscar-winners Russell Crowe and Brenda Fricker."
            };
            _context.Actors.Add(watts);

            Actor kunis = new Actor
            {
                FirstName = "Mila",
                LastName = "Kunis",
                Birth = DateTime.Parse("14/08/1983"),
                Nationality = "Ukranian",
                Description = "Mila Kunis was born Milena Markovna Kunis to a Jewish family in Chernivtsi, Ukraine, USSR (now independent Ukraine). Her mother, Elvira, is a physics teacher, " +
                "her father, Mark Kunis, is a mechanical engineer, and she has an older brother named Michael. Her family moved to Los Angeles, California, in 1991. After attending one semester of " +
                "college between gigs, she realized that she wanted to act for the rest of her life. She started acting when she was nine years old, when her father heard about an acting class on the radio and " +
                "decided to enroll Mila in it. There, she met her future agent. Her first gig was when she played a character named Melinda in Make a Wish, Molly (1995). From there, her career skyrocketed into big-budget films."
            };
            _context.Actors.Add(kunis);

            Actor theron = new Actor
            {
                FirstName = "Charlize",
                LastName = "Theron",
                Birth = DateTime.Parse("07/08/1975"),
                Nationality = "South-African",
                Description = "Charlize Theron was born in Benoni, a city in the greater Johannesburg area, in South Africa, the only child of Gerda Theron (née Maritz) and Charles Theron. She was raised on a farm outside the city. " +
                "Theron is of Afrikaner (Dutch, with some French Huguenot and German) descent, and Afrikaner military figure Danie Theron was her great-great-uncle."
            };
            _context.Actors.Add(theron);

            Actor lawrence = new Actor
            {
                FirstName = "Jennifer",
                LastName = "Lawrence",
                Birth = DateTime.Parse("15/08/1990"),
                Nationality = "American",
                Description = "Was the highest-paid actress in the world in 2015 and 2016. With her films grossing over $5.5 billion worldwide, Jennifer Lawrence is often cited as " +
                "the most successful actor of her generation. She is also thus far the only person born in the 1990s to have won an acting Oscar. Jennifer Shrader Lawrence was born " +
                "August 15, 1990 in Louisville, Kentucky, to Karen (Koch), who manages a children's camp, and Gary Lawrence, who works in construction. She has two older brothers, Ben and Blaine, " +
                "and has English, German, Irish, and Scottish ancestry."
            };
            _context.Actors.Add(lawrence);

            _context.SaveChanges();

            // Create directors
            // ================

            Director fincher = new Director
            {
                FirstName = "David",
                LastName = "Fincher",
                Birth = DateTime.Parse("28/08/1962"),
                Nationality = "American",
                Description = "David Fincher was born in 1962 in Denver, Colorado, and was raised in Marin County, California. When he was 18 years old he went to work for John Korty at " +
                "Korty Films in Mill Valley. He subsequently worked at ILM (Industrial Light and Magic) from 1981-1983. Fincher left ILM to direct TV commercials and music videos after " +
                "signing with N. Lee Lacy in Hollywood. He went on to found Propaganda in 1987 with fellow directors Dominic Sena, Greg Gold and Nigel Dick. Fincher has directed TV commercials " +
                "for clients that include Nike, Coca-Cola, Budweiser, Heineken, Pepsi, Levi's, Converse, AT&T and Chanel. He has directed music videos for Madonna, Sting, The Rolling Stones," +
                " Michael Jackson, Aerosmith, George Michael, Iggy Pop, The Wallflowers, Billy Idol, Steve Winwood, The Motels and, most recently, A Perfect Circle."
            };
            _context.Directors.Add(fincher);

            Director scorsese = new Director
            {
                FirstName = "Martin",
                LastName = "Scorsese",
                Birth = DateTime.Parse("17/11/1942"),
                Nationality = "American",
                Description = "Martin Charles Scorsese was born on November 17, 1942 in Queens, New York City, to Catherine Scorsese (née Cappa) and Charles Scorsese, who both worked in Manhattan's" +
                " garment district, and whose families both came from Palermo, Sicily. He was raised in the neighborhood of Little Italy, which later provided the inspiration for several of his films." +
                " Scorsese earned a B.S. degree in film communications in 1964, followed by an M.A. in the same field in 1966 at New York University's School of Film. During this time, he made numerous " +
                "prize-winning short films including The Big Shave (1967), and directed his first feature film, I Call First (1967)."
            };
            _context.Directors.Add(scorsese);

            Director russel = new Director
            {
                FirstName = "David",
                LastName = "Russel",
                Birth = DateTime.Parse("20/08/1958"),
                Nationality = "American",
                Description = "David Owen Russell is an American film writer, director, and producer, known for a cinema of intense, tragi-comedic characters whose love of life can surpass dark circumstances " +
                "faced in very specific worlds. His films address such themes as mental illness as stigma or hope; invention of self and survival; the family home as nexus of love, hate, transgression, and " +
                "strength; women of power and inspiration; beauty and comedy found in twisted humble circumstances; the meaning of violence, war, and greed; and the redemptive power of music above all."
            };
            _context.Directors.Add(russel);

            Director tarantino = new Director
            {
                FirstName = "Quentin",
                LastName = "Tarantino",
                Birth = DateTime.Parse("27/03/1963"),
                Nationality = "American",
                Description = "Quentin Jerome Tarantino was born in Knoxville, Tennessee. His father, Tony Tarantino, is an Italian-American actor and musician from New York," +
                " and his mother, Connie (McHugh), is a nurse from Tennessee. Quentin moved with his mother to Torrance, California, when he was four years old."
            };
            _context.Directors.Add(tarantino);

            Director nolan = new Director
            {
                FirstName = "Christopher",
                LastName = "Nolan",
                Birth = DateTime.Parse("30/07/1970"),
                Nationality = "British",
                Description = "Best known for his cerebral, often nonlinear, storytelling, acclaimed writer-director Christopher Nolan was born on July 30, 1970, in London, England." + 
                " Over the course of 15 years of filmmaking, Nolan has gone from low-budget independent films to working on some of the biggest blockbusters ever made."

            };
            _context.Directors.Add(nolan);

            _context.SaveChanges();

            // Create movies
            // =============

            Movie onceUponATimeInHollywood = new Movie
            {
                Name = "Once Upon a Time In Hollywood",
                Year = 2019,
                Description = "A faded television actor and his stunt double strive to achieve fame and success in the final years of Hollywood's Golden Age in 1969 Los Angeles.",
                OverallRating = 7,
                Duration = 161,
                DirectorId = FindDirector("Quentin", "Tarantino").Id,
                Director = FindDirector("Quentin", "Tarantino"),
                GenreId = FindGenre("Action").Id,
                Genre = FindGenre("Action")
            };
            _context.Movies.Add(onceUponATimeInHollywood);

            Movie theWolfOfWallStreet = new Movie
            {
                Name = "The Wolf of Wall Street",
                Year = 2013,
                Description = "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.",
                OverallRating = 8,
                Duration = 180,
                DirectorId = FindDirector("Martin", "Scorsese").Id,
                Director = FindDirector("Martin", "Scorsese"),
                GenreId = FindGenre("Drama").Id,
                Genre = FindGenre("Drama")
            };
            _context.Movies.Add(theWolfOfWallStreet);

            Movie theAudition = new Movie
            {
                Name = "The Audition",
                Year = 2015,
                Description = "Robert De Niro and Leonardo DiCaprio must compete for the lead role in Martin Scorsese’s next film.",
                OverallRating = 2,
                Duration = 16,
                DirectorId = FindDirector("Martin", "Scorsese").Id,
                Director = FindDirector("Martin", "Scorsese"),
                GenreId = FindGenre("Comedy").Id,
                Genre = FindGenre("Comedy")
            };
            _context.Movies.Add(theAudition);

            Movie shutterIsland = new Movie
            {
                Name = "Shutter Island",
                Year = 2010,
                Description = "World War II soldier - turned - U.S.Marshal Teddy Daniels investigates the disappearance of a patient from a" + 
                " hospital for the criminally insane, but his efforts are compromised by his troubling visions and also by a mysterious doctor.",
                OverallRating = 4,
                Duration = 138,
                DirectorId = FindDirector("Martin", "Scorsese").Id,
                Director = FindDirector("Martin", "Scorsese"),
                GenreId = FindGenre("Thriller").Id,
                Genre = FindGenre("Thriller")
            };
            _context.Movies.Add(shutterIsland);

            Movie inception = new Movie
            {
                Name = "Inception",
                Year = 2010,
                Description = "Cobb, a skilled thief who commits corporate espionage by infiltrating the subconscious of his targets is offered a chance to regain " + 
                    "his old life as payment for a task considered to be impossible: “inception”, the implantation of another person’s idea into a target’s subconscious.",
                OverallRating = 8,
                Duration = 148,
                DirectorId = FindDirector("Christopher", "Nolan").Id,
                Director = FindDirector("Christopher", "Nolan"),
                GenreId = FindGenre("Thriller").Id,
                Genre = FindGenre("Thriller")
            };
            _context.Movies.Add(inception);

            Movie fightClub = new Movie
            {
                Name = "Fight Club",
                Year = 1999,
                Description = "A ticking-time-bomb insomniac and a slippery soap salesman channel primal male aggression into a shocking " + 
                "new form of therapy. Their concept catches on, with underground “fight clubs” forming in every town, until an eccentric gets in the way and ignites an out-of-control spiral toward oblivion.",
                OverallRating = 8,
                Duration = 139,
                DirectorId = FindDirector("David", "Fincher").Id,
                Director = FindDirector("David", "Fincher"),
                GenreId = FindGenre("Drama").Id,
                Genre = FindGenre("Drama")
            };
            _context.Movies.Add(fightClub);

            Movie zodiac = new Movie
            {
                Name = "Zodiac",
                Year = 2007,
                Description = "The true story of the investigation of the “Zodiac Killer”, a serial killer who terrified the San " + 
                "Francisco Bay Area, taunting police with his ciphers and letters. The case becomes an obsession for three men as their " + 
                "lives and careers are built and destroyed by the endless trail of clues.",
                OverallRating = 8,
                Duration = 157,
                DirectorId = FindDirector("David", "Fincher").Id,
                Director = FindDirector("David", "Fincher"),
                GenreId = FindGenre("Drama").Id,
                Genre = FindGenre("Drama")
            };
            _context.Movies.Add(zodiac);

            Movie interstellar = new Movie
            {
                Name = "Interstellar",
                Year = 2014,
                Description = "Interstellar chronicles the adventures of a group of explorers who make use of a newly discovered wormhole " + 
                "to surpass the limitations on human space travel and conquer the vast distances involved in an interstellar voyage.",
                OverallRating = 8,
                Duration = 169,
                DirectorId = FindDirector("Christopher", "Nolan").Id,
                Director = FindDirector("Christopher", "Nolan"),
                GenreId = FindGenre("Drama").Id,
                Genre = FindGenre("Drama")
            };
            _context.Movies.Add(interstellar);

            Movie darkKnight = new Movie
            {
                Name = "The Dark Knight",
                Year = 2008,
                Description = "Batman raises the stakes in his war on crime.With the help of Lt.Jim Gordon and District Attorney Harvey Dent, " + 
                    "Batman sets out to dismantle the remaining criminal organizations that plague the streets.The partnership proves to be effective, " +
                    "but they soon find themselves prey to a reign of chaos unleashed by a rising criminal mastermind known to the terrified citizens of Gotham as the Joker.",
                OverallRating = 8,
                Duration = 152,
                DirectorId = FindDirector("Christopher", "Nolan").Id,
                Director = FindDirector("Christopher", "Nolan"),
                GenreId = FindGenre("Drama").Id,
                Genre = FindGenre("Drama")
            };
            _context.Movies.Add(darkKnight);

            Movie americanHustle = new Movie
            {
                Name = "American Hustle",
                Year = 2013,
                Description = "A con man, Irving Rosenfeld, along with his seductive partner Sydney Prosser, is forced to work for a wild F.B.I. Agent, Richie DiMaso, who pushes them into a world of Jersey powerbrokers and the Mafia.",
                OverallRating = 7,
                Duration = 138,
                DirectorId = FindDirector("David", "Russel").Id,
                Director = FindDirector("David", "Russel"),
                GenreId = FindGenre("Drama").Id,
                Genre = FindGenre("Drama")
            };
            _context.Movies.Add(americanHustle);

            Movie silverLingingsPlayook = new Movie
            {
                Name = "Silver Linings Playbook",
                Year = 2012,
                Description = "After spending eight months in a mental institution, a former teacher moves back in with his parents and tries to reconcile with his ex - wife.",
                OverallRating = 7,
                Duration = 122,
                DirectorId = FindDirector("David", "Russel").Id,
                Director = FindDirector("David", "Russel"),
                GenreId = FindGenre("Comedy").Id,
                Genre = FindGenre("Comedy")
            };
            _context.Movies.Add(silverLingingsPlayook);

            Movie seven = new Movie
            {
                Name = "Se7en",
                Year = 1995,
                Description = "Two detectives, a rookie and a veteran, hunt a serial killer who uses the seven deadly sins as his motives.",
                OverallRating = 9,
                Duration = 127,
                DirectorId = FindDirector("David", "Fincher").Id,
                Director = FindDirector("David", "Fincher"),
                GenreId = FindGenre("Thriller").Id,
                Genre = FindGenre("Thriller")
            };
            _context.Movies.Add(seven);

            _context.SaveChanges();

            InitRelatioships();
        }

        private static void InitRelatioships()
        {
            //Actormovie
            //==========

            //American Hustle
            ActorMovie americanHustleLawrence = new ActorMovie
            {
                ActorId = FindActor("Jennifer", "Lawrence").Id,
                Actor = FindActor("Jennifer", "Lawrence"),
                MovieId = FindMovie("American Hustle", 2013).Id,
                Movie = FindMovie("American Hustle", 2013)
            };
            _context.ActorMovies.Add(americanHustleLawrence);

            ActorMovie americanHustleCooper = new ActorMovie
            {
                ActorId = FindActor("Bradley", "Cooper").Id,
                Actor = FindActor("Bradley", "Cooper"),
                MovieId = FindMovie("American Hustle", 2013).Id,
                Movie = FindMovie("American Hustle", 2013)
            };
            _context.ActorMovies.Add(americanHustleCooper);

            ActorMovie americanHustleBale = new ActorMovie
            {
                ActorId = FindActor("Christian", "Bale").Id,
                Actor = FindActor("Christian", "Bale"),
                MovieId = FindMovie("American Hustle", 2013).Id,
                Movie = FindMovie("American Hustle", 2013)
            };
            _context.ActorMovies.Add(americanHustleBale);

            //Silver Linings Playbook
            ActorMovie silverLawrence = new ActorMovie
            {
                ActorId = FindActor("Jennifer", "Lawrence").Id,
                Actor = FindActor("Jennifer", "Lawrence"),
                MovieId = FindMovie("Silver Linings Playbook", 2012).Id,
                Movie = FindMovie("Silver Linings Playbook", 2012)
            };
            _context.ActorMovies.Add(silverLawrence);

            ActorMovie silverCooper = new ActorMovie
            {
                ActorId = FindActor("Bradley", "Cooper").Id,
                Actor = FindActor("Bradley", "Cooper"),
                MovieId = FindMovie("Silver Linings Playbook", 2012).Id,
                Movie = FindMovie("Silver Linings Playbook", 2012)
            };
            _context.ActorMovies.Add(silverCooper);

            //The Wolf Of Wall Street
            ActorMovie wolfDiCaprio = new ActorMovie
            {
                ActorId = FindActor("Leonardo", "Di Caprio").Id,
                Actor = FindActor("Leonardo", "Di Caprio"),
                MovieId = FindMovie("The Wolf of Wall Street", 2013).Id,
                Movie = FindMovie("The Wolf of Wall Street", 2013)
            };
            _context.ActorMovies.Add(wolfDiCaprio);

            ActorMovie wolfMcConaughey = new ActorMovie
            {
                ActorId = FindActor("Matthew", "McConaughey").Id,
                Actor = FindActor("Matthew", "McConaughey"),
                MovieId = FindMovie("The Wolf of Wall Street", 2013).Id,
                Movie = FindMovie("The Wolf of Wall Street", 2013)
            };
            _context.ActorMovies.Add(wolfMcConaughey);

            //Interstellar
            ActorMovie interstellarMcConaughey = new ActorMovie
            {
                ActorId = FindActor("Matthew", "McConaughey").Id,
                Actor = FindActor("Matthew", "McConaughey"),
                MovieId = FindMovie("Interstellar", 2014).Id,
                Movie = FindMovie("Interstellar", 2014)
            };
            _context.ActorMovies.Add(interstellarMcConaughey);

            //Once Upon A Time In Hollywood
            ActorMovie hollywoodDiCaprio = new ActorMovie
            {
                ActorId = FindActor("Leonardo", "Di Caprio").Id,
                Actor = FindActor("Leonardo", "Di Caprio"),
                MovieId = FindMovie("Once Upon a Time In Hollywood", 2019).Id,
                Movie = FindMovie("Once Upon a Time In Hollywood", 2019)
            };
            _context.ActorMovies.Add(hollywoodDiCaprio);

            ActorMovie hollywoodPitt = new ActorMovie
            {
                ActorId = FindActor("Brad", "Pitt").Id,
                Actor = FindActor("Brad", "Pitt"),
                MovieId = FindMovie("Once Upon a Time In Hollywood", 2019).Id,
                Movie = FindMovie("Once Upon a Time In Hollywood", 2019)
            };
            _context.ActorMovies.Add(hollywoodPitt);

            ActorMovie hollywoodRobbie = new ActorMovie
            {
                ActorId = FindActor("Margot", "Robbie").Id,
                Actor = FindActor("Margot", "Robbie"),
                MovieId = FindMovie("Once Upon a Time In Hollywood", 2019).Id,
                Movie = FindMovie("Once Upon a Time In Hollywood", 2019)
            };
            _context.ActorMovies.Add(hollywoodRobbie);

            //The Dark Knight
            ActorMovie darkKnightBale = new ActorMovie
            {
                ActorId = FindActor("Christian", "Bale").Id,
                Actor = FindActor("Christian", "Bale"),
                MovieId = FindMovie("The Dark Knight", 2008).Id,
                Movie = FindMovie("The Dark Knight", 2008)
            };
            _context.ActorMovies.Add(darkKnightBale);

            //Shutter Island
            ActorMovie shutterDiCaprio = new ActorMovie
            {
                ActorId = FindActor("Leonardo", "Di Caprio").Id,
                Actor = FindActor("Leonardo", "Di Caprio"),
                MovieId = FindMovie("Shutter Island", 2010).Id,
                Movie = FindMovie("Shutter Island", 2010)
            };
            _context.ActorMovies.Add(shutterDiCaprio);

            //Fight Club
            ActorMovie fightClubPitt = new ActorMovie
            {
                ActorId = FindActor("Brad", "Pitt").Id,
                Actor = FindActor("Brad", "Pitt"),
                MovieId = FindMovie("Fight Club", 1999).Id,
                Movie = FindMovie("Fight Club", 1999)
            };
            _context.ActorMovies.Add(fightClubPitt);

            //Zodiac
            ActorMovie zodiacGyllenhaal = new ActorMovie
            {
                ActorId = FindActor("Jake", "Gyllenhaal").Id,
                Actor = FindActor("Jake", "Gyllenhaal"),
                MovieId = FindMovie("Zodiac", 2007).Id,
                Movie = FindMovie("Zodiac", 2007)
            };
            _context.ActorMovies.Add(zodiacGyllenhaal);

            //The Audition
            ActorMovie auditionPitt = new ActorMovie
            {
                ActorId = FindActor("Brad", "Pitt").Id,
                Actor = FindActor("Brad", "Pitt"),
                MovieId = FindMovie("The Audition", 2015).Id,
                Movie = FindMovie("The Audition", 2015)
            };
            _context.ActorMovies.Add(auditionPitt);

            ActorMovie auditionDiCaprio = new ActorMovie
            {
                ActorId = FindActor("Leonardo", "Di Caprio").Id,
                Actor = FindActor("Leonardo", "Di Caprio"),
                MovieId = FindMovie("The Audition", 2015).Id,
                Movie = FindMovie("The Audition", 2015)
            };
            _context.ActorMovies.Add(auditionDiCaprio);

            //Se7en
            ActorMovie sevenPitt = new ActorMovie
            {
                ActorId = FindActor("Brad", "Pitt").Id,
                Actor = FindActor("Brad", "Pitt"),
                MovieId = FindMovie("Se7en", 1995).Id,
                Movie = FindMovie("Se7en", 1995)
            };
            _context.ActorMovies.Add(sevenPitt);

            _context.ActorMovies.AddRange(new List<ActorMovie>
            {
                americanHustleLawrence,
                americanHustleCooper,
                americanHustleBale,
                silverCooper,
                silverLawrence,
                interstellarMcConaughey,
                shutterDiCaprio,
                auditionDiCaprio,
                auditionPitt,
                zodiacGyllenhaal,
                wolfDiCaprio,
                wolfMcConaughey,
                hollywoodDiCaprio,
                hollywoodPitt,
                hollywoodRobbie,
                darkKnightBale,
                fightClubPitt,
                sevenPitt
            });

            //Favorites
            //=========

            Favorite favorite = new Favorite
            {
                UserId = FindUser("andreas.holvoet").Id,
                User = FindUser("andreas.holvoet"),
                MovieId = FindMovie("Once Upon a Time In Hollywood", 2019).Id,
                Movie = FindMovie("Once Upon a Time In Hollywood", 2019)
            };
            _context.Favorites.Add(favorite);

            Favorite favorite2 = new Favorite
            {
                UserId = FindUser("andreas.holvoet").Id,
                User = FindUser("andreas.holvoet"),
                MovieId = FindMovie("Se7en", 1995).Id,
                Movie = FindMovie("Se7en", 1995)
            };
            _context.Favorites.Add(favorite2);

            Favorite favorite3 = new Favorite
            {
                UserId = FindUser("thomas.verstraete").Id,
                User = FindUser("thomas.verstraete"),
                MovieId = FindMovie("American Hustle", 2013).Id,
                Movie = FindMovie("American Hustle", 2013)
            };
            _context.Favorites.Add(favorite3);

            Favorite favorite4 = new Favorite
            {
                UserId = FindUser("thomas.verstraete").Id,
                User = FindUser("thomas.verstraete"),
                MovieId = FindMovie("Se7en", 1995).Id,
                Movie = FindMovie("Se7en", 1995)
            };
            _context.Favorites.Add(favorite4);

            _context.Favorites.AddRange(new List<Favorite>
            {
                favorite,
                favorite2,
                favorite3,
                favorite4
            });

            //UserFollower
            //============

            UserFollower userFollower = new UserFollower
            {
                //thomas volgt andreas
                FollowingId = FindUser("andreas.holvoet").Id,
                Following = FindUser("andreas.holvoet"),
                FollowerId = FindUser("thomas.verstraete").Id,
                Follower = FindUser("thomas.verstraete")
            };

            _context.UserFollowers.Add(userFollower);

            _context.UserFollowers.AddRange(new List<UserFollower>
            {
                userFollower
            });

            _context.SaveChanges();

            //Reviews
            //=========
            Review review1 = new Review
            {
                Description = "Brilliant movie, absolutly loved it!",
                Date = DateTime.Now,
                Rating = 8,
                UserId = FindUser("thomas.verstraete").Id,
                User = FindUser("thomas.verstraete"),
                MovieId = FindMovie("American Hustle", 2013).Id,
                Movie = FindMovie("American Hustle", 2013)
            };
            _context.Reviews.Add(review1);

            Review review2 = new Review
            {
                Description = "Not my favorite, but still a very good effort.",
                Date = DateTime.Now,
                Rating = 6,
                UserId = FindUser("thomas.verstraete").Id,
                User = FindUser("thomas.verstraete"),
                MovieId = FindMovie("Fight Club", 1999).Id,
                Movie = FindMovie("Fight Club", 1999)
            };
            _context.Reviews.Add(review2);

            Review review3 = new Review
            {
                Description = "Absolute rubbish, how did this win an oscar?",
                Date = DateTime.Now,
                Rating = 2,
                UserId = FindUser("guest@moviemind.com").Id,
                User = FindUser("guest@moviemind.com"),
                MovieId = FindMovie("Fight Club", 1999).Id,
                Movie = FindMovie("Fight Club", 1999)
            };
            _context.Reviews.Add(review3);

            Review review4 = new Review
            {
                Description = "How many times can one man curse?",
                Date = DateTime.Now,
                Rating = 7,
                UserId = FindUser("guest@moviemind.com").Id,
                User = FindUser("guest@moviemind.com"),
                MovieId = FindMovie("The Wolf of Wall Street", 2013).Id,
                Movie = FindMovie("The Wolf of Wall Street", 2013)
            };
            _context.Reviews.Add(review4);

            Review review5 = new Review
            {
                Description = "Best movie made, ever",
                Date = DateTime.Now,
                Rating = 10,
                UserId = FindUser("admin@moviemind.com").Id,
                User = FindUser("admin@moviemind.com"),
                MovieId = FindMovie("The Dark Knight", 2008).Id,
                Movie = FindMovie("The Dark Knight", 2008)
            };
            _context.Reviews.Add(review5);

            _context.SaveChanges();
        }

        //Helper functions
        //================

        private static Director FindDirector(string fname, string lname)
        {
            return _context.Directors.Where(x => x.FirstName == fname && x.LastName == lname).Select(x => x).FirstOrDefault();
        }

        private static Genre FindGenre(string name)
        {
            return _context.Genres.Where(x => x.Name == name).Select(x => x).FirstOrDefault();
        }

        private static Actor FindActor(string fname, string lname)
        {
            return _context.Actors.Where(x => x.FirstName == fname && x.LastName == lname).Select(x => x).FirstOrDefault();
        }

        private static Movie FindMovie(string name, int year)
        {
            return _context.Movies.Where(x => x.Name == name && x.Year == year).Select(x => x).FirstOrDefault();
        }

        private static User FindUser(string userName)
        {
            return _context.Users.Where(x => x.UserName == userName).Select(x => x).FirstOrDefault();
        }

    }
}
