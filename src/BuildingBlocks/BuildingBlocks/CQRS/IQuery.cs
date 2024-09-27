﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace BuildingBlocks.CQRS
{ 
    //this is just a wrapper over IRequest Interface to seperate Cammand & Query
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}