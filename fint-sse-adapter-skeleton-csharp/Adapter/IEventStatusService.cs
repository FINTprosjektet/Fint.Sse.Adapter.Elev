﻿using FintEventModel.Model;

namespace Fint.SSE.Adapter
{
    public interface IEventStatusService
    {
        Event VerifyEvent(Event evt);
    }
}