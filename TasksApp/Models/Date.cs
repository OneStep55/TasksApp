using System.ComponentModel.DataAnnotations;

namespace TasksApp.Models
{
    public class Date
    {
        [Required]
        [Range(1, 31)]
        public int Day { get; set; }
        [Required]
        [Range(1, 12)]
        public int Month { get; set; }
        [Required]
        [Range(2000, 2100)]
        public int Year { get; set; }

       

        // Check day is valid or not
        public bool DateIsValid()
        {
            if (Month == 2) // February has 29 or 29 days
            {
                if (Year % 4 == 0 && Year % 100 != 0)
                {
                    if (Day > 29)
                    {
                        return false;
                    }
                } else if (Day > 28)
                {
                    return false;
                }
            } else if (Month == 4 || Month == 6 || Month == 9 || Month == 11) // 4,6,9,11 months has 30 days
            {
                if (Day > 30)
                {
                    return false;
                }
            } else if (Day > 31) // other month has 31 days
            {
                return false;
            }

            return true;
        }
    }
}
