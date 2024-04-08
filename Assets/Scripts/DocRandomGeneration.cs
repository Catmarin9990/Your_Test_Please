using System;
using UnityEngine;

public class DocRandomGeneration : MonoBehaviour
{
    [NonSerialized] public string[] engBoyNames =
        {
            "Ethan", "Olivia", "Benjamin", "Alexander",
            "William", "Daniel", "James", "Christopher",
        };

    [NonSerialized] public string[] engGrlNames =
    {
            "Emma", "Sophia", "Ava", "Charlotte",
            "Mia", "Michael", "Emily"
        };
    [NonSerialized] public string[] engSurnames =
        {
            "Thompson", "Davis", "Clark", "Johnson", "Martinez",
            "White", "Carter", "Brown", "Rodriguez", "Miller",
            "Wilson", "Taylor", "Anderson", "Moore", "Garcia"
        };

    [NonSerialized] public string[] armBoyNames =
    {
         "Arman", "Gevorg", "Hayk", "Tigran", "Vardan", 
         "Artur", "Suren", "Karen", "Gor", "Gagik", "Hovhannes", 
         "Vahagn", "Aram", "Ruben", "Mher", "Ashot", "Hrayr", "Vahan",
         "Aramayis", "Gurgen"
    };
    [NonSerialized] public string[] armGrlNames =
    {
         "Ani", "Lilit", "Anna", "Siranush", "Nune",
         "Marine", "Satenik", "Tatevik", "Silva", 
         "Gayane", "Mariam", "Astghik", "Anahit", 
         "Lusine", "Naira", "Emma", "Zara", 
         "Narine", "Liana",  "Anush"
    };

    [NonSerialized] public string[] armSurnames =
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

    [NonSerialized] public string[] faculty =
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


    private string[] subject = {
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
    [SerializeField] private GameObject[] studTests;


    private GameController gameController;


    private void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }


    public void generateStudentInfo(ref string initials, ref string studId, ref string studFaculty,
                                ref string[] gradeList, ref string fromWhere, ref bool suitable, ref bool isGirl)
    {
        string[] from = { "School", "College" };
        int chance = UnityEngine.Random.Range(0, 100);
        // name surname generate
        if (chance < engNameChance)
        {
            chance = UnityEngine.Random.Range(1, 101);
            if(chance > 50)
            {
                initials = engBoyNames[UnityEngine.Random.Range(0, engBoyNames.Length)] + ' ' + engSurnames[UnityEngine.Random.Range(0, engSurnames.Length)];
                isGirl = false;
            }
            else
            {
                initials = engGrlNames[UnityEngine.Random.Range(0, engGrlNames.Length)] + ' ' + engSurnames[UnityEngine.Random.Range(0, engSurnames.Length)];
                isGirl = true;
            }
        }
        else
        {
            chance = UnityEngine.Random.Range(1, 101);
            if(chance >= 50)
            {
                initials = armBoyNames[UnityEngine.Random.Range(0, armBoyNames.Length)] + ' ' + armSurnames[UnityEngine.Random.Range(0, armSurnames.Length)];
                isGirl = false;
            }
            else
            {
                initials = armGrlNames[UnityEngine.Random.Range(0, armGrlNames.Length)] + ' ' + armSurnames[UnityEngine.Random.Range(0, armSurnames.Length)];
                isGirl = true;
            }
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
            if (correctTest == correctTests[0]) studTest = studTests[0];
            else
            {
                studTest = studTests[1];
            }
            gameController.isTestCorrect = true;
        }
        else
        {
            if (correctTest == correctTests[1]) studTest = studTests[0];
            else studTest = studTests[1];
            gameController.isTestCorrect = false;
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
    public bool infoChanged;
    public bool isGirl;

    public enum typeOfDoc
    {
        studentId,
        diploma
    }
    public void createDoc(DocRandomGeneration docRandom)
    {
        docRandom.generateStudentInfo(ref initials, ref id, ref faculty, ref grades, ref fromWhere, ref suitable, ref isGirl);
    }

    public (string, string, string) getStudentId()
    {
        type = typeOfDoc.studentId;
        return (initials, id, faculty);
    }

    public (string, string, string, string[]) getDiploma()
    {
        string name = initials.Split(' ')[0];
        string surname = initials.Split(' ')[1];
        type = typeOfDoc.diploma;


        return (name, surname, fromWhere, grades);
    }

}
