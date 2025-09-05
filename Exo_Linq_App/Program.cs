using Exo_Linq_Context;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();
List<Student> students = context.Students;
List<Grade> grades = context.Grades;
List<Professor> professors = context.Professors;
List<Section> sections = context.Sections;


#region 1 Opérateur « Select »
#region 1.1
//Exercice 1.1 Ecrire une requête pour présenter, pour chaque étudiant, le nom de l’étudiant, la date de naissance, le login et le résultat pour l’année de l’ensemble des étudiants.
var result1 = from s in students
              select new
              {
                  nom = s.Last_Name,
                  dateNaissance = s.BirthDate,
                  login = s.Login,
                  resultatAnnuel = s.Year_Result
              };

var result1_bis = students.Select(s => new
{
    nom = s.Last_Name,
    dateNaissance = s.BirthDate,
    login = s.Login,
    resultatAnnuel = s.Year_Result
});

#endregion

#region 1.2
//Exercice 1.2 Ecrire une requête pour présenter, pour chaque étudiant, son nom complet (nom et prénom séparés par un espace), son id et sa date de naissance.
var result2 = from s in students
                  //select new { NomComplet = s.Last_Name + " " + s.First_Name }
              select new
              {
                  NomComplet = $"{s.Last_Name} {s.First_Name}",
                  Id = s.Student_ID,
                  DateNaissance = s.BirthDate
              };

var result2_bis = students.Select(s => new
{
    NomComplet = $"{s.Last_Name} {s.First_Name}",
    Id = s.Student_ID,
    DateNaissance = s.BirthDate
});

//foreach (var student in result2) {
//    Console.WriteLine(student.NomComplet);
//}

#endregion

#region 1.3
//Exercice 1.3 Ecrire une requête pour présenter, pour chaque étudiant, dans une seule chaine de caractère l’ensemble des données relatives à un étudiant séparées par le symbole |.
IEnumerable<string> result3 = from s in students
                                  //select s.Student_ID + "|" + s.First_Name + "|" // on fait toutes les props...
                              select $"{s.Student_ID}|{s.First_Name}|{s.BirthDate:yyyy-MM-dd}|{s.Login}|{s.Section_ID}|{s.Year_Result}|{s.Course_ID}";

IEnumerable<string> result3_bis = students.Select(s => $"{s.Student_ID}|{s.First_Name}|{s.BirthDate:yyyy-MM-dd}|{s.Login}|{s.Section_ID}|{s.Year_Result}|{s.Course_ID}");

//foreach (string s in result3)
//{
//    Console.WriteLine(s);
//}
#endregion
#endregion

#region 2 Opérateurs « Where » et « OrderBy »

#region 2.1
/* Exercice 2.1 Pour chaque étudiant né avant 1955, donner le nom, le résultat annuel et le statut.
Le statut prend la valeur « OK » si l’étudiant à obtenu au moins 12 comme résultat annuel
et « KO » dans le cas contraire. */

var result2_1 = from s in students
                where s.BirthDate.Year < 1955
                select new
                {
                    Nom = s.Last_Name,
                    ResultatAnnuel = s.Year_Result,
                    Status = s.Year_Result >= 12 ? "OK" : "KO"
                };

var result2_1_bis = students.Where(s => s.BirthDate.Year < 1955)
                            .Select(s => new
                            {
                                Nom = s.Last_Name,
                                ResultatAnnuel = s.Year_Result,
                                Status = s.Year_Result >= 12 ? "OK" : "KO"
                            });

//foreach (var rs in result2_1_bis)
//{
//    Console.WriteLine($"{rs.Nom} - {rs.ResultatAnnuel} - {rs.Status}");
//}
#endregion

#region 2.2
/*Exercice 2.2 Donner pour chaque étudiant entre 1955 et 1965 le nom, le résultat annuel et la
catégorie à laquelle il appartient. La catégorie est en fonction du résultat annuel obtenu ; un
résultat inférieur à 10 appartient à la catégorie « inférieure », un résultat égal à 10 appartient
à la catégorie « neutre », un résultat autre appartient à la catégorie « supérieure ».*/

string ComputeStudentCategory(Student s)
{
    if (s.Year_Result < 10)
    {
        return "inférieur";
    }
    else if (s.Year_Result == 10)
    {
        return "neutre";
    }
    else
    {
        return "supérieur";
    }
}

var result2_2 = from s in students
                where s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965
                select new
                {
                    last_name = s.Last_Name,
                    year_result = s.Year_Result,
                    //Categorie = s.Year_Result < 10 ? "inférieur" :
                    //                s.Year_Result == 10 ? "neutre" : "supérieur"
                    Categorie = ComputeStudentCategory(s)
                };


var result2_2_bis = students.Where(s => s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965)
                            .Select(s => new
                            {
                                last_name = s.Last_Name,
                                year_result = s.Year_Result,
                                //Categorie = s.Year_Result < 10 ? "inférieur" :
                                //                s.Year_Result == 10 ? "neutre" : "supérieur"
                                Categorie = ComputeStudentCategory(s)
                            });

//foreach (var rs in result2_2) {
//    Console.WriteLine($"{rs.last_name} - {rs.year_result} - {rs.Categorie}");
//}
#endregion

#region 2.3
/*Ecrire une requête pour présenter le nom, l’id de section et de tous les étudiants
qui ont un nom de famille qui termine par r.*/

var result2_3 = from s in students
                where s.Last_Name.EndsWith("r")
                select new
                {
                    Nom = s.Last_Name,
                    Id = s.Student_ID,
                };

var result2_3_bis = students.Where(s => s.Last_Name.EndsWith("r"))
                            .Select(s => new
                            {
                                Nom = s.Last_Name,
                                Id = s.Student_ID
                            });

//foreach (var rs in result2_3)
//{
//    Console.WriteLine($"{rs.Nom} - {rs.Id}");
//}
#endregion

#region 2.4
/*Exercice 2.4 Ecrire une requête pour présenter le nom et le résultat annuel classé par résultats
annuels décroissant de tous les étudiants qui ont obtenu un résultat annuel inférieur ou égal
à 3.*/

var result2_4 = from s in students
                where s.Year_Result <= 3
                orderby s.Year_Result descending
                select new
                {
                    Nom = s.Last_Name,
                    ResultatAnnuel = s.Year_Result
                };

var result2_4_bis = students.Where(s => s.Year_Result <= 3)
                            .OrderByDescending(s => s.Year_Result)
                            .Select(s => new
                            {
                                Nom = s.Last_Name,
                                ResultatAnnuel = s.Year_Result
                            });

var result2_4_bis_2 = students.Where(s => s.Year_Result <= 3)
                                .Select(s => new
                                {
                                    Nom = s.Last_Name,
                                    ResultatAnnuel = s.Year_Result
                                })
                                .OrderByDescending(s => s.ResultatAnnuel); // Attention OrderByDescending se base sur l'objet crée par le Select

//foreach (var rs in result2_4) {
//    Console.WriteLine($"{rs.Nom} - {rs.ResultatAnnuel}");
//}

#endregion

#region 2.5

/*Exercice 2.5 Ecrire une requête pour présenter le nom complet (nom et prénom séparés par un
espace) et le résultat annuel classé par nom croissant sur le nom de tous les étudiants
appartenant à la section 1110.*/

var result2_5 = from s in students
                where s.Section_ID == 1110
                orderby s.Last_Name
                select new
                {
                    Full_Name = $"{s.Last_Name} {s.First_Name}",
                    year_result = s.Year_Result
                };

var result2_5_bis = students.Where(s => s.Section_ID == 1110)
                            .OrderBy(s => s.Last_Name)
                            .Select(s => new
                            {
                                Full_Name = $"{s.Last_Name} {s.First_Name}",
                                year_result = s.Year_Result
                            });

//foreach (var rs in result2_5) {
//    Console.WriteLine($"{rs.Full_Name} - {rs.year_result}");
//}
#endregion

#region 2.6

/*Exercice 2.6 Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel
classé par ordre croissant sur la section de tous les étudiants appartenant aux sections 1010
et 1020 ayant un résultat annuel qui n’est pas compris entre 12 et 18.*/

var result2_6 = from s in students
                where (s.Section_ID == 1010 || s.Section_ID == 1020) &&
                        (s.Year_Result < 12 || s.Year_Result > 18)
                orderby s.Section_ID
                select new
                {
                    last_name = s.Last_Name,
                    section_id = s.Section_ID,
                    year_result = s.Year_Result
                };

var result2_6_bis = students.Where(s => (s.Section_ID == 1010 || s.Section_ID == 1020) &&
                        (s.Year_Result < 12 || s.Year_Result > 18))
                            .OrderBy(s => s.Student_ID)
                            .Select(s => new
                            {
                                last_name = s.Last_Name,
                                section_id = s.Section_ID,
                                year_result = s.Year_Result
                            });

//foreach(var rs in result2_6)
//{
//    Console.WriteLine($"{rs.last_name} - {rs.section_id} - {rs.year_result}");
//}
#endregion

#region 2.7

/* Exercice 2.7 Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel sur
100 (nommer la colonne ‘result_100’) classé par ordre décroissant du résultat de tous les
étudiants appartenant aux sections commençant par 13 et ayant un résultat annuel sur 100
inférieur ou égal à 60. */

var result2_7 = from s in students
                where s.Section_ID.ToString().StartsWith("13")
                    && s.Year_Result * 5 < 60
                orderby s.Year_Result descending
                select new
                {
                    last_name = s.Last_Name,
                    section_id = s.Section_ID,
                    result_100 = s.Year_Result * 5
                };

var result2_7_bis = students.Where(s => s.Section_ID.ToString().StartsWith("13")
                    && s.Year_Result * 5 < 60)
                            .OrderByDescending(s => s.Year_Result)
                            .Select(s => new
                            {
                                last_name = s.Last_Name,
                                section_id = s.Section_ID,
                                result_100 = s.Year_Result * 5
                            });

//foreach (var rs in result2_7) {
//    Console.WriteLine($"{rs.last_name} - {rs.section_id} - {rs.result_100}");
//}
#endregion

#endregion

#region 3 Opérateurs « Count », « Min », « Max », « Sum » et « Average »

#region 3.1

/* Exercice 3.1 Donner le résultat annuel moyen pour l’ensemble des étudiants. */

double resultatMoyen = (from s in students
                        select s.Year_Result) // [10, 4, 12, 3,...] tableau qui contient les year_result
                       .Average();

double resultatMoyen_bis = students.Average(s => s.Year_Result);

Console.WriteLine("3.1 - " + resultatMoyen);

#endregion

#region 3.2

/* Exercice 3.2 Donner le plus haut résultat annuel obtenu par un étudiant. */
int resultatLePlusHaut = (from s in students
                          select s.Year_Result) // [10, 4, 12, 3,...] tableau qui contient les year_result
                          .Max();

int resultatLePlusHaut_bis = students.Max(s => s.Year_Result);
Console.WriteLine("3.2 - " + resultatLePlusHaut_bis);

#endregion

#region 3.3

/* Exercice 3.3 Donner la somme des résultats annuels. */
int resultatsomme = (from s in students
                     select s.Year_Result) // [10, 4, 12, 3,...] tableau qui contient les year_result
                        .Sum();

int resultatsomme_bis = students.Sum(s => s.Year_Result);
Console.WriteLine("3.3 - " + resultatsomme);

#endregion

#region 3.4 

/* Exercice 3.4 Donner le résultat annuel le plus faible. */
int resultatLePlusBas = (from s in students
                         select s.Year_Result) // [10, 4, 12, 3,...] tableau qui contient les year_result
                          .Min();

int resultatLePlusBas_bis = students.Min(s => s.Year_Result);
Console.WriteLine("3.4 - " + resultatLePlusBas_bis);
#endregion

#region 3.5

/* Exercice 3.5 Donner le nombre de lignes qui composent la séquence « Students » ayant obtenu
un résultat annuel impair. */

int nbrDeYearResultImpair = (from s in students
                             where s.Year_Result % 2 != 0
                             select s.Year_Result)
                            .Count();

int nbrDeYearResultImpair_bis = students.Count(s => s.Year_Result % 2 != 0);
Console.WriteLine("3.5 - " + nbrDeYearResultImpair);
#endregion

#endregion

#region 4 Opérateurs « GroupBy », « Join » et « GroupJoin »

#region 4.1
/* Exercice 4.1 Donner pour chaque section, le résultat maximum (« Max_Result ») obtenu par les
étudiants. */

var result4_1 = context.Students.GroupBy(st => st.Section_ID)
                                .Select(grp => new
                                {
                                    SectionId = grp.Key,
                                    Max_Result = grp.Max(st => st.Year_Result)
                                });

var result4_1bis = from st in context.Students
                   group st by st.Section_ID into grp
                   select new
                   {
                       SectionId = grp.Key,
                       Max_Result = grp.Max(st => st.Year_Result)
                   };

#endregion

#region 4.2
/* Exercice 4.2 Donner pour toutes les sections commençant par 10, le résultat annuel moyen
(« AVGResult ») obtenu par les étudiants. */

var result4_2 = from s in sections
                join st in students on s.Section_ID equals st.Section_ID into gStudents
                select new
                {
                    Section_ID = s.Section_ID,
                    Section_Name = s.Section_Name,
                    Section_Delegate_ID = s.Delegate_ID,
                    Moyenne = gStudents.Average(s => s.Year_Result),
                };

var result4_2_bis = context.Students.Where(st => st.Section_ID.ToString().StartsWith("10"))
                                .GroupBy(st => st.Section_ID)
                                .Select(grp => new
                                {
                                    SectionId = grp.Key,
                                    AVGResult = grp.Average(st => st.Year_Result)
                                });
#endregion

#region 4.3

/* Exercice 4.3 Donner le résultat moyen (« AVGResult ») et le mois en chiffre (« BirthMonth »)
pour les étudiants né le même mois entre 1970 et 1985. */

var result4_3 = context.Students.Where(st => st.BirthDate.Year >= 1970 && st.BirthDate.Year <= 1985)
                                .GroupBy(st => st.BirthDate.Month)
                                .Select(grp => new
                                {
                                    BirthMonth = grp.Key,
                                    AVGResult = grp.Average(st => st.Year_Result)
                                });

var result4_3bis = from st in context.Students
                   where st.BirthDate.Year >= 1970 && st.BirthDate.Year <= 1985
                   group st by st.BirthDate.Month into grp
                   select new
                   {
                       BirthMonth = grp.Key,
                       AVGResult = grp.Average(st => st.Year_Result)
                   };

#endregion

#region 4.4

/* Exercice 4.4 Donner pour toutes les sections qui compte plus de 3 étudiants, la moyenne des
résultats annuels (« AVGResult »). */

var result4_4 = context.Students.GroupBy(st => st.Section_ID)
                                .Where(grp => grp.Count() > 3)
                                .Select(grp => new
                                {
                                    SectionId = grp.Key,
                                    AVGResult = grp.Average(st => st.Year_Result)
                                });

#endregion

#region 4.5

/* Exercice 4.5 Donner pour chaque cours, le nom du professeur responsable ainsi que la section
dont le professeur fait partie.*/

var result4_5 = from c in context.Courses
                join p in context.Professors on c.Professor_ID equals p.Professor_ID
                join s in context.Sections on p.Section_ID equals s.Section_ID
                select new
                {
                    CourseName = c.Course_Name,
                    ProfessorName = p.Professor_Name,
                    SectionName = s.Section_Name
                };

// La version expression de requête est plus simple...
var result4_5bis = context.Courses.Join(
                                        context.Professors,
                                        c => c.Professor_ID,
                                        p => p.Professor_ID,
                                        (c, p) => new
                                        {
                                            Course = c,
                                            Professor = p
                                        }
                                    )
                                    .Join(
                                        context.Sections,
                                        cp => cp.Professor.Section_ID,
                                        s => s.Section_ID,
                                        (cp, s) => new
                                        {
                                            CourseName = cp.Course.Course_Name,
                                            ProfessorName = cp.Professor.Professor_Name,
                                            SectionName = s.Section_Name
                                        }
                                    );

#endregion

#region 4.6
/* Exercice 4.6 Donner pour chaque section, l’id, le nom et le nom de son délégué. Classer les
sections dans l’ordre inverse des id de section. */

var result4_6 = from s in context.Sections
                join st in context.Students on s.Delegate_ID equals st.Student_ID
                orderby s.Section_ID descending
                select new
                {
                    SectionId = s.Section_ID,
                    SectionName = s.Section_Name,
                    DelegateName = st.Last_Name
                };

var result4_6bis = context.Sections.Join(
                                        context.Students,
                                        s => s.Delegate_ID,
                                        st => st.Student_ID,
                                        (s, st) => new
                                        {
                                            SectionId = s.Section_ID,
                                            SectionName = s.Section_Name,
                                            DelegateName = st.Last_Name
                                        }
                                    )
                                    .OrderByDescending(res => res.SectionId);
#endregion

#region 4.7 
/* Exercice 4.7 Donner, pour toutes les sections, le nom des professeurs qui en sont membres
Section_ID - Section_Name :
-Professor_Name1
- Professor_Name2
- … */

var result4_7 = from s in sections
                join p in professors on s.Section_ID equals p.Section_ID into gProfs
                select new
                {
                    SectionId = s.Section_ID,
                    SectionName = s.Section_Name,
                    Professors = gProfs.Select(p => p.Professor_Name)
                };

foreach (var sp in result4_7)
{
    Console.WriteLine($"{sp.SectionId} - {sp.SectionName}");

    foreach (var p in sp.Professors)
    {
        Console.WriteLine($"\t - {p}");
    }
}

#endregion

#region 4.8

/* Exercice 4.8 Même objectif que la question 4.7, mais seules les sections comportant au moins
un professeur doivent être reprises. */

Console.WriteLine("4.8");

var result4_8 = from s in sections
                join p in professors on s.Section_ID equals p.Section_ID into gProfs
                //where gProfs.Count() >= 1 // Filtre pour ne garder que les sections avec au moins 1 prof!
                where gProfs.Any() // alternative au filtre
                select new
                {
                    SectionId = s.Section_ID,
                    SectionName = s.Section_Name,
                    Professors = gProfs.Select(p => p.Professor_Name)
                };

foreach (var sp in result4_8)
{
    Console.WriteLine($"{sp.SectionId} - {sp.SectionName}");

    foreach (var p in sp.Professors)
    {
        Console.WriteLine($"\t - {p}");
    }
}
#endregion

#region 4.9

/* Exercice 4.9 Donner à chaque étudiant ayant obtenu un résultat annuel supérieur ou égal à 12
son grade en fonction de son résultat annuel et sur base de la table grade. La liste doit être
classée dans l’ordre alphabétique des grades attribués. */

Console.WriteLine("4.9");
// CROSS JOIN (pas trop opti)
var result4_9 = from st in students
                from g in grades
                where st.Year_Result >= 12 && st.Year_Result >= g.Lower_Bound && st.Year_Result <= g.Upper_Bound
                orderby g.GradeName
                select new
                {
                    Name = st.Last_Name,
                    Result = st.Year_Result,
                    Grade = g.GradeName
                };

foreach (var sg in result4_9)
{
    Console.WriteLine($"{sg.Name} - {sg.Result} - {sg.Grade}");
}

Console.WriteLine("4.9_bis");
// students.Select -> grades.FirstOrDefault
var result4_9_bis = (from st in students
                     where st.Year_Result >= 12
                     select new
                     {
                         Name = st.Last_Name,
                         Result = st.Year_Result,
                         Grade = grades.FirstOrDefault(g => st.Year_Result >= g.Lower_Bound && st.Year_Result <= g.Upper_Bound)?.GradeName ?? "Pas de grade..."
                     }).OrderBy(sg => sg.Grade); // on ne peut pas trier avant car Grade n'existait pas avant le select

foreach (var sg in result4_9_bis)
{
    Console.WriteLine($"{sg.Name} - {sg.Result} - {sg.Grade}");
}
#endregion

#region 4.10

var result4_10 = context.Professors.GroupJoin(                                      // Groupe join entre Prof et course
                                        context.Courses,
                                        p => p.Professor_ID,
                                        c => c.Professor_ID,
                                        (prof, courses) => new
                                        {
                                            Prof = prof,                            // Un prof
                                            TempCourses = courses                   // Une liste de course (Potentiellement vide)
                                        }
                                   )
                                   .SelectMany(                                     // Fonction pour applanir le resultat
                                        res => res.TempCourses.DefaultIfEmpty(),    // → Permet d'obtenir un resultat comme "left join"
                                        (res, c) => new                             //   Le prof sans course, ne sont pas perdu
                                        {                                           //   contrairement à un Join (Inner join)
                                            res.Prof,
                                            Course = c
                                        }
                                   )
                                   .GroupJoin(                                      // Groupe join entre le resultat précédent et les section
                                        context.Sections,
                                        res => res.Prof.Section_ID,
                                        s => s.Section_ID,
                                        (res, sections) => new
                                        {
                                            Prof = res.Prof,
                                            Course = res.Course,
                                            TempSections = sections
                                        }
                                   )
                                   .SelectMany(                                     // Fonction pour applanir le resultat
                                        res => res.TempSections.DefaultIfEmpty(),
                                        (res, s) => new
                                        {
                                            ProfessorName = res.Prof.Professor_Name,
                                            Section = s?.Section_Name,
                                            CourseName = res.Course?.Course_Name,
                                            CourseEcts = res.Course?.Course_Ects
                                        }
                                   )
                                   .OrderByDescending(res => res.CourseEcts);

Console.Clear();
foreach (var item in result4_10)
{
    Console.WriteLine(item);
}
Console.WriteLine();

#endregion

#endregion