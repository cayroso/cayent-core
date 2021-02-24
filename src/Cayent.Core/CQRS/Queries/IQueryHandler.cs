using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : class// IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResponse> HandleAsync(TQuery query);

    }
}
