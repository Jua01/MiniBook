using MiniBook.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBook.Data.Repositories.Base
{
    public class BaseRepository
    {
        protected ResourceDbContext Context { get; set; }
        protected BaseRepository(ResourceDbContext context)
        {
            Context = context;
        }
    }
}
