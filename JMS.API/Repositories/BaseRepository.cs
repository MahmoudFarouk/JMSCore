using JMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMS.API.Repositories
{
    public class BaseRepository
    {
        protected readonly JMSDBContext dbContext;

        public BaseRepository(JMSDBContext context)
        {
            dbContext = context;
        }
    }
}
