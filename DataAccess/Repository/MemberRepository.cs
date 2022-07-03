using BusinessObject.DataAccess;
using BusinessObject.Models;

namespace BusinessObject.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public bool CheckLogin(string userName, string password) => MemberDAO.Instance.CheckLogin(userName, password);

        public void DeleteMember(int memberID) => MemberDAO.Instance.DeleteMember(memberID);

        public Member GetMemberByID(int memberID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMembers();

        public void InsertMember(Member member) => MemberDAO.Instance.AddNewMember(member);

        public bool IsAdmin(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);
    }
}
