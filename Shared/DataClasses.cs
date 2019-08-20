using System;
using System.Collections.Generic;

namespace Shared {
    public class Person {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
        public void Write() => Console.WriteLine($"LastName: {LastName}, FirstName: {FirstName}");
    }

    public class Trip {
        public long Budget { get; set; }
    }

    public static class Globals {
        public static readonly List<Person> PersonList = new List<Person>() {
            {1,"Sonnnie","Akenhead","sakenhead0@odnoklassniki.ru","1941-03-14T17:16:42Z"},
            {2,"Cynthie","Mellonby","cmellonby1@blogtalkradio.com","2007-09-14T01:31:24Z"},
            {3,"Pansy","Spirritt","pspirritt2@skyrock.com","1987-02-03T22:46:35Z"},
            {4,"Elisha","Le Prevost","eleprevost3@aol.com","1916-09-02T03:50:10Z"},
            {5,"Hodge","Vigar","hvigar4@mozilla.org","1918-04-15T12:32:44Z"},
            {6,"Alva","Dei","adei5@google.fr","1943-03-10T17:03:27Z"},
            {7,"Cammi","Banister","cbanister6@xing.com","2013-07-22T09:36:02Z"},
            {8,"Edythe","Shiers","eshiers7@zimbio.com","2008-12-25T11:58:48Z"},
            {9,"Darci","Pfeffer","dpfeffer8@wix.com","2002-02-12T17:10:36Z"},
            {10,"Diana","Pierrepoint","dpierrepoint9@hugedomains.com","1971-11-24T07:06:14Z"},
            {11,"Xena","Lorriman","xlorrimana@phpbb.com","1977-09-06T21:53:08Z"},
            {12,"Alaine","Pippard","apippardb@nps.gov","2010-09-10T23:15:49Z"},
            {13,"Nelle","Beer","nbeerc@github.com","1903-02-12T08:58:05Z"},
            {14,"Coop","Matterson","cmattersond@ask.com","1957-04-01T11:47:50Z"},
            {15,"Mirella","Moreside","mmoresidee@simplemachines.org","1948-07-19T12:39:19Z"},
            {16,"Glen","Kach","gkachf@networksolutions.com","1962-04-15T23:47:57Z"},
            {17,"Logan","Ubsdell","lubsdellg@scribd.com","1931-02-17T10:19:41Z"},
            {18,"Val","Szubert","vszuberth@wunderground.com","1918-03-23T21:42:46Z"},
            {19,"Major","Peto","mpetoi@t.co","1913-02-18T22:48:14Z"},
            {20,"Jemmie","Hammatt","jhammattj@1und1.de","1963-11-05T20:15:47Z"},
            {21,"Philbert","Crudge","pcrudgek@networkadvertising.org","1930-01-25T07:33:49Z"},
            {22,"Kara-lynn","Olyonov","kolyonovl@themeforest.net","2019-05-26T08:39:08Z"},
            {23,"Goldarina","De Lorenzo","gdelorenzom@amazon.co.jp","1923-08-31T10:38:12Z"},
            {24,"Chaddie","Slack","cslackn@cnet.com","1908-02-06T13:47:21Z"},
            {25,"Vaughn","Brisset","vbrisseto@devhub.com","1980-06-04T08:08:38Z"},
            {26,"Bartholomeo","McCarrell","bmccarrellp@posterous.com","1958-07-09T22:45:33Z"},
            {27,"Xylina","Dinsdale","xdinsdaleq@hubpages.com","1954-09-11T03:51:32Z"},
            {28,"Riane","Lavell","rlavellr@examiner.com","1934-06-04T09:58:10Z"},
            {29,"Latisha","Donnan","ldonnans@upenn.edu","1971-07-22T10:24:04Z"},
            {30,"Cassaundra","Keppe","ckeppet@sakura.ne.jp","1935-06-29T02:12:20Z"},
            {31,"Ave","Hooban","ahoobanu@vk.com","1941-10-04T16:30:48Z"},
            {32,"Rosalie","Bergeau","rbergeauv@umich.edu","1915-03-02T09:51:28Z"},
            {33,"Elissa","Epton","eeptonw@zdnet.com","2009-06-02T16:05:33Z"},
            {34,"Mommy","Iannitti","miannittix@printfriendly.com","1956-07-03T15:46:56Z"},
            {35,"Conni","Blais","cblaisy@flickr.com","1993-01-29T08:48:11Z"},
            {36,"Basile","Fusco","bfuscoz@bizjournals.com","1976-02-17T14:03:16Z"},
            {37,"Patrica","Summerlee","psummerlee10@nsw.gov.au","1943-09-28T18:21:40Z"},
            {38,"Lovell","Hawker","lhawker11@ehow.com","1996-12-18T01:25:27Z"},
            {39,"Thorpe","Handling","thandling12@smugmug.com","1933-07-23T02:35:28Z"},
            {40,"Aloysia","Bamber","abamber13@issuu.com","1901-09-24T10:39:34Z"},
            {41,"Veronique","Agron","vagron14@blogger.com","1924-01-14T06:48:21Z"},
            {42,"Carmel","Gever","cgever15@mit.edu","1998-12-04T11:14:24Z"},
            {43,"Netty","Glenton","nglenton16@statcounter.com","1954-09-27T18:51:29Z"},
            {44,"Adolphus","Pyer","apyer17@unc.edu","1980-04-16T12:42:38Z"},
            {45,"Manon","Emanuelli","memanuelli18@goo.gl","1907-02-14T19:34:22Z"},
            {46,"Bartholomeus","Balleine","bballeine19@google.cn","1958-03-15T08:58:34Z"},
            {47,"Berny","O'Coskerry","bocoskerry1a@alibaba.com","2011-11-07T06:32:38Z"},
            {48,"Syman","Carlesi","scarlesi1b@kickstarter.com","1987-10-23T06:40:16Z"},
            {49,"Codie","Llewhellin","cllewhellin1c@acquirethisname.com","2004-05-11T01:09:43Z"},
            {50,"Sansone","Raymen","sraymen1d@slashdot.org","1928-04-07T08:12:31Z"},
            {51,"Marguerite","Ledner","mledner1e@about.me","1984-01-02T20:14:15Z"},
            {52,"Shayla","Lugard","slugard1f@e-recht24.de","1998-08-24T18:05:23Z"},
            {53,"Idalia","Minney","iminney1g@moonfruit.com","1951-11-27T20:26:20Z"},
            {54,"Huberto","Vawton","hvawton1h@huffingtonpost.com","1914-05-09T06:25:00Z"},
            {55,"Mayer","Hutcheon","mhutcheon1i@trellian.com","1937-04-25T10:29:52Z"},
            {56,"Ambrosi","Behan","abehan1j@1688.com","2007-11-28T00:05:00Z"},
            {57,"Mose","Estcot","mestcot1k@sciencedaily.com","1948-01-30T08:37:44Z"},
            {58,"Francklyn","Lamperd","flamperd1l@unblog.fr","1913-10-26T08:49:39Z"},
            {59,"Paulie","Tilby","ptilby1m@illinois.edu","1913-12-12T17:43:44Z"},
            {60,"Web","Padley","wpadley1n@mail.ru","1953-01-11T17:29:42Z"},
            {61,"Cristobal","Primarolo","cprimarolo1o@hp.com","1983-06-24T12:14:02Z"},
            {62,"Charles","Orcas","corcas1p@army.mil","1999-11-28T18:45:39Z"},
            {63,"Lance","Mountford","lmountford1q@smh.com.au","1902-04-02T14:56:01Z"},
            {64,"Frederich","Hickeringill","fhickeringill1r@google.com","1933-06-11T05:12:39Z"},
            {65,"Amalea","Blyth","ablyth1s@paypal.com","1919-12-13T16:52:05Z"},
            {66,"Benedicta","Dicte","bdicte1t@storify.com","2005-09-27T21:02:49Z"},
            {67,"Edgar","Swinden","eswinden1u@bandcamp.com","2008-10-20T18:28:00Z"},
            {68,"Devonne","Gumbley","dgumbley1v@guardian.co.uk","1917-04-28T16:12:27Z"},
            {69,"Rafferty","Parram","rparram1w@webs.com","1986-03-29T15:00:03Z"},
            {70,"Theresa","Pignon","tpignon1x@feedburner.com","2003-10-26T22:28:16Z"},
            {71,"Charlean","MacKowle","cmackowle1y@pcworld.com","1907-03-07T22:48:02Z"},
            {72,"Amil","Dehn","adehn1z@sciencedaily.com","1944-12-04T17:20:25Z"},
            {73,"Collette","Bresland","cbresland20@google.com.br","2012-07-22T03:36:04Z"},
            {74,"Kingsly","Ramelet","kramelet21@seattletimes.com","1978-05-26T05:46:05Z"},
            {75,"Sigfrid","Benley","sbenley22@marriott.com","1912-02-26T17:32:45Z"},
            {76,"Jsandye","Van der Hoeven","jvanderhoeven23@addtoany.com","1901-12-12T11:37:50Z"},
            {77,"Renae","Sheaber","rsheaber24@woothemes.com","1924-07-13T13:54:58Z"},
            {78,"Marice","Petrol","mpetrol25@tinyurl.com","1954-08-02T17:18:30Z"},
            {79,"Karlotta","Owen","kowen26@wired.com","1901-11-20T17:58:51Z"},
            {80,"Town","Phripp","tphripp27@instagram.com","1918-06-08T06:35:18Z"},
            {81,"Ardelle","Calderonello","acalderonello28@typepad.com","1973-09-15T05:14:49Z"},
            {82,"Sanderson","Gurner","sgurner29@theguardian.com","1941-06-17T22:15:41Z"},
            {83,"Dallas","Youtead","dyoutead2a@sfgate.com","1974-07-06T07:08:21Z"},
            {84,"Anthe","Dumbleton","adumbleton2b@admin.ch","2000-04-21T18:27:43Z"},
            {85,"Torr","Roddie","troddie2c@uol.com.br","1951-09-21T02:35:02Z"},
            {86,"Anallese","Devote","adevote2d@cnbc.com","1959-07-16T01:19:28Z"},
            {87,"Iago","Jagg","ijagg2e@vistaprint.com","2017-12-26T14:29:35Z"},
            {88,"Sonni","Aers","saers2f@altervista.org","1942-01-14T17:58:47Z"},
            {89,"Hyman","Annes","hannes2g@hp.com","1982-05-27T14:26:36Z"},
            {90,"Norine","Arnald","narnald2h@digg.com","1934-11-28T06:05:17Z"},
            {91,"Peggie","Buckthorpe","pbuckthorpe2i@ow.ly","1965-10-05T00:14:03Z"},
            {92,"Leona","Osgar","losgar2j@paypal.com","2018-02-23T22:59:18Z"},
            {93,"Malachi","Treversh","mtreversh2k@mit.edu","1939-09-06T12:11:37Z"},
            {94,"Walt","Burras","wburras2l@biblegateway.com","1963-09-20T06:52:05Z"},
            {95,"Huberto","Allmark","hallmark2m@hubpages.com","1917-12-05T12:22:09Z"},
            {96,"Lexy","Cornelleau","lcornelleau2n@sakura.ne.jp","1997-06-30T14:45:23Z"},
            {97,"Piper","Lasseter","plasseter2o@imageshack.us","1940-12-11T02:15:34Z"},
            {98,"Reg","Tregust","rtregust2p@cloudflare.com","1952-07-21T12:45:18Z"},
            {99,"Pauli","Crowd","pcrowd2q@over-blog.com","1944-04-08T22:22:38Z"},
            {100,"Emelda","Sutherington","esutherington2r@dropbox.com","1902-02-07T23:35:06Z"}
        };
    }
}
