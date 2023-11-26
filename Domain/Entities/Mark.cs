using Org.BouncyCastle.Pqc.Crypto.NtruPrime;

namespace SchoolHubApi.Domain.Entities
{
    public class Mark : Entity
    {
        public int MarkName { get; set; }
        public int HomeworkId { get; set; }
        public Homework Homework { get; set; }
        private Mark()
        {
        }
        public Mark(int markName)
        {
            MarkName = markName;
        }
    }
}
