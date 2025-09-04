using Exo_Linq_Context;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();
List<Student> students = context.Students;


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
                            .Select(s => new {
                                Nom = s.Last_Name,
                                ResultatAnnuel = s.Year_Result
                            });

var result2_4_bis_2 = students.Where(s => s.Year_Result <= 3)
                                .Select(s => new {
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
                            .Select(s => new {
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
                            .Select(s => new {
                                last_name = s.Last_Name,
                                section_id = s.Section_ID,
                                result_100 = s.Year_Result * 5
                            });

foreach (var rs in result2_7) {
    Console.WriteLine($"{rs.last_name} - {rs.section_id} - {rs.result_100}");
}
#endregion

#endregion