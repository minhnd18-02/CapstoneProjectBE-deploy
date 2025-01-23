using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BoardRepo : GenericRepo<Board>, IBoardRepo
    {
        public BoardRepo(ApiContext context) : base(context)
        {
        }
    }
}
