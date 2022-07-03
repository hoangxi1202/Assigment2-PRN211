using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
namespace BusinessObject.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        void InsertMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int memberID);
        Member GetMemberByID(int memberID);
        bool CheckLogin(string userName, string password);
        bool IsAdmin(string userName, string password);
    }
}
