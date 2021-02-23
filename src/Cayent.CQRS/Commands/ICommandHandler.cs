using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        Task HandleAsync(TCommand command);
    }
}
