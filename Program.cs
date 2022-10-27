using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SchoolAbsence
{
    struct Absence
    {
        public int month;
        public int day;
        public string firstname;
        public string surname;
        public string absence;

        public Absence(int month, int day, string surname, string firstname, string absence)
        {
            this.month = month;
            this.day = day;
            this.firstname = firstname;
            this.surname = surname;
            this.absence = absence;
        }
    }
    class SchoolAbsence
    {

        /*
            The txt file looks like this:

            # 01 15                             date
            Galagonya Alfonz OXXXXXN            each latter is a lesson O=attended X=authorized absence I=Absent N=No lesson (by authorized absent the task means that the student had official paper of his absence.)
            # 01 16 
            Alma Hedvig OOOOOIO 
            Galagonya Alfonz XXXXXXX
            .....
            ....

            
        */
        static List<Absence> AbsenceList = new List<Absence>();

        //Task 1: Read and store the data of naplo.txt
        static void Feladat1()
        {
            StreamReader sr = new StreamReader("naplo.txt");
            int month = 0;
            int day = 0;
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split();

                if (line[0] == "#")
                {
                    month = int.Parse(line[1]);
                    day = int.Parse(line[2]);
                }
                else
                {
                    string surname = line[0];
                    string firstname = line[1];
                    string absence = line[2];

                    Absence item = new Absence(month, day, surname, firstname, absence);
                    AbsenceList.Add(item);
                }
            }

            sr.Close();
        }

        //Task 2: Count how many entries the list has
        static void Feladat2()
        {
            Console.WriteLine("Task 2");
            Console.WriteLine($"The list has {AbsenceList.Count} entries");
        }

        //Task 3: Count how many authorized and unauthorized absences are recorded
        static void Feladat3()
        {
            Console.WriteLine("3. feladat");
            int authorizedAbsence = 0; //X
            int unauthorizedAbsance = 0; //I

            foreach (Absence sor in AbsenceList)
            {
                for (int i = 0; i < sor.absence.Length; i++)
                {
                    if (sor.absence[i] == 'X')
                    {
                        authorizedAbsence++;
                    }
                    else if (sor.absence[i] == 'I')
                    {
                        unauthorizedAbsance++;
                    }
                }
            }
            Console.WriteLine($"\tNumber of authorized absence: {authorizedAbsence}\n\tNumber of unauthorized absance: {unauthorizedAbsance}");
        }

        //Task 4: Make a method that takes two argument a month and a day and returns what day of the week it was
        static string hetnapja(int month, int day)
        {
            string[] days = { "monday", "tuesday", "wdnesday", "thursday", "friday", "saturday", "sunday" };
            int[] months = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 335 };

            int dayOfTheWeek = (months[month - 1] + day) % 7;
            return days[dayOfTheWeek];
        }

        //Task 5: Ask the user to type in a date and use the method made in the previous task to print what day of the week was on that day
        static void Feladat5()
        {
            Console.WriteLine("Task 5");

            Console.Write("Month (1-12)= ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Day (1-31)= ");
            int day = int.Parse(Console.ReadLine());

            Console.WriteLine($"It was {hetnapja(month, day)}.");
        }

        //Task 6: Ask the user for a day of the week and a lesson. Print how many students were absent on that lesson throughout the half year
        static void Feladat6()
        {
            Console.WriteLine("Task 4");

            Console.Write("Day of the week (monday-friday)= ");
            string day = Console.ReadLine();
            Console.Write("lesson (1-6)= ");
            int ora = int.Parse(Console.ReadLine());

            int absence = 0;

            foreach (Absence sor in AbsenceList)
            {
                if (hetnapja(sor.month, sor.day) == day.ToLower() && sor.absence[ora - 1] == 'X' || sor.absence[ora - 1] == 'I')
                {
                    absence++;
                }
            }
            Console.WriteLine($"Throughout the half year there were {absence} absences ");
        }

        //Task 7: Print the student with the most absences, if there is more print all with spcae between them.
        static void Feladat7()
        {
            Console.WriteLine("Task 7");
            List<string> students = new List<string>();
            foreach (Absence sor in AbsenceList)
            {
                string name = sor.surname + " " + sor.firstname;
                if (!students.Contains(name))
                    students.Add(name);
            }

            int counter;
            int max = 0;
            List<string> mostAbsences = new List<string>();

            foreach (string name in students)
            {
                counter = 0;
                foreach (Absence sor in AbsenceList)
                {
                    string teljesnev = sor.surname + " " + sor.firstname;

                    if (name == teljesnev)
                    {
                        for (int i = 0; i < sor.absence.Length; i++)
                        {
                            if (sor.absence[i] == 'X' || sor.absence[i] == 'I')
                                counter++;
                        }
                    }
                }

                if (counter > max)
                {
                    max = counter;
                    mostAbsences = new List<string>();
                    mostAbsences.Add(name);
                }
                else if (counter == max)
                {

                    mostAbsences.Add(name);
                }
            }
            Console.Write("Students with the most absences: ");
            foreach (string name in mostAbsences)
            {
                Console.Write($"{name}, ");
            }

        }
        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat5();
            Feladat6();
            Feladat7();
            Console.ReadKey();
        }
    }
}
