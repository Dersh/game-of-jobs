using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static int Value { get; set; }

    public class CompetenceContainer
    {
        public int id;
        public int score;
        public CompetenceContainer(int id)
        {
            this.id = id;
            score = 0;
        }
    }
    public enum CompetenceType : int {
        Accuracy = 1,
        Emphathy = 2,
        Communication = 3,
        FollowingProcedure = 4,
        AttentionToDetails = 5,
        RoutineWork = 6,
        FindingRegularities = 7
    }
    private static List<CompetenceContainer> _competences;
    public static List<CompetenceContainer> GetCompetences()
    {
        if (_competences == null)
        {
            _competences = new List<CompetenceContainer>();
            for (int i = 1; i < 8; i++)
            {
                _competences.Add(new CompetenceContainer(i));
            }
        }
        return _competences;
    }
    public static void SetCompetence(CompetenceType type, int value)
    {
        GetCompetences()[(int)type].score = value;
    }
    public static void AddCompetence(CompetenceType type)
    {
        GetCompetences()[(int)type].score++;
    }
    public static void SubtractCompetence(CompetenceType type)
    {
        GetCompetences()[(int)type].score--;
    }
}
