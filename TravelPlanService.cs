using System;
using System.Text.Json;


namespace G11_Mahmudjon_Mirqobilov
{
    public class TravelPlanService
    {
        private readonly string filePath;

        public TravelPlanService(string path)
        {
            filePath = path;

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }

        public List<TravelPlan> GetAll()
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TravelPlan>>(json) ?? new List<TravelPlan>();
        }

        public void SaveAll(List<TravelPlan> plans)
        {
            var json = JsonSerializer.Serialize(plans, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void Add(TravelPlan plan)
        {
            var plans = GetAll();
            plans.Add(plan);
            SaveAll(plans);
        }

        public void Update(int id, TravelPlan updated)
        {
            var plans = GetAll();
            var index = plans.FindIndex(p => p.Id == id);
            if (index != -1)
            {
                plans[index] = updated;
                SaveAll(plans);
            }
        }

        public void Delete(int id)
        {
            var plans = GetAll();
            plans.RemoveAll(p => p.Id == id);
            SaveAll(plans);
        }

        public TravelPlan GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }


    }
};