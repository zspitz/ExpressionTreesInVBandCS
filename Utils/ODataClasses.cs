﻿using System;
using System.Collections.Generic;

namespace Utils {
    public class Person {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
        public void Write() => Console.WriteLine($"LastName: {LastName}, FirstName: {FirstName}");
    }

    public class Trip {
        public long Budget { get; set; }
    }

    public static class Globals {
        public static readonly List<Person> PersonList = new List<Person>() {
            { 1, "Wyn", "Baalham" },
            { 2, "Catherine", "Alonso" },
            { 3, "Marie-jeanne", "Schoroder" },
            { 4, "Elisa", "Airton" },
            { 5, "Keelia", "McCool" },
            { 6, "Ania", "Balog" },
            { 7, "Gilberto", "Wycliffe" },
            { 8, "Lyon", "Grinnell" },
            { 9, "Bord", "Tickner" },
            { 10, "Toddy", "Teall" },
            { 11, "Dido", "Rispin" },
            { 12, "Betta", "Ferrant" },
            { 13, "Patty", "Yerlett" },
            { 14, "Erna", "Bertenshaw" },
            { 15, "Tripp", "Wardingly" },
            { 16, "Udale", "Darby" },
            { 17, "Ernst", "Breffitt" },
            { 18, "Matty", "Oles" },
            { 19, "Cara", "Correa" },
            { 20, "Horace", "Inchbald" },
            { 21, "Ailbert", "Sherratt" },
            { 22, "Katrinka", "Crowdace" },
            { 23, "Roda", "Hartup" },
            { 24, "Blanch", "Soonhouse" },
            { 25, "Pamela", "Becken" },
            { 26, "Atlanta", "Heggman" },
            { 27, "Sam", "Teare" },
            { 28, "Brien", "Buckel" },
            { 29, "Pattie", "Coppo" },
            { 30, "Doria", "Huband" },
            { 31, "Lacie", "Nowick" },
            { 32, "Ebeneser", "Garrad" },
            { 33, "Artemas", "Theobalds" },
            { 34, "Korella", "Zini" },
            { 35, "Sheela", "Batting" },
            { 36, "Rog", "Ogley" },
            { 37, "Sharona", "Kiggel" },
            { 38, "Anastasie", "Gionettitti" },
            { 39, "Cherilyn", "Bengle" },
            { 40, "Winni", "Grotty" },
            { 41, "Bird", "Vial" },
            { 42, "Humberto", "Ronca" },
            { 43, "Ebba", "McAster" },
            { 44, "Margo", "Wyke" },
            { 45, "Anastasie", "Alessandrini" },
            { 46, "Halimeda", "Ratazzi" },
            { 47, "Weidar", "Jamot" },
            { 48, "Bren", "McGuffog" },
            { 49, "Constantin", "Forten" },
            { 50, "Fax", "Tankin" },
            { 51, "King", "Paddeley" },
            { 52, "Odetta", "Poles" },
            { 53, "Borg", "Bramham" },
            { 54, "Brianna", "Dimitrescu" },
            { 55, "Holt", "Pilmore" },
            { 56, "Rosalind", "Senner" },
            { 57, "Jobie", "Persent" },
            { 58, "Peyter", "Becker" },
            { 59, "Malina", "Spavins" },
            { 60, "Tamarra", "Brill" },
            { 61, "Jorie", "Eefting" },
            { 62, "Vick", "Castille" },
            { 63, "Linet", "Brien" },
            { 64, "Henrie", "Gooly" },
            { 65, "Suzanne", "Cressingham" },
            { 66, "Lenee", "Britten" },
            { 67, "Felipe", "Coggan" },
            { 68, "Mark", "Asplin" },
            { 69, "Brigg", "Schimaschke" },
            { 70, "Thedric", "Roubottom" },
            { 71, "Petra", "Bennett" },
            { 72, "Shawna", "Abbatini" },
            { 73, "Lucien", "Strowger" },
            { 74, "Christie", "Dunan" },
            { 75, "Rik", "Winchurst" },
            { 76, "Leonardo", "Decruse" },
            { 77, "Israel", "Gartenfeld" },
            { 78, "Pryce", "Ghelardoni" },
            { 79, "Bev", "Sansum" },
            { 80, "Cyb", "Dole" },
            { 81, "Gayel", "Dugan" },
            { 82, "Hieronymus", "Brunner" },
            { 83, "Jaime", "Ramet" },
            { 84, "Gil", "Coxhell" },
            { 85, "Sile", "Burkin" },
            { 86, "Bogart", "Dufoure" },
            { 87, "Jacquie", "Lamprey" },
            { 88, "Larissa", "Bowles" },
            { 89, "Dexter", "Dobbs" },
            { 90, "Johnnie", "Antcliffe" },
            { 91, "Earle", "Raynham" },
            { 92, "Bunni", "Mc Dermid" },
            { 93, "Tonia", "Aubin" },
            { 94, "Timi", "Chinge" },
            { 95, "Morty", "Kitcatt" },
            { 96, "Leeland", "Greatbach" },
            { 97, "Keith", "Hards" },
            { 98, "Tremain", "Brocklebank" },
            { 99, "Gasparo", "Ohanessian" },
            { 100, "Augustus", "Ferris" }
        };
    }
}
