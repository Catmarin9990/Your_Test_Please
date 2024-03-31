using System;
using UnityEngine;

public class DocRandomGeneration : MonoBehaviour
{
    private string[] engNames =
        {
            "Ethan", "Olivia", "Benjamin", "Emma", "Alexander",
            "Sophia", "William", "Ava", "Daniel", "Charlotte",
            "James", "Mia", "Michael", "Emily", "Christopher",
        };

    private string[] engSurnames =
        {
            "Thompson", "Davis", "Clark", "Johnson", "Martinez",
            "White", "Carter", "Brown", "Rodriguez", "Miller",
            "Wilson", "Taylor", "Anderson", "Moore", "Garcia"
        };

    private string[] armNames =
    {
         "Arman", "Ani", "Gevorg", "Lilit", "Hayk",
         "Anna", "Tigran", "Siranush", "Vardan", "Nune",
         "Artur", "Marine", "Suren", "Satenik", "Karen",
         "Tatevik", "Gor", "Silva", "Gagik", "Gayane",
         "Hovhannes", "Mariam", "Vahagn", "Astghik", "Aram",
         "Anahit", "Ruben", "Lusine", "Mher", "Naira",
         "Ashot", "Emma", "Hrayr", "Zara", "Vahan",
         "Narine", "Aramayis", "Liana", "Gurgen", "Anush"
    };

    private string[] armSurnames =
    {
        "Harutyunyan", "Avetisyan", "Hakobyan", "Grigoryan", "Manukyan",
        "Ghazaryan", "Petrosyan", "Mkrtchyan", "Sargsyan", "Asatryan",
        "Ohanyan", "Melkonyan", "Vardanyan", "Minasyan", "Hovhannisyan",
        "Martirosyan", "Sahakyan", "Karapetyan", "Poghosyan", "Davtyan",
        "Gasparyan", "Hakobyan", "Tovmasyan", "Hovsepyan", "Sahakyan",
        "Grigoryan", "Mkhitaryan", "Gevorgyan", "Petrosyan", "Kazaryan",
        "Martirosyan", "Sargsyan", "Gasparyan", "Avagyan", "Harutyunyan",
        "Sahakyan", "Karapetyan", "Hovhannisyan", "Mkrtchyan", "Avetisyan"
    };

    private string[] faculty =
    {
        "Arts",
        "Science",
        "Engineering",
        "Medicine",
        "Law",
        "Business",
        "Social Sciences",
        "Education",
        "IT",
        "Humanities"
    };

    string[] subject = {
        "Math",
        "English",
        "Physics",
        "Chemistry",
        "Biology",
        "History",
        "Geography",
        "Literature",
        "Russian",
        "Health"
    };

    [Header("Chance System Settings")]
    [SerializeField] private int engNameChance = 15;
    [SerializeField] private int unknownStudentChance = 10;
    [SerializeField] private int correctChance = 60;

    [Header("Test Settings")]
    [SerializeField] private GameObject[] correctTests;
    [SerializeField] private GameObject[] studCorrectTests;
    [SerializeField] private GameObject[] studWrongTests;


    private GameController gameController;


    private void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }


    public void generateStudentInfo(ref string initials, ref string studId, ref string studFaculty,
                                ref string[] gradeList, ref string fromWhere, ref bool suitable)
    {
        string[] from = { "School", "College" };
        int chance = UnityEngine.Random.Range(0, 100);
        // name surname generate
        if (chance < engNameChance)
        {
            initials = engNames[UnityEngine.Random.Range(0, engNames.Length)] + ' ' + engSurnames[UnityEngine.Random.Range(0, engSurnames.Length)];
        }
        else
        {
            initials = armNames[UnityEngine.Random.Range(0, armNames.Length)] + ' ' + armSurnames[UnityEngine.Random.Range(0, armSurnames.Length)];
        }

        // ID generation
        studId = UnityEngine.Random.Range(10000000, 100000000).ToString();

        studFaculty = faculty[UnityEngine.Random.Range(0, faculty.Length)];

        fromWhere = from[UnityEngine.Random.Range(0, from.Length)];

        // generate greades
        gradeList = new string[subject.Length];

        int[] gradeTypeList = { 5, 10, 20, 100 };

        float gradeType = gradeTypeList[UnityEngine.Random.Range(0, gradeTypeList.Length)];
        int minGrade = 0, maxGrade = 0;
        string studPerformance = "";

        chance = UnityEngine.Random.Range(0, 100);
        if (chance < 34)
            studPerformance = "low";
        else if (chance < 64)
            studPerformance = "medium";
        else
            studPerformance = "high";

        switch (studPerformance)
        {
            case "low":
                if (gradeType == 5)
                {
                    minGrade = 3;
                    maxGrade = 4;
                }
                else
                {
                    minGrade = (int)((gradeType / 100f) * 40f);
                    maxGrade = (int)((gradeType / 100f) * 65f);
                }
                break;
            case "medium":
                if (gradeType == 5)
                {
                    minGrade = 3;
                    maxGrade = 5;
                }
                else
                {
                    minGrade = (int)((gradeType / 100f) * 65f);
                    maxGrade = (int)((gradeType / 100f) * 90f);
                }
                break;
            case "high":
                if (gradeType == 5)
                {
                    minGrade = 4;
                    maxGrade = (int)gradeType;
                }
                else
                {
                    minGrade = (int)((gradeType / 100f) * 80f);
                    maxGrade = (int)gradeType;
                }
                break;

        }
        for (int i = 0; i < subject.Length; i++)
        {
            gradeList[i] = subject[i] + ' ' + UnityEngine.Random.Range(minGrade, maxGrade + 1).ToString();
        }
        gradeChecker(studFaculty, gradeList, gradeType, ref suitable);
    }

    public void gradeChecker(string faculty, string[] gradeList, float gradeType, ref bool suitable)
    {
        float passGrade = (gradeType == 5) ? 3 : (gradeType / 100f) * 65f;
        suitable = true;
        string[] arr;
        switch (faculty)
        {
            case "Arts":
                arr = new string[] { "English", "History", "Literature" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Science":
                arr = new string[] { "Math", "Physics", "Chemistry" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Engineering":
                arr = new string[] { "Math", "Physics", "Chemistry" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Medicine":
                arr = new string[] { "Biology", "Chemistry", "Health" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Law":
                arr = new string[] { "History", "English", "Literature" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Business":
                arr = new string[] { "Math", "English", "Geography" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Social Sciences":
                arr = new string[] { "History", "Geography", "English" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Education":
                arr = new string[] { "English", "History", "Literature" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "IT":
                arr = new string[] { "Math", "Physics", "English" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
            case "Humanities":
                arr = new string[] { "English", "History", "Literature" };
                suitable = findAllGrades(gradeList, arr, passGrade);
                break;
        }
    }
    private bool findAllGrades(string[] gradeList, string[] facultyChecker, float passGrade)
    {
        string[] arr;

        arr = Array.FindAll(gradeList, (grade) =>
        {
            string faculty = grade.Split(' ')[0];

            if (faculty == facultyChecker[0] || faculty == facultyChecker[1] || faculty == facultyChecker[2])
            {
                if (float.Parse(grade.Split(' ')[1]) < passGrade)
                {
                    return false;
                }
                return true;
            }
            return false;
        });

        if (arr.Length == 3) return true;
        return false;
    }

    public void getTests(ref GameObject correctTest, ref GameObject studTest)
    {
        correctTest = correctTests[UnityEngine.Random.Range(0, correctTests.Length)];

        int chance = UnityEngine.Random.Range(0, 100);

        if (chance <= correctChance)
        {
            studTest = studCorrectTests[UnityEngine.Random.Range(0, studCorrectTests.Length)];
        }
        else
        {
            // heto
            studTest = studCorrectTests[UnityEngine.Random.Range(0, studCorrectTests.Length)];

        }
    }
}

public class documentClass
{
    private string initials;
    private string id;
    private string faculty;
    private string fromWhere;
    private string[] grades = null;
    public typeOfDoc type;
    public bool suitable;

    public enum typeOfDoc
    {
        studentId,
        diploma
    }
    public void createDoc(DocRandomGeneration docRandom)
    {
        docRandom.generateStudentInfo(ref initials, ref id, ref faculty, ref grades, ref fromWhere, ref suitable);
    }

    public (string, string, string) getStudentId()
    {
        type = typeOfDoc.studentId;
        return (initials, id, faculty);
    }

    public (string, string, string, string[]) getDiploma()
    {
        string name = initials.Split(' ')[0];
        string surname = initials.Split(" ")[1];
        type = typeOfDoc.diploma;


        return (name, surname, fromWhere, grades);
    }

}
