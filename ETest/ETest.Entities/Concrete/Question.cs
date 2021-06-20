using System;
using System.Collections.Generic;
using Core.Entities.Abstract;

namespace ETest.Entities.Concrete
{
    public class Question:IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public char Answer { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers  { get; set; }
    }
}