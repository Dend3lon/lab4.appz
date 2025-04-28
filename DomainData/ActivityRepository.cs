using DomainData.Models;

namespace DomainData
{
    public class ActivityRepository
    {
        private readonly AntiCafeContext _context;

        public ActivityRepository()
        {
            _context = new AntiCafeContext();
        }

        public void CreateActivity(string name)
        {
            var activity = new Activity
            {
                Name = name
            };
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        public bool DeleteActivity(int id)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.Id == id);
            if (activity == null)
                return false;

            _context.Activities.Remove(activity);
            _context.SaveChanges();
            return true;
        }

        public List<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }
    }
}
