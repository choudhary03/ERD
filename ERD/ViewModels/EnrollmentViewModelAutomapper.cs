using ERD.Models;

namespace ERD.ViewModels
{
    public class EnrollmentViewModelAutomapper
    {
        public string Firstname { get; set; }
        public string ActivityName { get; set; }
        public int ActivityCount { get; set; }
        public List<string> ActivityList { get; set; }
        public List<ActivityCounter> ActivityCounterList { get; set; }
    }

    public class ActivityCounter
    {
        public string Name { get; set; }
        public int Counter { get; set; }
    }

}
