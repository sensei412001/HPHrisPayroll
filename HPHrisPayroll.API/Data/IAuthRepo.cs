using System.Threading.Tasks;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Data
{
    public interface IAuthRepo
    {
          Task<Users> Login(string employeeNo, string password);
          Task<Users> GetUser(string username);
    }
}