using Core.Entities.Abstract;

namespace ETest.Entities.Concrete
{
    public class UserAnswer:IEntity
    {
        public int Id { get; set; }
        public char Answer { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public bool IsTrue { get; set; }
        public int UserId { get; set; }
        public virtual User User  { get; set; }
    }
}